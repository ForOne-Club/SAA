using Terraria.ModLoader.IO;

namespace SAA.Content.Planting.System
{
    public class PlantSystem : ModSystem
    {
        internal static HashSet<(int, int)> wet = new();
        public override void SaveWorldData(TagCompound tag)
        {
            List<int> i = new();
            List<int> j = new();
            foreach (var (x, y) in wet)
            {
                i.Add(x);
                j.Add(y);
            }
            tag["wet_x"] = i;
            tag["wet_y"] = j;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            List<int> i = (List<int>)tag.GetList<int>("wet_x");
            List<int> j = (List<int>)tag.GetList<int>("wet_y");
            wet = new();
            for (int k = 0; k < i.Count; k++)
            {
                wet.Add((i[k], j[k]));
            }
        }
    }

    public static class PlantData
    {
        internal static float CropGrowMult;
        private static readonly Dictionary<int, (int seed, int crop, int grow, float chance)> plant = new();
        public static void RegisterPlant(int tileType, int seed, int crop, int grow, float chance)
            => plant.Add(tileType, (seed, crop, grow, chance));
        public static bool HasPlant(Tile tile, out int grow)
        {
            grow = 0;
            if (tile.HasTile && plant.TryGetValue(tile.TileType, out var data))
            {
                grow = data.grow;
                return true;
            }
            return false;
        }
        public static int GetSeed(Tile tile) => plant[tile.TileType].seed;
        public static int GetCrop(Tile tile) => plant[tile.TileType].crop;
        public static int GetGrow(Tile tile) => plant[tile.TileType].grow;
        public static bool Growing(Tile tile)
        {
            int tileType = tile.TileType;
            if (CropGrowMult == 1) return true;
            float x = plant[tileType].chance;
            x += (1 - x) * CropGrowMult;
            int y = 1;
            while ((int)x != x)
            {
                x *= 10;
                y *= 10;
            }
            return Main.rand.NextBool((int)x, y);
        }
    }
}
