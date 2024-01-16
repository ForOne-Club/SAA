namespace SAA.Content.Foods
{
    public class 吐司面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("吐司面包");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(38, 30, 206, 72000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦>(), 8);
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 2);
            recipe.AddIngredient(ModContent.ItemType<黄油>(), 1);
            recipe.AddTile(TileID.Furnaces);
            recipe.ReplaceResult(this, 2);
            recipe.Register();
        }
    }
}