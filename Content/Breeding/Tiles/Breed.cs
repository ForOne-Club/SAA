using SAA.Content.Foods;

namespace SAA.Content.Breeding.Tiles
{
    public enum BreedStage : byte
    {
        Breeded,
        Producting,
        Product
    }
    public abstract class Breed : ModTile
    {
        /// <summary>
        /// 物块高度, 2/3
        /// </summary>
        protected virtual int Height => 2;
        /// <summary>
        /// 生产速度1到100
        /// </summary>
        protected virtual int GrowthRate => 1;
        protected virtual int NeedItemType => 283;
        protected virtual int ProductItemType => ModContent.ItemType<蛋>();
        protected virtual int DropItemType => ModContent.ItemType<Items.鸭笼>();
        protected virtual void ModifyTileObjectData() { }
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            Main.tileNoFail[Type] = true;
            //TileID.Sets.ReplaceTileBreakUp[Type] = true;
            TileID.Sets.IgnoredInHouseScore[Type] = true;
            TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
            this.RegisterTile(Color.Brown);
            switch (Height)
            {
                case 2:
                    TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
                    break;
                default:
                    TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
                    break;
            }
            ModifyTileObjectData();
            TileObjectData.addTile(Type);
            HitSound = SoundID.Tink;
            DustType = DustID.WoodFurniture;
        }
        /// <summary>
        /// 尝试让牧场生产
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="growMagnification">生产倍率，与生产速度相乘提高生产概率</param>
        /// <param name="needDayTime">需要白天</param>
        public void TryGrow(int i, int j, int growMagnification = 1, bool needDayTime = true)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 3;
            int y = j - Main.tile[i, j].TileFrameY / 18;

            BreedStage stage = GetStage(x, y);
            if (Main.dayTime || !needDayTime)//白天
            {
                if (Main.rand.Next(100) < (GrowthRate * growMagnification))
                {
                    if (stage == BreedStage.Producting)
                    {
                        for (int w = 0; w < 3; w++)
                        {
                            for (int h = 0; h < Height; h++)
                            {
                                Main.tile[x + w, y + h].TileFrameX += 54;
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
            TryGrow(i, j);
        }
        public BreedStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return (BreedStage)(tile.TileFrameX / 54);
        }
        public override void MouseOver(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 3;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            BreedStage stage = GetStage(x, y);
            Player player = Main.LocalPlayer;
            if (stage == BreedStage.Breeded)
            {
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = NeedItemType;
            }
            if (stage == BreedStage.Product)
            {
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = ProductItemType;
            }
        }
        public void TryPickOrFeed(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 3;
            int y = j - Main.tile[i, j].TileFrameY / 18;

            BreedStage stage = GetStage(x, y);
            Player player = Main.LocalPlayer;
            if (stage == BreedStage.Breeded)
            {
                if (player.HasItem(NeedItemType))
                {
                    if (player.ConsumeItem(NeedItemType))
                    {
                        for (int w = 0; w < 3; w++)
                        {
                            for (int h = 0; h < Height; h++)
                            {
                                Main.tile[x + w, y + h].TileFrameX += 54;
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    NetMessage.SendTileSquare(-1, x + w, y + h, 1);
                                }
                            }
                        }
                    }
                }
            }
            else if (stage == BreedStage.Product)
            {
                for (int w = 0; w < 3; w++)
                {
                    for (int h = 0; h < Height; h++)
                    {
                        Main.tile[x + w, y + h].TileFrameX -= 108;
                        if (Main.netMode != NetmodeID.SinglePlayer)
                        {
                            NetMessage.SendTileSquare(-1, x + w, y + h, 1);
                        }
                    }
                }

                Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
                int herbItemType = ProductItemType;//收获
                int herbItemStack = 1;
                ModifyPick(ref herbItemType, ref herbItemStack);
                Item.NewItem(new EntitySource_TileBreak(i, j), worldPosition, herbItemType, herbItemStack);
            }
        }
        protected virtual void ModifyPick(ref int herbItemType, ref int herbItemStack) { }
        public override bool RightClick(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 3;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            BreedStage stage = GetStage(x, y);
            if (stage != BreedStage.Producting)
            {
                TryPickOrFeed(i, j);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool CanDrop(int i, int j)
        {
            Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
            Item.NewItem(new EntitySource_TileBreak(i, j), worldPosition, DropItemType);
            return false;
        }
    }
}
