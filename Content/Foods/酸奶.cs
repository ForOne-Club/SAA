using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 酸奶 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("酸奶");
            // Tooltip.SetDefault("\"放心喝，它不会帮助消化\"");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(20, 22, 206, 9000, true);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 1);
            recipe.AddTile(ModContent.TileType<牛奶发酵桶>());
            recipe.Register();
        }
    }
}