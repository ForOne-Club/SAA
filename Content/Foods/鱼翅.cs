namespace SAA.Content.Foods
{
    public class 鱼翅 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("鱼翅");
            // Tooltip.SetDefault("\"没有买卖就没有杀害\"");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(22, 16, 26, 36000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SharkFin, 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}