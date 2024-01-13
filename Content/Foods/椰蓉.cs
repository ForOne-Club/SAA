namespace SAA.Content.Foods
{
    public class 椰蓉 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("椰蓉");
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 7200;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coconut, 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.ReplaceResult(this, 2);
            recipe.Register();
        }
    }
}