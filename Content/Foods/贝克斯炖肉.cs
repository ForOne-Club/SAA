namespace SAA.Content.Foods
{
    public class 贝克斯炖肉 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("贝克斯炖肉");
            // Tooltip.SetDefault("第一只被发现的短爬兽被命名为贝克斯，此菜因此得名" + "\n会赋予你短暂的怒气效果");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(20, 30, 206, 48000, true);
        }
        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(117, 3600);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<短爬兽排>(), 1);
            recipe.AddIngredient(ModContent.ItemType<血角>(), 2);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}