namespace SAA.Content.Foods
{
    public class 黄油 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("黄油");
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(32, 26, 1, 20);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<酸奶>(), 5);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}