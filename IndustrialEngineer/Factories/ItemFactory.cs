using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using IndustrialEnginner;
using IndustrialEnginner.Blocks;
using IndustrialEnginner.Items;

namespace GameEngine_druhypokus.Factories
{
    public class ItemFactory
    {
        public static ItemRegistry LoadItems(string path)
        {
            ItemRegistry itemRegistry = new ItemRegistry();
            var presets = LoadJson(path);

            itemRegistry.Log = new Log(ItemPropertiesSetup(presets, "Log"));
            itemRegistry.Registry.Add(itemRegistry.Log);

            itemRegistry.Stone = new Stone(ItemPropertiesSetup(presets, "Stone"));
            itemRegistry.Registry.Add(itemRegistry.Stone);

            itemRegistry.RawIron = new RawIron(ItemPropertiesSetup(presets, "RawIron"));
            itemRegistry.Registry.Add(itemRegistry.RawIron);

            itemRegistry.IronIngot = new IronIngot(ItemPropertiesSetup(presets, "IronIngot"));
            itemRegistry.Registry.Add(itemRegistry.IronIngot);

            itemRegistry.Drill = new Drill(ItemPropertiesSetup(presets, "Drill"));
            itemRegistry.Registry.Add(itemRegistry.Drill);

            itemRegistry.Furnace = new Furnace(ItemPropertiesSetup(presets, "Furnace"));
            itemRegistry.Registry.Add(itemRegistry.Furnace);

            itemRegistry.WoodenPlatform = new WoodenPlatform(ItemPropertiesSetup(presets, "WoodenPlatform"));
            itemRegistry.Registry.Add(itemRegistry.WoodenPlatform);

            itemRegistry.Energy = new Energy(ItemPropertiesSetup(presets, "Energy"));
            itemRegistry.Registry.Add(itemRegistry.Energy);

            itemRegistry.Time = new Time(ItemPropertiesSetup(presets, "Time"));
            itemRegistry.Registry.Add(itemRegistry.Time);

            return itemRegistry;
        }

        private static List<ItemPreset> LoadJson(string path)
        {
            List<ItemPreset> presets;
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                presets = JsonSerializer.Deserialize<List<ItemPreset>>(json);
            }

            return presets;
        }

        private static ItemProperties ItemPropertiesSetup(List<ItemPreset> presets, string name)
        {
            var preset = presets.Find(x => x.Name == name);
            return new ItemProperties(preset.Id, preset.Name, GameData.Sprites[preset.Texture],
                preset.MaxStackCount,preset.Flammable, preset.CalorificValue, preset.Placeable, preset.PlacedEntityId);
        }
    }

    public class ItemPreset
    {
        public string Name { get; set; }
        public string Texture { get; set; }
        public int Id { get; set; }
        public int MaxStackCount { get; set; }
        public bool Placeable { get; set; }
        public int PlacedEntityId { get; set; }
        public bool Flammable { get; set; }
        public int CalorificValue { get; set; }
    }
}