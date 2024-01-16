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
            Item.SetOriginFood(26, 24, 206, 3600);
        }
    }
}