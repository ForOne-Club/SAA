using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 李子树 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override short FrameWidth => 32;
        protected override int Height => 3;
        protected override int GrowthRate => 12;
        protected override bool CanPick => true;
        protected override bool CanSwayInWind => false;
        protected override bool FlipHorizontally => false;
        protected override int HerbItemType => ItemID.Plum;
        protected override int SeedItemType => ModContent.ItemType<李子种子>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 4, 4, 48 };
            TileObjectData.newTile.CoordinateWidth = 30;
            TileObjectData.newTile.DrawYOffset = -32;
        }
        protected override void ModifyDropSeedCount(ref int seedItemType, ref int seedItemStack, Player player, PlantStage stage)
        {
            if (stage != PlantStage.Planted)
            {
                seedItemType = ItemID.Wood;
                seedItemStack = Main.rand.Next(5, 9);
            }
        }
    }
}
