namespace SAA.Content.Foods
{
    public class 生翅根 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("哪里来的鸡？");
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(20, 20, 0, 8, true);
        }
    }
}
