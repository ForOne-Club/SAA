namespace SAA.Content.Foods
{
    public class 烤橡实 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetOriginFood(20, 20, 26, 7200);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Acorn);
            recipe.AddTile(TileID.Campfire);
            recipe.Register();
        }
    }
}