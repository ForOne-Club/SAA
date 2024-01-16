namespace SAA.Content.Foods
{
    public class 油炸翅尖 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetFood(26, 22, 1, 5);
        }
        public override void AddRecipes() => this.Fried(ModContent.ItemType<生翅尖>(), 5);
    }
}
