using SAA.Content.Foods;
using SAA.Content.Items;
using Terraria.GameContent.ItemDropRules;

namespace SAA.Content.Sys
{
    public class HungerforNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Vulture)
            {
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生翅尖>(), 0.17f));
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生翅根>(), 0.12f));
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生鸡腿>(), 0.08f));
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<蛋>(), 0.02f));
            }
            if (npc.type == NPCID.Crab)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<蟹棒>(), 5, 1, 1));
            }
        }
        public override void ModifyShop(NPCShop shop)
        {
            //if (shop.Name == "Shop")
            //{
            //    if (shop.NpcType == NPCID.Merchant)
            //    {
            //        shop.Add(ModContent.ItemType<锄头>());
            //        if (shop.TryGetEntry(ModContent.ItemType<锄头>(), out NPCShop.Entry entry))
            //        {
            //            _ = entry.Disable();//禁止售卖
            //        }
            //    }
            //}
        }
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type == NPCID.Merchant)
            {
                for (int i = items.Length - 1; i > 16; i--)
                {
                    if (items[i - 1] != null) items[i] = items[i - 1];
                }
                items[16] = new Item(ModContent.ItemType<锄头>());
            }
            base.ModifyActiveShop(npc, shopName, items);
        }
    }
}
