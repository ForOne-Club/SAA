using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 樱桃树 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override short FrameWidth => 34;
        protected override int GrowthRate => 12;
        public override bool CanPick => true;
        protected override int HerbItemType => 4286;
        protected override int SeedItemType => ModContent.ItemType<樱桃种子>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 8, 28 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 32;
            TileObjectData.newTile.DrawYOffset = -12;//所以要上升12
        }
    }
}
