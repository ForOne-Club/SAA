namespace SAA.Content.Foods
{
    public class 杨桃面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("杨桃面包");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 32;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 59400;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦面包>(), 1);
            recipe.AddIngredient(ItemID.Starfruit, 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}