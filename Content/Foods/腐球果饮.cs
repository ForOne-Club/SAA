namespace SAA.Content.Foods
{
    public class 腐球果饮 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("腐球果饮");
            // Tooltip.SetDefault("处理好的腐球果加上少许盐水，口味适中" + "\n会赋予你特殊的免疫效果");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(20, 30, 206, 10800, true);
        }
        public override void OnConsumeItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<Buffs.腐球免疫>(), 7200);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海水>(), 1);
            recipe.AddIngredient(ModContent.ItemType<腐球果>(), 2);
            recipe.Register();
        }
    }
}