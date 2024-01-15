namespace SAA.Content.Foods
{
    public class 鱼翅 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("鱼翅");
            // Tooltip.SetDefault("\"没有买卖就没有杀害\"");
        }
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 40);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.9f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 36000;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.SharkFin, 1);
            recipe.AddTile(TileID.CookingPots);
            recipe.Register();
        }
    }
}