namespace SAA.Content.Placeable
{
    public class 牛奶发酵桶 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("牛奶发酵桶");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 32;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.createTile = ModContent.TileType<Tiles.牛奶发酵桶>();
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Wood, 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}