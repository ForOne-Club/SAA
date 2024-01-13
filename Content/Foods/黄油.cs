namespace SAA.Content.Foods
{
    public class 黄油 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("黄油");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Green;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<酸奶>(), 5);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}