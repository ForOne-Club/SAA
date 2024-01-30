namespace SAA.Content.Breeding.Items
{
    public class 野鸭笼 : PlaceItem
    {
        protected override int TileType => ModContent.TileType<Tiles.野鸭笼>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.MallardDuck)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .Register();
        }
    }
}
