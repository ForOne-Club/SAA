namespace SAA.Content.Foods
{
    public class 海麦面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("海麦面包");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 4, 50);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 72000;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦>(), 4);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}