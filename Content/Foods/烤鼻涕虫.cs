using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 烤鼻涕虫 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烤鼻涕虫");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(40, 22, 26, 43200);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sluggy, 2);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}