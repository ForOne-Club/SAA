using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 樱桃种子 : Seed
    {
        protected override int TileType => ModContent.TileType<樱桃树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(4286)
            .Register();
        }
    }
}
