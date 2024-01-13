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
            Item.width = 20;
            Item.height = 30;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 8, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 14400;
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<Buffs.腐球免疫>(), 7200);
            return base.UseItem(player);
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