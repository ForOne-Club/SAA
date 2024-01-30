namespace SAA.Content.Breeding.Items
{
    public class 鸭笼 : PlaceItem
    {
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Duck)
            .AddRecipeGroup(RecipeGroupID.IronBar, 5)
            .Register();
        }
    }
}
