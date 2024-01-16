using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 奶酪 : CanHoldAndPlaceFood
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
            Item.SetOriginFood(46, 26, 206, 45000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 5);
            recipe.AddTile(ModContent.TileType<牛奶发酵桶>());
            recipe.Register();
        }
    }
}