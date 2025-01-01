using SAA.Content.Foods;
using SAA.Content.Items;
using SAA.Content.Placeable.Tiles;
using SAA.Content.Planting.Seeds;

namespace SAA.Content.Sys
{
    public class GlobalTileForFood : GlobalTile
    {
        public override void Load()
        {
            On_WorldGen.KillTile_ShouldDropSeeds += WorldGen_KillTile_ShouldDropSeeds;
            base.Load();
        }
        private static Player GetPlayerForTile(int x, int y)
        {
            return Main.player[Player.FindClosest(new Vector2(x, y) * 16f, 16, 16)];
        }
        private bool WorldGen_KillTile_ShouldDropSeeds(On_WorldGen.orig_KillTile_ShouldDropSeeds orig, int x, int y)
        {
            if (GetPlayerForTile(x, y).HeldItem.type == ModContent.ItemType<丰收镰刀>())
            {
                return true;
            }
            else if (Main.rand.NextBool(2))
            {
                return ShouldDropSeeds(x, y);
            }
            return false;
        }
        private static bool ShouldDropSeeds(int x, int y)
        {
            //让背包里有这些物品的玩家可以在割草时得到种子
            if (!GetPlayerForTile(x, y).HasItem(281))
            {
                if (!GetPlayerForTile(x, y).HasItem(986))
                {
                    if (!GetPlayerForTile(x, y).HasItem(ModContent.ItemType<种子机关枪>()))
                    {
                        return GetPlayerForTile(x, y).HasItem(ModContent.ItemType<作物采集器>());
                    }
                }
            }
            return true;
        }
        public override void RandomUpdate(int i, int j, int type)
        {
            if (j < Main.maxTilesY - 300 && j > Main.worldSurface)
            {
                if (WorldGen.genRand.NextBool(200))
                {
                    if (type == TileID.Stone)
                    {
                        if (Helper.HasNotAnySameOne(i, j, 60, 60, ModContent.TileType<油果植株>()))
                        {
                            if (Helper.CanPlaceOnIt(i, j + 2, 1, 2, false, true))
                            {
                                //Main.LocalPlayer.Center = new Vector2(i * 16, j * 16);//传送实验
                                WorldGen.Place1x2Top(i, j, (ushort)ModContent.TileType<油果植株>(), 0);
                            }
                        }
                    }
                    if (type == 384)//红木树叶
                    {
                        if (Helper.HasNotAnySameOne(i, j, 50, 50, ModContent.TileType<丛林鸟巢>()))
                        {
                            if (Helper.CanPlaceOnIt(i, j - 1, 2, 2, false))
                            {
                                WorldGen.Place2x2Style(i + 1, j - 1, (ushort)ModContent.TileType<丛林鸟巢>());
                            }
                        }
                    }
                }
            }
            else if (j < Main.worldSurface)
            {
                if (Main.raining)
                {
                    if (type == TileID.Grass)
                    {
                        if (WorldGen.genRand.NextBool(50))
                        {
                            if (Helper.HasNotAnySameOne(i, j, 40, 40, ModContent.TileType<白蘑木桩>()))
                            {
                                if (Helper.CanPlaceOnIt(i, j - 1, 2, 1))
                                {
                                    WorldGen.Place2x1(i, j - 1, (ushort)ModContent.TileType<白蘑木桩>(), 0);
                                }
                            }
                        }
                        if (WorldGen.genRand.NextBool(70))
                        {
                            if (Helper.HasNotAnySameOne(i, j, 50, 50, ModContent.TileType<白蘑树桩>()))
                            {
                                if (Helper.CanPlaceOnIt(i, j - 1, 2, 2))
                                {
                                    WorldGen.Place2x2Style(i + 1, j - 1, (ushort)ModContent.TileType<白蘑树桩>(), 0);//右下角为基准
                                }
                            }
                        }
                    }
                }
            }
        }
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (fail)//多次开采掉落
            {

            }
            else//一次性掉落
            {
                Player player = Main.LocalPlayer;
                Item item = player.HeldItem;
                Tile tile = Main.tile[i, j];
                bool herbdrop = false;
                int herbtype = 0;
                bool seeddrop = false;
                int seedtype = 0;
                if (item.type == ModContent.ItemType<丰收镰刀>())
                {
                    switch (type)
                    {
                        case TileID.Seaweed:
                            herbdrop = Main.rand.NextBool(4);
                            herbtype = ModContent.ItemType<海带>();
                            break;
                        case TileID.CorruptThorns:
                            herbdrop = Main.rand.NextBool(4);
                            herbtype = ModContent.ItemType<腐球果>();
                            break;
                        case TileID.SeaOats:
                            herbdrop = Main.rand.NextBool(2);
                            herbtype = ModContent.ItemType<海麦>();
                            seeddrop = Main.rand.NextBool(3);
                            seedtype = ModContent.ItemType<海燕麦种子>();
                            break;
                        case TileID.CrimsonThorns:
                            herbdrop = Main.rand.NextBool(4);
                            herbtype = ModContent.ItemType<血角>();
                            break;
                        case TileID.Plants:
                            if (tile.TileFrameX < 106 && j < Main.worldSurface * 0.45f)
                            {
                                herbdrop = Main.rand.NextBool(5);
                                herbtype = ModContent.ItemType<云锦果>();
                            }
                            break;
                    }
                }
                else
                {
                    switch (type)
                    {
                        case TileID.Seaweed:
                            herbdrop = Main.rand.NextBool(6);
                            herbtype = ModContent.ItemType<海带>();
                            break;
                        case TileID.CorruptThorns:
                            herbdrop = Main.rand.NextBool(6);
                            herbtype = ModContent.ItemType<腐球果>();
                            break;
                        case TileID.SeaOats:
                            herbdrop = Main.rand.NextBool(4);
                            herbtype = ModContent.ItemType<海麦>();
                            seeddrop = Main.rand.NextBool(5);
                            seedtype = ModContent.ItemType<海燕麦种子>();
                            break;
                        case TileID.CrimsonThorns:
                            herbdrop = Main.rand.NextBool(6);
                            herbtype = ModContent.ItemType<血角>();
                            break;
                        case TileID.Plants:
                            if (tile.TileFrameX < 106 && j < Main.worldSurface * 0.45f)
                            {
                                herbdrop = Main.rand.NextBool(8);
                                herbtype = ModContent.ItemType<云锦果>();
                            }
                            break;
                    }
                }
                if (herbdrop && herbtype > 0)
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, herbtype, 1);
                }
                if (seeddrop && seedtype > 0)
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, seedtype, 1);
                }
            }
        }
        public override bool CanDrop(int i, int j, int type)
        {
            if (type == TileID.Bamboo)
            {
                if (Main.tile[i, j].TileFrameX == 0)
                {
                    Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, ModContent.ItemType<竹笋>(), 1);
                    return false;
                }
            }
            return base.CanDrop(i, j, type);
        }
    }
}