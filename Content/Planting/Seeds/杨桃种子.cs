using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 杨桃种子 : Seed
    {
        protected override int TileType => ModContent.TileType<杨桃树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(4297)
            .Register();
        }
    }
}
