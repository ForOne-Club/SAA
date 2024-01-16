using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 烤蚜虫 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烤蚜虫");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(40, 22, 26, 72000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Buggy, 2);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}