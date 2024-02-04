using SAA.Content.Foods;

namespace SAA.Content.Placeable.Tiles
{
    public class 丛林鸟巢 : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileNoFail[Type] = true;
            //TileID.Sets.ReplaceTileBreakUp[Type] = true;
            TileID.Sets.IgnoredInHouseScore[Type] = true;
            TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
            this.RegisterTile(Color.Brown);
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.DrawYOffset = 4;
            TileObjectData.addTile(Type);
            HitSound = SoundID.Dig;
            DustType = DustID.WoodFurniture;
        }
        private void TryGrow(int i, int j, bool needDayTime = true)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;

            if (Main.dayTime || !needDayTime)//白天
            {
                if (Main.rand.Next(100) < 6)
                {
                    if (Main.tile[x, y].TileFrameX == 0)
                    {
                        for (int w = 0; w < 2; w++)
                        {
                            for (int h = 0; h < 2; h++)
                            {
                                Main.tile[x + w, y + h].TileFrameX += 36;
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    NetMessage.SendTileSquare(-1, x + w, y + h, 1);
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void RandomUpdate(int i, int j)
        {
            TryGrow(i, j, false);
        }
        public override bool CanDrop(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;

            if (Main.tile[x, y].TileFrameX != 0)
            {
                Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
                Item.NewItem(new EntitySource_TileBreak(i, j), worldPosition, ModContent.ItemType<蛋>());
            }
            return false;
        }
        public override void MouseOver(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            Player player = Main.LocalPlayer;
            if (Main.tile[x, y].TileFrameX != 0)
            {
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = ModContent.ItemType<蛋>();
            }
        }
        public void TryPickOrFeed(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;

            Player player = Main.LocalPlayer;
            if (Main.tile[x, y].TileFrameX != 0)
            {
                for (int w = 0; w < 2; w++)
                {
                    for (int h = 0; h < 2; h++)
                    {
                        Main.tile[x + w, y + h].TileFrameX -= 36;
                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            NetMessage.SendTileSquare(-1, x + w, y + h, 1);
                        }
                    }
                }

                Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
                int herbItemType = ModContent.ItemType<蛋>();//收获
                int herbItemStack = 1;
                int item = Item.NewItem(new EntitySource_TileBreak(i, j), worldPosition, herbItemType, herbItemStack);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                }
            }
        }
        public override bool RightClick(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            if (Main.tile[x, y].TileFrameX != 0)
            {
                TryPickOrFeed(i, j);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
