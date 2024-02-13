namespace SAA.Content.Planting.Tiles.Plants
{
    public class 丛林孢子 : Plant
    {
        protected override short FrameWidth => 18;
        protected override int GrowthRate => 15;
        protected override int HerbItemType => 331;
        protected override int SeedItemType => 331;
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 12, 20 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.DrawYOffset = -4;//所以要上升12
        }
    }
}
