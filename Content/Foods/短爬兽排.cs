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
            Item.SetFoodMaterials(26, 28, 0, 17, true);
        }
    }
}