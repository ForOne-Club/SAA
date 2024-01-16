namespace SAA.Content.Foods
{
    public class 蜂蜜面包 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetOriginFood(38, 32, 206, 43200);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦面包>(), 1);
            recipe.AddIngredient(ItemID.BottledHoney, 1);
            recipe.Register();
        }
    }
}