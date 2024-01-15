using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 生命果实 : Plant
    {
        protected override short FrameWidth => 30;
        protected override int GrowthRate => 5;
        protected override int HerbItemType => 1291;
        protected override int SeedItemType => ModContent.ItemType<生命果实种子>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 4, 28 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 28;
            TileObjectData.newTile.DrawYOffset = -12;//所以要上升12
        }
        protected override void ModifyDropHerbCount(ref int herbItemType, ref int herbItemStack, Player player, PlantStage stage)
        {
            if (stage == PlantStage.Grown)
            {
                herbItemStack = 1;
            }
        }
        protected override void ModifyDropSeedCount(ref int herbItemType, ref int seedItemStack, Player player, PlantStage stage) { }
    }
}
