namespace SAA.Content.Foods
{
    public class 海水 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("海水");
            // Tooltip.SetDefault("用海生子将水中的盐份吸收后饮用，然后将海生子一同食用回复少量饱食度");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 30;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 9000;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海带>(), 1);
            recipe.AddCondition(Condition.NearWater);
            recipe.Register();
        }
    }
}