using SAA.Content.Planting.System;

namespace SAA.Content.Planting.Tiles.Plants
{
    public abstract class Plant : ModTile
    {
        public abstract void RegisterPlant(ref int seed, ref int crop, ref int grow);
        public override void SetStaticDefaults()
        {
            this.SetTileBase(false, false, false, false, true, true, true, false, false, false);
            this.SetTileValue(false, true, 0, true, 0, 0, 0);
            this.RegisterTile(Color.YellowGreen);
            int seed = 0, crop = 0, grow = 0;
            RegisterPlant(ref seed, ref crop, ref grow);
            PlantData.RegisterPlant(Type, seed, crop, grow, 0.001f);
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = -2;
        }
    }
}
