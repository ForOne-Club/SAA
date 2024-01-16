namespace SAA.Content.Foods
{
    public class 生鸡腿 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("哪里来的鸡？");
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(22, 24, 0, 16);
        }
    }
}
