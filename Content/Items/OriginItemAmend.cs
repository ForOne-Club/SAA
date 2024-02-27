using Terraria;
using Terraria.ID;

namespace SAA.Content.Items
{
    public class OriginItemAmend : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(225)//丝绸
            .AddIngredient<棉线>(2)
            .AddTile(TileID.Loom)
            .Register();
            Recipe.Create(1991)//捕虫网
            .AddRecipeGroup(RecipeGroupID.IronBar)
            .AddIngredient(ItemID.Cobweb,10)
            .AddTile(TileID.Loom)
            .Register();
        }
        public override void PostAddRecipes()
        {
            //丝绸
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.Silk), out IEnumerable<Recipe> recipe5))
            {
                foreach (Recipe rec in recipe5)
                {
                    if (rec.RemoveIngredient(150))
                    {
                        rec.AddIngredient(ItemID.Cobweb, 50);
                    }
                }
            }
            base.PostAddRecipes();
        }
    }
}