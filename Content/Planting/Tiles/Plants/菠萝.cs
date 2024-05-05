using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 菠萝 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override short FrameWidth => 32;
        protected override int GrowthRate => 12;
        public override bool CanPick => true;
        protected override bool CropHarvestCantAffect => true;
        protected override int HerbItemType => ItemID.Pineapple;
        protected override int SeedItemType => ModContent.ItemType<菠萝头>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 8, 28 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 30;
            TileObjectData.newTile.DrawYOffset = -12;//所以要上升12
        }
    }
}
