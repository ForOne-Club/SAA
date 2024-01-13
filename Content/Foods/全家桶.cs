namespace SAA.Content.Foods
{
    public class 全家桶 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("疯狂星期四v我50");
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 46;
            Item.SetFood(2, 18 + 5 + 9);
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Normal, true).ToItemRarity();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<油炸翅尖>())
                .AddIngredient(ModContent.ItemType<油炸翅根>())
                .AddIngredient(ModContent.ItemType<油炸鸡腿>())
                .Register();
        }
    }
}
