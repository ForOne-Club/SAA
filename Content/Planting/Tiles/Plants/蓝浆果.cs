namespace SAA.Content.Planting.Tiles.Plants
{
    public class 蓝浆果 : Plant
    {
        protected override int GrowthRate => 20;
        public override short FrameWidth => 34;
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 8, 28 };
            TileObjectData.newTile.CoordinateWidth = 32;
        }
    }
}
