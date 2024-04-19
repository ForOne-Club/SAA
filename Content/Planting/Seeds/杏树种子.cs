using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 杏树种子 : Seed
    {
        protected override int TileType => ModContent.TileType<杏树>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Apricot)
            .Register();
        }
    }
}
