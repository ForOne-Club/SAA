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
            Item.SetOriginFood(18, 28, 206, 5400);
        }
    }
}