namespace SAA.Content.Foods
{
    public class 海麦 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("海麦");
            // Tooltip.SetDefault("长在海边的麦子，泰拉瑞亚里真是什么都有呢");
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(30, 16, 0, 5);
        }
    }
}