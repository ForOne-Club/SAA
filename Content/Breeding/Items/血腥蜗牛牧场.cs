using SAA.Content.NPCs;

namespace SAA.Content.Breeding.Items
{
    public class 血腥蜗牛牧场 : PlaceItem
    {
        protected override int Width => 30;
        protected override int Height => 16;
        protected override int TileType => ModContent.TileType<Tiles.血腥蜗牛牧场>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<血腥奶蜗牛>()
            .AddIngredient(ItemID.WaterBucket, 2)
            .AddRecipeGroup(RecipeGroupID.IronBar, 6)
            .Register();
        }
    }
}
