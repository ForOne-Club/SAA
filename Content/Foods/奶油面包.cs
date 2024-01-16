namespace SAA.Content.Foods
{
    public class 奶油面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("奶油面包");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(38, 30, 206, 57600);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦面包>(), 1);
            recipe.AddIngredient(ModContent.ItemType<奶油>(), 1);
            recipe.Register();
        }
    }
}