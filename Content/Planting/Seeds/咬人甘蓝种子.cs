using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 咬人甘蓝种子 : Seed
    {
        protected override int TileType => ModContent.TileType<咬人甘蓝>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = 500;
        }
    }
}
