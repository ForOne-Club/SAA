using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 芒果种子 : Seed
    {
        protected override int Height => 20;
        protected override int TileType => ModContent.TileType<芒果树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Mango)
            .Register();
        }
    }
}
