namespace SAA.Content.NPCs
{
    public class 奶蜗牛 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("奶蜗牛");
        }
        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 38;
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.noUseGraphic = true;
            Item.value = Item.sellPrice(0, 0, 2, 0);
        }
        public override bool? UseItem(Player player)
        {
            Vector2 pos = player.Center + new Vector2(player.direction * Main.rand.Next(30, 40), Main.rand.Next(-10, 10));
            NPC.NewNPC(null, (int)pos.X, (int)pos.Y, ModContent.NPCType<奶牛>());
            return true;
        }
    }
}