namespace SAA.Content.Foods
{
    public class 奶油吐司面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("奶油吐司面包");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 13, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 207;
            Item.buffTime = 48600;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<吐司面包>(), 1);
            recipe.AddIngredient(ModContent.ItemType<奶油>(), 1);
            recipe.Register();
        }
    }
}