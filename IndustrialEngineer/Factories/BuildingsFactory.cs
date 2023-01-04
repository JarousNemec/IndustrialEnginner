using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using IndustrialEngineer.Blocks;
using IndustrialEnginner;
using IndustrialEnginner.Enums;
using IndustrialEnginner.GameEntities;
using IndustrialEnginner.Gui;
using SFML.Graphics;

namespace IndustrialEngineer.Factories
{
    public class BuildingsFactory
    {
        public static BuildingsRegistry LoadBuildings(string path)
        {
            var presets = LoadJson(path);
            var properties = MakePropertiesList(presets);
            var registry = new BuildingsRegistry();

            registry.Drill = new Drill(properties.Find(x => x.Name == "Drill"));
            registry.Registry.Add(registry.Drill);

            registry.Furnace = new Furnace(properties.Find(x => x.Name == "Furnace"));
            registry.Registry.Add(registry.Furnace);
            
            registry.WoodenPlatform = new WoodenPlatform(properties.Find(x => x.Name == "WoodenPlatform"));
            registry.Registry.Add(registry.WoodenPlatform);
            return registry;
        }

        public static void SetDialogsToMachines()
        {
            GameData.BuildingsRegistry.Drill.Properties.Dialog = GameData.DialogsRegistry.DrillDialog;
            GameData.BuildingsRegistry.Furnace.Properties.Dialog = GameData.DialogsRegistry.FurnaceDialog;
        }

        private static List<BuildingProperties> MakePropertiesList(List<PlaceableEntityPreset> presets)
        {
            List<BuildingProperties> propertiesList = new List<BuildingProperties>();
            foreach (var preset in presets)
            {
                List<Sprite> states = new List<Sprite>();
                foreach (var state in preset.States)
                {
                    states.Add(GameData.Sprites[state]);
                }
                propertiesList.Add(new BuildingProperties(GameData.Sprites[preset.Texture],states.ToArray(),preset.Name, preset.Id, (BlockType)preset.CanBePlacedOnType, preset.DropItemId, preset.CanStepOn, null, preset.MaximalBufferedEnergyValue));
            }
            return propertiesList;
        }

        private static List<PlaceableEntityPreset> LoadJson(string path)
        {
            List<PlaceableEntityPreset> presetsList;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                presetsList = JsonSerializer.Deserialize<List<PlaceableEntityPreset>>(json);
            }

            return presetsList;
        }

        private class PlaceableEntityPreset
        {
            public string Texture { get; set; }
            public string[] States { get; set; }
            public short Id { get; set; }
            public string Name { get; set; }
            public int CanBePlacedOnType { get; set; }
            public int DropItemId { get; set; }
            public bool CanStepOn { get; set; }
            public int MaximalBufferedEnergyValue { get; set; }
        }
    }
}