using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 李子种子 : Seed
    {
        protected override int TileType => ModContent.TileType<李子树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Plum)
            .Register();
        }
    }
}
