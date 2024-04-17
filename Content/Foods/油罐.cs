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
            Item.SetFoodMaterials(24, 30, 1, 40);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<油果>(), 5)
                .AddTile(TileID.Bottles)
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<瓜子>(), 8)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}
