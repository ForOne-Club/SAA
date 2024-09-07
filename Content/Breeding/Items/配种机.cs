namespace SAA.Content.Breeding.Items
{
    public class 配种机 : PlaceItem
    {
        protected override int Width => 30;
        protected override int Height => 16;
        protected override int TileType => ModContent.TileType<Tiles.配种机>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WaterBucket, 4)
            .AddIngredient(ItemID.Glass, 6)
            .AddRecipeGroup(RecipeGroupID.IronBar, 8)
            .Register();
        }
    }
}
