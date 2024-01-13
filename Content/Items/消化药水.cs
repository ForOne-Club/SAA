namespace SAA.Content.Items
{
    public class 消化药水 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("消化药水");
            // Tooltip.SetDefault("帮助消化" + "\n\"这玩意真的不是混合果汁？\"");
        }
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 30;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 30, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = ModContent.BuffType<Buffs.酸甜可口>();
            Item.buffTime = 10800;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BottledWater, 3);
            recipe.AddIngredient(ItemID.Lemon);
            recipe.AddIngredient(ItemID.Grapefruit);
            recipe.AddIngredient(ItemID.BloodOrange);
            recipe.AddTile(TileID.Bottles);
            recipe.ReplaceResult(this, 3);
            recipe.Register();
        }
    }
}