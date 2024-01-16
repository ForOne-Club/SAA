using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 火龙果种子 : Seed
    {
        protected override int TileType => ModContent.TileType<火龙果>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Dragonfruit)
            .Register();
        }
    }
}
