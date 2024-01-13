namespace SAA.Content.Foods
{
    public class 乌贼 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("乌贼");
            // Tooltip.SetDefault("眼疾手快！");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 42;
            Item.consumable = true;
            Item.maxStack = 30;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 0, 4, 0);
        }
        public override bool? UseItem(Player player)
        {
            Vector2 pos = player.Center + new Vector2(player.direction * Main.rand.Next(30, 40), Main.rand.Next(-10, 10));
            NPC.NewNPC(null, (int)pos.X, (int)pos.Y, NPCID.Squid);
            return true;
        }
    }
    internal class 乌贼物品获取 : GlobalNPC
    {
        public override void SetDefaults(NPC npc)
        {
            if (npc.type == NPCID.Squid)
            {
                npc.catchItem = (short)ModContent.ItemType<乌贼>();
            }
        }
    }
}