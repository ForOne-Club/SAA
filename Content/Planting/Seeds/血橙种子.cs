using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 血橙种子 : Seed
    {
        protected override int TileType => ModContent.TileType<血橙树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.BloodOrange)
            .Register();
        }
    }
}
