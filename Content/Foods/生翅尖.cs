namespace SAA.Content.Foods
{
    public class 生翅尖 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("哪里来的鸡？");
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(24, 16, 0, 4, true);
        }
    }
}
