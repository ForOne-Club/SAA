namespace SAA.Content.Foods
{
    public class 油炸鸡腿 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetFood(28, 26, 1, 18);
        }
        public override void AddRecipes() => this.Fried(ModContent.ItemType<生鸡腿>(), 2);
    }
}
