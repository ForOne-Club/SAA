namespace SAA.Content.Foods
{
    public class 海麦面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("海麦面包");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(38, 30, 26, 72000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦>(), 4);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}