using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 桃核 : Seed
    {
        protected override int Width => 18;
        protected override int TileType => ModContent.TileType<桃树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Peach)
            .Register();
        }
    }
}
