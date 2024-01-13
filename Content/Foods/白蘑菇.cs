namespace SAA.Content.Foods
{
    public class 白蘑菇 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("你能在树桩和木桩附近找到它们");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Base, false).ToItemRarity();
        }
    }
}
