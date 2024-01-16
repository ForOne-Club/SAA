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
            Item.SetOriginFood(38, 30, 207, 48600);
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