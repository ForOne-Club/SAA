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
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.rare = ItemRarityID.White;
        }
    }
}