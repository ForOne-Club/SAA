using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 辣椒种子 : Seed
    {
        protected override int TileType => ModContent.TileType<辣椒>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(5277)
            .Register();
        }
    }
}
