using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RecipeApp
{
    public partial class MainWindow : Window
    {
        private RecipeManager recipeManager;
        private List<Ingredient> currentIngredients;
        private List<TextBox> stepTextBoxes;

        public MainWindow()
        {
            InitializeComponent();
            recipeManager = new RecipeManager();
            currentIngredients = new List<Ingredient>();
            stepTextBoxes = new List<TextBox>();
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            string name = IngredientNameTextBox.Text.Trim();
            if (!double.TryParse(IngredientQuantityTextBox.Text, out double quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }
            string unit = IngredientUnitTextBox.Text.Trim();
            if (!double.TryParse(IngredientCaloriesTextBox.Text, out double calories) || calories <= 0)
            {
                MessageBox.Show("Please enter valid calories.");
                return;
            }
            string foodGroup = IngredientFoodGroupTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(unit) || string.IsNullOrWhiteSpace(foodGroup))
            {
                MessageBox.Show("Please fill out all ingredient fields.");
                return;
            }

            Ingredient ingredient = new Ingredient
            {
                Name = name,
                Quantity = quantity,
                Unit = unit,
                Calories = calories,
                FoodGroup = foodGroup
            };

            currentIngredients.Add(ingredient);
            ClearIngredientFields();
            MessageBox.Show("Ingredient added.");
        }

        private void ClearIngredientFields()
        {
            IngredientNameTextBox.Clear();
            IngredientQuantityTextBox.Clear();
            IngredientUnitTextBox.Clear();
            IngredientCaloriesTextBox.Clear();
            IngredientFoodGroupTextBox.Clear();
        }

        private void GenerateStepFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(NumberOfStepsTextBox.Text, out int numberOfSteps) || numberOfSteps <= 0)
            {
                MessageBox.Show("Please enter a valid number of steps.");
                return;
            }

            StepsPanel.Children.Clear();
            stepTextBoxes.Clear();

            for (int i = 0; i < numberOfSteps; i++)
            {
                var stepTextBox = new TextBox
                {
                    Margin = new Thickness(0, 0, 0, 10)
                };
                StepsPanel.Children.Add(new TextBlock { Text = $"Step {i + 1}:" });
                StepsPanel.Children.Add(stepTextBox);
                stepTextBoxes.Add(stepTextBox);
            }
        }

        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = RecipeNameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(recipeName))
            {
                MessageBox.Show("Please enter a recipe name.");
                return;
            }

            Recipe recipe = new Recipe(recipeName);
            foreach (var ingredient in currentIngredients)
            {
                recipe.AddIngredient(ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.Calories, ingredient.FoodGroup);
            }

            foreach (var stepTextBox in stepTextBoxes)
            {
                string step = stepTextBox.Text.Trim();
                if (!string.IsNullOrWhiteSpace(step))
                {
                    recipe.AddStep(step);
                }
            }

            recipeManager.AddRecipe(recipe);
            currentIngredients.Clear();
            ClearRecipeFields();
            UpdateRecipeList();
        }

        private void ClearRecipeFields()
        {
            RecipeNameTextBox.Clear();
            StepsPanel.Children.Clear();
            stepTextBoxes.Clear();
        }

        private void ApplyFilterButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = FilterIngredientTextBox.Text.Trim();
            string foodGroup = (FilterFoodGroupComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            bool isCaloriesValid = double.TryParse(FilterCaloriesTextBox.Text, out double maxCalories);

            var filteredRecipes = recipeManager.GetRecipes()
                .Where(r => string.IsNullOrWhiteSpace(ingredient) || r.Ingredients.Any(i => i.Name.Equals(ingredient, StringComparison.OrdinalIgnoreCase)))
                .Where(r => string.IsNullOrWhiteSpace(foodGroup) || r.Ingredients.Any(i => i.FoodGroup.Equals(foodGroup, StringComparison.OrdinalIgnoreCase)))
                .Where(r => !isCaloriesValid || r.CalculateTotalCalories() <= maxCalories);

            RecipesListBox.Items.Clear();
            foreach (var recipe in filteredRecipes)
            {
                RecipesListBox.Items.Add(recipe.RecipeID);
            }
        }

        private void UpdateRecipeList()
        {
            RecipesListBox.Items.Clear();
            foreach (var recipe in recipeManager.GetRecipes())
            {
                RecipesListBox.Items.Add(recipe.RecipeID);
            }
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }
    }

    public class Recipe
    {
        public string RecipeID { get; set; }
        public List<Ingredient> Ingredients { get; }
        public List<string> Steps { get; }

        public Recipe(string recipeID)
        {
            RecipeID = recipeID;
            Ingredients = new List<Ingredient>();
            Steps = new List<string>();
        }

        public void AddIngredient(string name, double quantity, string unit, double calories, string foodGroup)
        {
            Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit, Calories = calories, FoodGroup = foodGroup });
        }

        public void AddStep(string description)
        {
            Steps.Add(description);
        }

        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(i => i.Calories);
        }
    }

    public class RecipeManager
    {
        private List<Recipe> recipes;

        public RecipeManager()
        {
            recipes = new List<Recipe>();
        }

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        public List<Recipe> GetRecipes()
        {
            return recipes;
        }
    }
}
