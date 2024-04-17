namespace SAA.Content.Foods
{
    public class 油炸白蘑菇 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetFood(24, 22, 1, 8);
        }
        public override void AddRecipes()
        {
            this.Fried(ModContent.ItemType<白蘑菇>(), 5, 2);
            this.Fried(ModContent.ItemType<白蘑菇>(), 25, 10, true);
        }
    }
}
