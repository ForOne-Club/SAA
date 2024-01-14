namespace SAA.Content.Foods
{
    public class 腐球果 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("腐球果");
            // Tooltip.SetDefault("据说是腐化之地荆棘的果实，可食用，味甜且腻");
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 15);
            Item.rare = ItemRarityID.White;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.8f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 5400;
        }
    }
}