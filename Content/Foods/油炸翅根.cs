namespace SAA.Content.Foods
{
    public class 油炸翅根 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("很油很亮");
        }
        public override void SetDefaults()
        {
            Item.SetFood(24, 22, 1, 12);
        }
        public override void AddRecipes()
        {
            this.Fried(ModContent.ItemType<生翅根>(), 4, 2);
            this.Fried(ModContent.ItemType<生翅根>(), 20, 10, true);
        }
    }
}
