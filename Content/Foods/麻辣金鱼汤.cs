namespace SAA.Content.Foods
{
    public class 麻辣金鱼汤 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("麻辣金鱼汤");
            // Tooltip.SetDefault("鲜香的鱼汤与麻辣的血角交相辉映，再加上海生子进行调味，着实让人上瘾" + "\n会赋予你怒气效果");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(20, 30, 206, 72000, true);
        }
        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(117, 7200);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<海带>(), 1)
            .AddIngredient(ItemID.Goldfish, 1)
            .AddIngredient(ModContent.ItemType<血角>(), 2)
            .AddTile(TileID.CookingPots)
            .Register();
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<海带>(), 1)
            .AddIngredient(ItemID.Goldfish, 1)
            .AddIngredient(5277)//辣椒
            .AddTile(TileID.CookingPots)
            .Register();
        }
    }
}