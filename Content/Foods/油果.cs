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
            Item.width = 24;
            Item.height = 34;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 40);
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Base, false).ToItemRarity();
        }
    }
}
