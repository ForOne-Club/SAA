namespace SAA.Content.Foods
{
    public class 蜂蜜面包 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 32;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 2, 50);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 43200;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦面包>(), 1);
            recipe.AddIngredient(ItemID.BottledHoney, 1);
            recipe.Register();
        }
    }
}