using SAA.Content.Foods;
using SAA.Content.Items;
using SAA.Content.Planting.Seeds;
using SAA.Content.Planting.System;
using Terraria.GameContent.Metadata;

namespace SAA.Content.Planting.Tiles.Plants
{
    public enum PlantStage : byte
    {
        Planted,
        Growing,
        Grown
    }
    public abstract class Plant : ModTile
    {
        /// <summary>
        /// 生长阶段宽度
        /// </summary>
        protected virtual short FrameWidth => 18;
        /// <summary>
        /// 物块高度, 1 or 2
        /// </summary>
        protected virtual int Height => 2;
        /// <summary>
        /// 生长速度1到100
        /// </summary>
        protected virtual int GrowthRate => 1;
        /// <summary>
        /// 采摘
        /// </summary>
        protected virtual bool CanPick => false;
        protected virtual int HerbItemType => ModContent.ItemType<海麦>();
        protected virtual int SeedItemType => ModContent.ItemType<海燕麦种子>();
        protected virtual void ModifyTileObjectData() { }
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;
            //Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            //TileID.Sets.ReplaceTileBreakUp[Type] = true;
            TileID.Sets.IgnoredInHouseScore[Type] = true;
            TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);
            // 让这种瓷砖与高尔夫球互动，就像其他植物一样

            // 我们不使用这个，因为我们的瓷砖应该只有在它完全长大的时候才能被发现。这就是我们使用IsTileSpelunkable钩子的原因
            //Main.tileSpelunker[Type] = true;

            // 不要使用这个，它会引起许多意想不到的副作用
            //Main.tileAlch[Type] = true;

            this.RegisterTile(Color.Green);
            if (Height == 2) TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
            else TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            ModifyTileObjectData();
            TileObjectData.newTile.AnchorValidTiles = new int[] {
                ModContent.TileType<Arable>(),
            };
            TileObjectData.addTile(Type);

            HitSound = SoundID.Grass;
            DustType = DustID.Grass;
        }
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 0)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
        public override bool IsTileSpelunkable(int i, int j)
        {
            PlantStage stage = GetStage(i, j);
            return stage == PlantStage.Grown;
        }
        /// <summary>
        /// 尝试让作物生长
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="growMagnification">生长倍率，与生长速度相乘提高生长概率</param>
        /// <param name="needDayTime">需要白天</param>
        /// <param name="needWet">需要耕地潮湿</param>
        public void TryGrow(int i, int j, int growMagnification = 1, bool needDayTime = true, bool needWet = true)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            if (Height == 2 && tile.TileFrameY == 0) return;//顶端物块无效
            PlantStage stage = GetStage(i, j);
            Tile land = Framing.GetTileSafely(i, j + 1);
            bool flag = land.TileType == ModContent.TileType<Arable>();
            if (land.HasTile && flag)
            {
                if ((Main.dayTime || !needDayTime) && (PlowlandSystem.wet.Contains((i, j + 1)) || !needWet))//白天与湿地生长
                {
                    if (Main.rand.Next(100) < (GrowthRate * growMagnification))
                    {
                        if (stage != PlantStage.Grown)
                        {
                            tile.TileFrameX += FrameWidth;
                            if (Height == 2) Main.tile[i, j - 1].TileFrameX += FrameWidth;
                            if (Main.netMode != NetmodeID.SinglePlayer)
                            {
                                NetMessage.SendTileSquare(-1, i, j, 1);
                                if (Height == 2) NetMessage.SendTileSquare(-1, i, j - 1, 1);
                            }
                        }
                    }
                }
            }
            else
            {
                WorldGen.KillTile(i, j, false, false, true);
            }
        }
        public override void RandomUpdate(int i, int j)
        {
            TryGrow(i, j);
        }
        public PlantStage GetStage(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            return (PlantStage)(tile.TileFrameX / FrameWidth);
        }
        protected virtual void ModifyDropHerbCount(ref int herbItemStack, Player player, PlantStage stage)
        {
            if (stage == PlantStage.Grown)//可采摘的作物不会因为丰收镰刀增加收获
            {
                if (player.HeldItem.type == ModContent.ItemType<丰收镰刀>() && !CanPick) herbItemStack = Main.rand.Next(1, 3);
                else herbItemStack = 1;
            }
        }
        protected virtual void ModifyDropSeedCount(ref int seedItemStack, Player player, PlantStage stage)
        {
            if (!CanPick)//可采摘的作物不会掉落种子
            {
                if (player.HeldItem.type == ItemID.Sickle)
                    seedItemStack = 1;
                else if (player.HeldItem.type == ModContent.ItemType<丰收镰刀>())
                {
                    if (stage == PlantStage.Grown) seedItemStack = Main.rand.Next(1, 3);
                    else seedItemStack = 1;
                }
            }
        }
        public override bool CanDrop(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            PlantStage stage = GetStage(i, j);

            if (Height == 2 && tile.TileFrameY == 0)//第二格不掉落
            {
                return false;
            }

            Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
            Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];

            int herbItemType = HerbItemType;//收获
            int herbItemStack = 0;

            int seedItemType = SeedItemType;//种子
            int seedItemStack = 0;


            if (nearestPlayer.active)
            {
                ModifyDropHerbCount(ref herbItemStack, nearestPlayer, stage);
                ModifyDropSeedCount(ref seedItemStack, nearestPlayer, stage);
            }

            var source = new EntitySource_TileBreak(i, j);

            if (herbItemType > 0 && herbItemStack > 0)
            {
                Item.NewItem(source, worldPosition, herbItemType, herbItemStack);
            }

            if (seedItemType > 0 && seedItemStack > 0)
            {
                Item.NewItem(source, worldPosition, seedItemType, seedItemStack);
            }

            return false;
        }
        public override void MouseOver(int i, int j)
        {
            PlantStage stage = GetStage(i, j);
            if (CanPick && stage == PlantStage.Grown)
            {
                Player player = Main.LocalPlayer;
                player.noThrow = 2;
                player.cursorItemIconEnabled = true;
                player.cursorItemIconID = HerbItemType;
            }
        }
        protected virtual void ModifyPick(ref int herbItemType, ref int herbItemStack) { }
        public override bool RightClick(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j);
            PlantStage stage = GetStage(i, j);

            if (stage == PlantStage.Grown)
            {
                if (Height == 2 && tile.TileFrameY == 0)
                {
                    tile.TileFrameX -= FrameWidth;
                    if (Height == 2) Main.tile[i, j + 1].TileFrameX -= FrameWidth;
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendTileSquare(-1, i, j, 1);
                        if (Height == 2) NetMessage.SendTileSquare(-1, i, j + 1, 1);
                    }
                }
                else
                {
                    tile.TileFrameX -= FrameWidth;
                    if (Height == 2) Main.tile[i, j - 1].TileFrameX -= FrameWidth;
                    if (Main.netMode != NetmodeID.SinglePlayer)
                    {
                        NetMessage.SendTileSquare(-1, i, j, 1);
                        if (Height == 2) NetMessage.SendTileSquare(-1, i, j - 1, 1);
                    }
                }
                Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
                int herbItemType = HerbItemType;//收获
                int herbItemStack = 1;
                ModifyPick(ref herbItemType, ref herbItemStack);
                Item.NewItem(new EntitySource_TileBreak(i, j), worldPosition, herbItemType, herbItemStack);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
