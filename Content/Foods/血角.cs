namespace SAA.Content.Foods
{
    public class 血角 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("血角");
            // Tooltip.SetDefault("据说是血腥之地荆棘的果实，辣味十足");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 15);
            Item.rare = ItemRarityID.White;
        }
    }
}