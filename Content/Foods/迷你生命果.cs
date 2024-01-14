namespace SAA.Content.Foods
{
    public class 迷你生命果 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 16;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 25, 0);
            Item.rare = ItemRarityID.Orange;
        }
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(1291);//生命果
            recipe.AddIngredient(Item.type, 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}