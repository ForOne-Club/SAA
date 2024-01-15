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
            Item.width = 20;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 3, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 48000;
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(117, 3600);
            return base.UseItem(player);
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