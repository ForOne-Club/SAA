namespace SAA.Content.Foods
{
    public class 云锦果 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("云锦果");
            // Tooltip.SetDefault("生长于空岛的水果，绵软无比");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 999;
            Item.value = Item.sellPrice(0, 0, 0, 10);
            Item.rare = ItemRarityID.White;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 3600;
        }
    }
}