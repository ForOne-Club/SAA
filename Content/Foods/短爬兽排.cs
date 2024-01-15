namespace SAA.Content.Foods
{
    public class 短爬兽排 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("短爬兽排");
            // Tooltip.SetDefault("短爬兽的背肋肉，因为经常的拉伸锻炼而导致口感紧致");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
        }
    }
}