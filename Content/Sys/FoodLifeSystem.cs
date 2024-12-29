using SAA.Content.Foods;

namespace SAA.Content.Sys;

public class FoodLifeSystem : ModSystem
{
    public static int ChestCheckIndex = 0;
    public override void PostUpdateTime()
    {
        //if(Main.netMode == NetmodeID.SinglePlayer|| Main.netMode == NetmodeID.Server)
        if(HungerSetting.FoodSpoilt)
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
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex] != null && chest.item[inventoryIndex].stack > 0)
                        {
                            if (chest.item[inventoryIndex].GetGlobalItem<HungerforItem>().ShelfLife > 0)
                            {
                                chest.item[inventoryIndex].GetGlobalItem<HungerforItem>().ShelfLife--;
                            }
                            else if (chest.item[inventoryIndex].GetGlobalItem<HungerforItem>().ShelfLife == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<腐烂物>());
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
