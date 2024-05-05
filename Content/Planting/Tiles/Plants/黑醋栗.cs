using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 黑醋栗 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override short FrameWidth => 34;
        protected override int GrowthRate => 12;
        public override bool CanPick => true;
        protected override int HerbItemType => ItemID.BlackCurrant;
        protected override int SeedItemType => ModContent.ItemType<黑醋栗种子>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 8, 28 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 32;
            TileObjectData.newTile.DrawYOffset = -12;//所以要上升12
        }
    }
}
