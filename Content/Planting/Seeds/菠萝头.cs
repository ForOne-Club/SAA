using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 菠萝头 : Seed
    {
        protected override int Width => 16;
        protected override int Height => 14;
        protected override int TileType => ModContent.TileType<菠萝>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Pineapple)
            .Register();
        }
    }
}
