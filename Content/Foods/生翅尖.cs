namespace SAA.Content.Foods
{
    public class 生翅尖 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("哪里来的鸡？");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Normal, false).ToItemRarity();
        }
    }
}
