using SAA.Content.Foods;
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
            }
            if (npc.type == NPCID.Crab)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<蟹棒>(), 5, 1, 1));
            }
        }
    }
}
