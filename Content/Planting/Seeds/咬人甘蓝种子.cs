using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 咬人甘蓝种子 : 种子袋
    {
        protected override int TileType => ModContent.TileType<咬人甘蓝>();
        protected override int ItemType => ModContent.ItemType<Items.咬人甘蓝>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = 500;
        }
    }
}
