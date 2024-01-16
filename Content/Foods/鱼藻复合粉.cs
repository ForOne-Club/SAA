namespace SAA.Content.Foods
{
    public class 鱼藻复合粉 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("鱼藻复合粉");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(38, 30, 206, 9000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ItemID.Sashimi);
            recipe.AddIngredient(ModContent.ItemType<海带>(), 2);
            recipe.Register();
        }
    }
}