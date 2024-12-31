namespace SAA.Content.Foods
{
    public class 海带 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("海生子");
            // Tooltip.SetDefault("与长在深海中的海草共生，它可以很好的吸取海水中的盐分");
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(24, 32, 0, 3, true);
        }
    }
}