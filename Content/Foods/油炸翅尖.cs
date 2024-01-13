namespace SAA.Content.Foods
{
    public class 油炸翅尖 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("很油很亮");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.SetFood(1, 5);
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Normal, false).ToItemRarity();
        }
        public override void AddRecipes() => this.Fried(ModContent.ItemType<生翅尖>(), 5);
    }
}
