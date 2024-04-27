namespace SAA.Content.Items
{
    public class 棉线 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.rare = ItemRarityID.White;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<棉花>(5)
            .AddTile(TileID.Loom)//织布机
            .Register();
        }
    }
}