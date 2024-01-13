using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 油罐 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("装满油的罐子");
            // Tooltip.SetDefault("油炸的必备材料\n" + "压缩油果，小子");
        }
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 0, 2);
            Item.rare = new Rarity(Phase.BeforeFleshWall, ItemType.Another, GetDiff.Normal, false).ToItemRarity();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<油果>(), 5)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
