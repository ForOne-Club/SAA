namespace SAA.Content.Foods
{
    public class 油果 : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("小小的身体里蕴含大大的能量\n" +
                "这玩意在地下洞穴还挺常见"); */
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(24, 34, 1, 8);
        }
    }
}
