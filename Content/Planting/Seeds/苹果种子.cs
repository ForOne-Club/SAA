using SAA.Content.Planting.Tiles.Plants;
using Terraria.ID;

namespace SAA.Content.Planting.Seeds
{
    public class 苹果种子 : Seed
    {
        protected override int TileType => ModContent.TileType<苹果树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Apple)
            .Register();
        }
    }
}
