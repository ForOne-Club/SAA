namespace SAA.Content.Foods
{
    public class 云锦面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("云锦面包");
            // Tooltip.SetDefault("酥脆的面包与处理完美的酸味相结合，适合所有人的美味" + "\n会赋予你羽落效果");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 3, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 50400;
        }
        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(BuffID.Featherfall, 10800);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦面包>(), 1);
            recipe.AddIngredient(ModContent.ItemType<云锦果>(), 4);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}