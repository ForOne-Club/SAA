using SAA.Content.Accessory;

namespace SAA.Content.Sys
{
    public class ChestItemPut : ModSystem
    {
        /// <summary>
        /// 在箱子里放置物品
        /// </summary>
        /// <param name="itemtype">物品ID</param>
        /// <param name="frame">帧数(从左往右数第几个-1)</param>
        /// <param name="probability">放置概率0到100</param>
        /// <param name="itemstack">物品数量</param>
        /// <param name="replaceitemtype">要替换的物品ID</param>
        /// <param name="boxtype">物块ID</param>
        private static void AddItemInBox(int itemtype, int frame, int probability, int itemstack = 1, int replaceitemtype = -1, int boxtype = TileID.Containers)
        {
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].TileType == boxtype)
                {
                    if (Main.tile[chest.x, chest.y].TileFrameX == frame * 36 && Main.rand.Next(100) < probability)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            int type = (replaceitemtype == -1) ? ItemID.None : replaceitemtype;
                            if (chest.item[inventoryIndex].type == type)
                            {
                                chest.item[inventoryIndex].SetDefaults(itemtype);
                                chest.item[inventoryIndex].stack = itemstack;
                                break;
                            }
                        }
                    }
                }
            }
        }
        public override void PostWorldGen()
        {
            AddItemInBox(ModContent.ItemType<求生手册>(), 4, 50, 1, 5007, 234);
        }
    }
}