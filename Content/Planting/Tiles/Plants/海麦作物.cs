using SAA.Content.Foods;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 海麦作物 : Plant
    {
        public override void RegisterPlant(ref int seed, ref int crop, ref int grow)
        {
            seed = -1;
            crop = ModContent.ItemType<海麦>();
            grow = 3;
        }
    }
}
