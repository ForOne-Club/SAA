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
            Item.buffTime = 72000;
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(117, 7200);
            return base.UseItem(player);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海带>(), 1);
            recipe.AddIngredient(ItemID.Goldfish, 1);
            recipe.AddIngredient(ModContent.ItemType<血角>(), 2);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}