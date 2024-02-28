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
            .AddIngredient(ItemID.Cobweb,10)
            .AddRecipeGroup(RecipeGroupID.IronBar)
            .AddTile(TileID.Loom)
            .Register();
            Recipe.Create(254,2)//黑线
            .AddIngredient<棉线>(2)
            .AddIngredient(ItemID.BlackDye)
            .AddTile(TileID.DyeVat)
            .Register();
        }
        public override void PostAddRecipes()
        {
            //丝绸
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.Silk), out IEnumerable<Recipe> recipe1))
            {
                foreach (Recipe rec in recipe1)
                {
                    if (rec.RemoveIngredient(150))
                    {
                        rec.AddIngredient(ItemID.Cobweb, 50);
                    }
                }
            }
            //绿线
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.GreenThread), out IEnumerable<Recipe> recipe2))
            {
                foreach (Recipe rec in recipe2)
                {
                    if (rec.RemoveIngredient(195))
                    {
                        rec.AddIngredient<棉线>(2);
                        rec.AddIngredient(ItemID.GreenDye);
                        rec.RemoveTile(TileID.Loom);
                        rec.AddTile(TileID.DyeVat);
                        rec.ReplaceResult(ItemID.GreenThread, 2);
                    }
                }
            }
            //细绳
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.WhiteString), out IEnumerable<Recipe> recipe3))
            {
                foreach (Recipe rec in recipe3)
                {
                    if (rec.RemoveIngredient(150))
                    {
                        rec.AddIngredient<棉线>(5);
                    }
                }
            }
            base.PostAddRecipes();
        }
    }
}