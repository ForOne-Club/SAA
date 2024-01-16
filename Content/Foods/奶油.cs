using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 奶油 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("奶油");
            // Tooltip.SetDefault("\"简直是甜品爱好者的福音！\"");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(26, 24, 206, 18000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 2);
            recipe.AddTile(ModContent.TileType<牛奶发酵桶>());
            recipe.Register();
        }
    }
}