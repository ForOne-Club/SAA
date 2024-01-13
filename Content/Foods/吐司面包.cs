namespace SAA.Content.Foods
{
    public class 吐司面包 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("吐司面包");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 30;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 9, 0);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 72000;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦>(), 8);
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 2);
            recipe.AddIngredient(ModContent.ItemType<黄油>(), 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.ReplaceResult(this, 2);
            recipe.Register();
        }
    }
}