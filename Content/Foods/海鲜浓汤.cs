namespace SAA.Content.Foods
{
    public class 海鲜浓汤 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("海鲜浓汤");
            // Tooltip.SetDefault("会让你饱腹感十足");
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 44;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Orange;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 207;
            Item.buffTime = 72000;
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<Buffs.饱腹>(), 7200);
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<鱼藻复合粉>());
            recipe.AddIngredient(ItemID.Shrimp);
            recipe.AddIngredient(ItemID.Tuna, 2);
            recipe.AddIngredient(ItemID.BottledWater, 2);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<鱼藻复合粉>());
            recipe2.AddIngredient(ItemID.RockLobster);
            recipe2.AddIngredient(ItemID.Tuna, 2);
            recipe2.AddIngredient(ItemID.BottledWater, 2);
            recipe2.AddTile(TileID.CookingPots);
            recipe2.Register();
        }
    }
}