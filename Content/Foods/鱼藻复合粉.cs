namespace SAA.Content.Foods
{
    public class 鱼藻复合粉 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("鱼藻复合粉");
        }
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 30;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 5, 0);
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
            Recipe recipe = CreateRecipe(4);
            recipe.AddIngredient(ItemID.Sashimi);
            recipe.AddIngredient(ModContent.ItemType<海带>(), 2);
            recipe.Register();
        }
    }
}