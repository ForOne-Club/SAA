using SAA.Content.Foods;
using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 辣椒 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override short FrameWidth => 34;
        protected override int GrowthRate => 12;
        protected override bool CanPick => true;
        protected override int HerbItemType => 5277;
        protected override int SeedItemType => ModContent.ItemType<辣椒种子>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 8, 28 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 32;
            TileObjectData.newTile.DrawYOffset = -12;//所以要上升12
        }
    }
}
