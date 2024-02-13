using SAA.Content.Foods;

namespace SAA.Content.Breeding.Items
{
    public class 海带缸 : PlaceItem
    {
        protected override int TileType => ModContent.TileType<Tiles.海带缸>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WaterBucket)
            .AddIngredient<海带>(2)
            .AddIngredient(ItemID.Glass, 10)
            .Register();
        }
    }
}
