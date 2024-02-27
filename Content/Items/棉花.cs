namespace SAA.Content.Items
{
    public class 棉花 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 5);
            Item.rare = ItemRarityID.White;
        }
    }
}