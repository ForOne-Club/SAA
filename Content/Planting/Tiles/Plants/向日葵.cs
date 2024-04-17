namespace SAA.Content.Planting.Tiles.Plants
{
    public class 向日葵 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override int Height => 4;
        protected override short FrameWidth => 36;
        protected override int GrowthRate => 10;
        protected override bool CanPick => true;
        protected override bool PickJustOneTime => true;
        protected override bool CanSwayInWind => false;
        protected override bool FlipHorizontally => false;
        protected override int HerbItemType => ModContent.ItemType<Foods.瓜子>();
        protected override void ModifyPick(ref int herbItemType, ref int herbItemStack)
        {
            herbItemStack = 3;
        }
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };
            TileObjectData.newTile.AnchorValidTiles = new[] { (int)TileID.Grass };
            TileObjectData.newTile.Origin = new Point16(0, 3);
        }
        public override void TryGrow(int i, int j, int growMagnification = 1, bool needDayTime = true, bool needWet = true)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18 + Height - 1;

            PlantStage stage = GetStage(x, y);
            if (Main.dayTime)
            {
                if (Main.rand.NextFloat(100) < (GrowthRate * growMagnification) / 8f)
                {
                    if (stage != PlantStage.Grown)
                    {
                        for (int w = 0; w < 2; w++)
                        {
                            for (int h = 0; h < Height; h++)
                            {
                                Main.tile[x + w, y - h].TileFrameX += FrameWidth;
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    NetMessage.SendTileSquare(-1, x + w, y - h, 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int w = 0; w < 2; w++)
                        {
                            for (int h = 0; h < Height; h++)
                            {
                                Main.tile[x + w, y - h].ClearTile();
                            }
                        }
                        WorldGen.PlaceSunflower(x, y);
                        //WorldGen.Place2xX(x, y - 1, TileID.Sunflower);
                    }
                }
            }
        }
    }
}
