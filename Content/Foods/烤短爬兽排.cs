using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 烤短爬兽排 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烤短爬兽排");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(26, 28, 26, 64800);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<短爬兽排>(), 1);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}