using Terraria.GameContent.ItemDropRules;
namespace SAA.Content.Foods
{
    public class 蟹棒 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("蟹棒");
            // Tooltip.SetDefault("你确定要生吃这玩意？");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.rare = ItemRarityID.Blue;
            Item.scale = 0.75f;
        }
    }
    public class XBForNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Crab)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<蟹棒>(), 5, 1, 1));
            }
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }
}