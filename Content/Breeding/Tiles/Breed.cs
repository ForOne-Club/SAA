using SAA.Content.Foods;
using SAA.Content.Sys;

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
        /// 物块宽度
        /// </summary>
        protected virtual int Width => 3;
        /// <summary>
        /// 物块高度, 2/3
        /// </summary>
        protected virtual int Height => 2;
        /// <summary>
        /// 生产速度1到100
        /// </summary>
        protected virtual int GrowthRate => 1;
        /// <summary>
        /// 生产速度1到100
        /// </summary>
        protected virtual bool NeedSun => true;
        /// <summary>
        /// null表示什么都不需要
        /// </summary>
        protected virtual int[] NeedItemType => new int[] { ItemID.Seed, ModContent.ItemType<瓜子>() };
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
            int x = i - g % Width;
            int y = j - Main.tile[i, j].TileFrameY / 18;

            BreedStage stage = GetStage(x, y);
            if (Main.dayTime || !needDayTime)//白天
            {
                if (Main.rand.NextFloat(100) < (GrowthRate * growMagnification / 3f / Height))//保证概率准确需要除以物块数量
                {
                    if (NeedItemType == null && stage != BreedStage.Product)
                    {
                        for (int w = 0; w < Width; w++)
                        {
                            for (int h = 0; h < Height; h++)
                            {
                                Main.tile[x + w, y + h].TileFrameX += (short)(Width * 18);
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    NetMessage.SendTileSquare(-1, x + w, y + h, 1);
                                }
                            }
                        }
                    }
                    else if (stage == BreedStage.Producting)
                    {
                        for (int w = 0; w < Width; w++)
                        {
                            for (int h = 0; h < Height; h++)
                            {
                                Main.tile[x + w, y + h].TileFrameX += (short)(Width * 18);
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
            TryGrow(i, j, HungerSetting.GrowMagnification, NeedSun);
        }
        public BreedStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return (BreedStage)(tile.TileFrameX / (Width * 18));
        }
        public override void MouseOver(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % Width;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            BreedStage stage = GetStage(x, y);
            Player player = Main.LocalPlayer;
            if (NeedItemType != null && stage == BreedStage.Breeded)
            {
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                bool canfind = false;
                for (int k = 0; k < NeedItemType.Length; k++)
                {
                    if (player.HasItem(NeedItemType[k]))
                    {
                        player.cursorItemIconID = NeedItemType[k];
                        canfind = true;
                        break;
                    }
                }
                if (!canfind)
                {
                    player.cursorItemIconID = NeedItemType[0];
                }
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
            int x = i - g % Width;
            int y = j - Main.tile[i, j].TileFrameY / 18;

            BreedStage stage = GetStage(x, y);
            Player player = Main.LocalPlayer;
            if (stage == BreedStage.Breeded)
            {
                for (int k = 0; k < NeedItemType.Length; k++)
                {
                    if (player.HasItem(NeedItemType[k]))
                    {
                        int item = player.FindItem(NeedItemType[k]);
                        player.inventory[item].stack--;
                        if (player.inventory[item].stack <= 0) player.inventory[item].TurnToAir();
                        //if (player.ConsumeItem(NeedItemType[k]))如果是食物的话将被算作食用
                        //{
                        for (int w = 0; w < Width; w++)
                        {
                            for (int h = 0; h < Height; h++)
                            {
                                Main.tile[x + w, y + h].TileFrameX += (short)(Width * 18);
                                if (Main.netMode != NetmodeID.SinglePlayer)
                                {
                                    NetMessage.SendTileSquare(-1, x + w, y + h, 1);
                                }
                            }
                        }
                        break;
                        //}
                    }
                }
            }
            else if (stage == BreedStage.Product)
            {
                for (int w = 0; w < Width; w++)
                {
                    for (int h = 0; h < Height; h++)
                    {
                        Main.tile[x + w, y + h].TileFrameX -= (short)(Width * 36);
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
                int item = Item.NewItem(new EntitySource_TileBreak(i, j), worldPosition, herbItemType, herbItemStack);
                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                }
            }
        }
        protected virtual void ModifyPick(ref int herbItemType, ref int herbItemStack) { }
        public override bool RightClick(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % Width;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            BreedStage stage = GetStage(x, y);
            if (stage != BreedStage.Producting)
            {
                if (NeedItemType == null && stage == BreedStage.Breeded)
                {
                    return false;
                }
                else
                {
                    TryPickOrFeed(i, j);
                    return true;
                }
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
