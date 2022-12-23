using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using GameEngine_druhypokus.Factories;
using IndustrialEngineer.Enums;
using IndustrialEnginner;
using IndustrialEnginner.CraftingRecipies;
using IndustrialEnginner.Items;
using SFML.Graphics;

namespace IndustrialEngineer.Factories
{
    public class RecipeFactory
    {
        public static RecipesRegistry LoadRecipes(string path, GameData gameData)
        {
            RecipesRegistry registry = new RecipesRegistry();
            var presets = LoadJson(path);
            registry.Registry = RecipesRegistrySetup(presets, gameData);
            return registry;
        }

        private static List<RecipePreset> LoadJson(string path)
        {
            List<RecipePreset> presets;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                presets = JsonSerializer.Deserialize<List<RecipePreset>>(json);
            }

            return presets;
        }

        private static List<Recipe> RecipesRegistrySetup(List<RecipePreset> presets, GameData gameData)
        {
            List<Recipe> recipes = new List<Recipe>();
            foreach (var preset in presets)
            {
                recipes.Add(new Recipe(preset.Name, gameData.GetSprites()[preset.Texture], preset.Id, preset.DropId,
                    preset.DropCount, (RecipeType)preset.RecipeType, preset.Ingredients));
            }

            return recipes;
        }
    }

    public class RecipePreset
    {
        public string Name { get; set; }
        public string Texture { get; set; }
        public int Id { get; set; }
        public int DropId { get; set; }
        public int DropCount { get; set; }
        public int RecipeType { get; set; }
        public Ingredient[] Ingredients { get; set; }
    }
}