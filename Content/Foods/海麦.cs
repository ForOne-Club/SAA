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
            Item.width = 30;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 1);
            Item.rare = ItemRarityID.White;
        }
    }
}