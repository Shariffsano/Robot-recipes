using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applied_exercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RecipeManager recipeManager = new RecipeManager();
            UserManager userManager = new UserManager();

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. View Recipes");
                Console.WriteLine("2. Add Recipe");
                Console.WriteLine("3. Delete Recipe");
                Console.WriteLine("4. Activate Recipe");
                Console.WriteLine("5. Check Active Recipe");
                Console.WriteLine("6. Exit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        recipeManager.DisplayRecipes();
                        break;

                    case "2":
                        recipeManager.AddRecipe();
                        break;

                    case "3":
                        recipeManager.DeleteRecipe();
                        break;

                    case "4":
                        recipeManager.ActivateRecipe();
                        break;

                    case "5":
                        recipeManager.CheckActiveRecipe();
                        break;

                    case "6":
                        exitProgram = true;
                        Console.WriteLine("Exiting the program. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }
    }

    class RecipeManager
    {
        private List<Recipe> recipes = new List<Recipe>();
        private string activeRecipe = null;

        public RecipeManager()
        {
            LoadRecipes();
        }

        public void DisplayRecipes()
        {
            Console.WriteLine("Recipes:");
            foreach (var recipe in recipes)
            {
                Console.WriteLine(recipe.Name);
            }
        }

        public void AddRecipe()
        {
            Console.Write("Enter recipe name: ");
            string name = Console.ReadLine();

            if (recipes.Any(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Recipe with the same name already exists. Choose a different name.");
                return;
            }

            Console.Write("Enter recipe size (e.g., 100x100x100): ");
            string size = Console.ReadLine();

            Recipe newRecipe = new Recipe(name, size);
            recipes.Add(newRecipe);

            SaveRecipes();
            Console.WriteLine("Recipe added successfully.");
        }

        public void DeleteRecipe()
        {
            Console.Write("Enter recipe name to delete: ");
            string name = Console.ReadLine();

            Recipe recipeToDelete = recipes.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (recipeToDelete != null)
            {
                recipes.Remove(recipeToDelete);
                SaveRecipes();
                Console.WriteLine("Recipe deleted successfully.");
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        public void ActivateRecipe()
        {
            Console.Write("Enter recipe name to activate: ");
            string name = Console.ReadLine();

            Recipe recipeToActivate = recipes.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (recipeToActivate != null)
            {
                activeRecipe = recipeToActivate.Name;
                Console.WriteLine($"Recipe '{activeRecipe}' activated.");
            }
            else
            {
                Console.WriteLine("Recipe not found.");
            }
        }

        public void CheckActiveRecipe()
        {
            if (activeRecipe != null)
            {
                Console.WriteLine($"Active Recipe: {activeRecipe}");
            }
            else
            {
                Console.WriteLine("No active recipe.");
            }
        }

        private void SaveRecipes()
        {
            using (StreamWriter writer = new StreamWriter("recipes.txt"))
            {
                foreach (var recipe in recipes)
                {
                    writer.WriteLine($"{recipe.Name},{recipe.Size}");
                }
            }
        }

        private void LoadRecipes()
        {
            if (File.Exists("recipes.txt"))
            {
                recipes.Clear();
                string[] lines = File.ReadAllLines("recipes.txt");

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        recipes.Add(new Recipe(parts[0], parts[1]));
                    }
                }
            }
        }
    }

    class Recipe
    {
        public string Name { get; }
        public string Size { get; }

        public Recipe(string name, string size)
        {
            Name = name;
            Size = size;
        }
    }

    class UserManager
    {
        // Implement user management if needed
    }
}