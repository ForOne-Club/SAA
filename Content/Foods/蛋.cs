namespace SAA.Content.Foods
{
    public class 蛋 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 5);
            Item.rare = ItemRarityID.Blue;
        }
    }
}