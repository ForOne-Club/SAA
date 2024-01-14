using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 蓝莓 : Plant
    {
        public override short FrameWidth => 34;
        protected override int GrowthRate => 10;
        protected override bool CanPick => true;
        protected override int HerbItemType => ModContent.ItemType<Foods.蓝莓>();
        protected override int SeedItemType => ModContent.ItemType<蓝莓种子>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 8, 28 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 32;
            TileObjectData.newTile.DrawYOffset = -12;//所以要上升12
        }
    }
}
