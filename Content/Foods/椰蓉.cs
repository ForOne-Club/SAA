namespace SAA.Content.Foods
{
    public class 椰蓉 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("椰蓉");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(30, 16, 26, 7200);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coconut, 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.ReplaceResult(this, 2);
            recipe.Register();
        }
    }
}