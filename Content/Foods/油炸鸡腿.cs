namespace SAA.Content.Foods
{
    public class 油炸鸡腿 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("很油很亮");
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;
            Item.SetFood(1, 18);
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Normal, false).ToItemRarity();
        }
        public override void AddRecipes() => this.Fried(ModContent.ItemType<生鸡腿>(), 2);
    }
}
