namespace SAA.Content.Foods
{
    public class 香酥鸡腿堡 : CanHoldAndPlaceFood
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
            Item.SetFood(28, 26, 2, 36);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(2);
            recipe.AddIngredient<海麦面包>();
            recipe.AddIngredient<油炸鸡腿>(2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}