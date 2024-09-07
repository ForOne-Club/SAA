using Terraria.ID;

namespace SAA.Content.Breeding.Items
{
    public class 䴙䴘笼 : PlaceItem
    {
        protected override int TileType => ModContent.TileType<Tiles.䴙䴘笼>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Grebe)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .Register();
        }
    }
}
