using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 柠檬种子 : Seed
    {
        protected override int TileType => ModContent.TileType<柠檬树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Lemon)
            .Register();
        }
    }
}
