using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 碳烤蟹棒 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetOriginFood(40, 28, 26, 36000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<蟹棒>(), 1);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}