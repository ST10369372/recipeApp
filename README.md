# recipeApp
### Instructions for Compiling and Running the Software



Steps to Compile the Software
1. **Download the Source Code**: Obtain the source code from the provided repository or source.
2. **Open the Project**:
   - Open Visual Studio.
   - Select `Open a project or solution` from the start screen.
   - Navigate to the folder where the source code is located and select the solution file (`.sln`).
3. **Restore NuGet Packages**:
   - Go to `Tools > NuGet Package Manager > Manage NuGet Packages for Solution`.
   - Click on `Restore` to restore any missing packages.
4. **Build the Project**:
   - Select `Build > Build Solution` from the menu or press `Ctrl+Shift+B`.
   - Ensure the build is successful and there are no errors.

#### Steps to Run the Software
1. **Run the Application**:
   - After a successful build, press `F5` or select `Debug > Start Debugging` to run the application.
   - Alternatively, you can select `Debug > Start Without Debugging` to run the application without attaching the debugger.
2. **Use the Application**:
   - The main window of the application will open, allowing you to add recipes, ingredients, and steps, and view or filter recipes as described in the user manual.


     GitHub Repository: https://github.com/ST10369372/recipeApp.git

 Description of Changes Based on Feedback

Based on the lecturer's feedback, the following changes were made to the Recipe Application:

1. **Improved Input Validation**: Enhanced the validation logic to ensure that recipe names contain only letters and that ingredients, quantities, and steps are entered correctly.
2. **Dynamic Step Input**: Modified the steps input method to allow users to specify the number of steps and dynamically generate text boxes for each step. This improves the user experience by providing a clear structure for entering multiple steps.
3. **Independent Ingredient Input Fields**: Reorganized the ingredient input section to have independent text boxes for each attribute (name, quantity, unit, calories, food group). This change enhances clarity and usability, ensuring users can input ingredients more efficiently and with fewer errors.
4. **User Interface Enhancements**: Made several UI improvements to ensure a more intuitive and user-friendly experience, such as clear labeling, organized layout, and interactive elements for generating step fields.


