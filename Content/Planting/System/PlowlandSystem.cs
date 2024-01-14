using Terraria.ModLoader.IO;

namespace SAA.Content.Planting.System
{
    public class PlowlandSystem : ModSystem
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
}
