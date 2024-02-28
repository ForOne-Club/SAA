using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 烤小肉 : CanHoldAndPlaceFood
    {
        protected override void SetFoodDust()
        {
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[2] {
                Color.Yellow,
                Color.Orange,
            };
        }
        public override void SetDefaults()
        {
            Item.SetFood(28, 14, 0, 7);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<小肉>();
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}