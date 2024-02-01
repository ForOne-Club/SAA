using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 黑醋栗种子 : Seed
    {
        protected override int TileType => ModContent.TileType<黑醋栗>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BlackCurrant)
            .Register();
        }
    }
}
