namespace SAA.Content.Foods
{
    public class 油炸白蘑菇 : ModItem
    {
        public override void SetStaticDefaults()
        {

        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.SetFood(1, 6);
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Base, false).ToItemRarity();
        }
        public override void AddRecipes()
        {
            this.Fried(ModContent.ItemType<白蘑菇>(), 5);
        }
    }
}
