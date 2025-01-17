using SAA.Content.Foods;
using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Sys;

public class FoodLifeSystem : ModSystem
{
    public static int ChestCheckIndex = 0;
    public override void PostUpdateTime()
    {
        //if(Main.netMode == NetmodeID.SinglePlayer|| Main.netMode == NetmodeID.Server)
        if (HungerSetting.FoodSpoilt)
        {
            if (ChestCheckIndex >= 1000)
            {
                ChestCheckIndex = 0;
            }
            Chest chest = Main.chest[ChestCheckIndex];
            if (chest != null)
            {
                if (chest.item != null)//联机时本地的是null，打开箱子会从服务端读取，而且是分开跑的
                {
                    bool 冰箱 = false;
                    if (Main.tile[chest.x, chest.y].TileType == ModContent.TileType<冰箱>())
                    {
                        冰箱 = true;
                    }
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex] != null && chest.item[inventoryIndex].stack > 0)
                        {
                            if (chest.item[inventoryIndex].GetGlobalItem<HungerforItem>().ShelfLife > 0)
                            {
                                if (冰箱)
                                {
                                    if (Main.rand.NextBool(3)) chest.item[inventoryIndex].GetGlobalItem<HungerforItem>().ShelfLife--;
                                }
                                else
                                {
                                    chest.item[inventoryIndex].GetGlobalItem<HungerforItem>().ShelfLife--;
                                }
                            }
                            else if (chest.item[inventoryIndex].GetGlobalItem<HungerforItem>().ShelfLife == 0)
                            {
                                int stack = chest.item[inventoryIndex].stack;
                                chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<腐烂物>());
                                chest.item[inventoryIndex].stack = stack;
                            }
                        }
                    }
                }
            }
            ChestCheckIndex++;
        }
        base.PostUpdateTime();
    }
}
