namespace SAA.Content.Foods
{
    public class 绿茶 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(18, 20, 1, 6);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<东方树叶>());
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}
