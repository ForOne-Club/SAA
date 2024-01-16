namespace SAA.Content.Foods
{
    public class 海水 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("海水");
            // Tooltip.SetDefault("用海生子将水中的盐份吸收后饮用，然后将海生子一同食用回复少量饱食度");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(20, 30, 26, 10800, true);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海带>(), 1);
            recipe.AddCondition(Condition.NearWater);
            recipe.Register();
        }
    }
}