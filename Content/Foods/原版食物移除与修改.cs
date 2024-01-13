using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 原版食物移除与修改 : ModSystem
    {
        public override void AddRecipes()
        {
            //烤棉花糖
            Recipe recipe0 = Recipe.Create(969);
            recipe0.AddIngredient(ItemID.Marshmallow, 1);
            recipe0.AddTile(ModContent.TileType<烤肉篝火>());
            recipe0.Register();
            //烤猫头鹰
            Recipe recipe = Recipe.Create(4031);
            recipe.AddIngredient(ItemID.Owl, 1);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
            base.AddRecipes();
        }
        public override void PostAddRecipes()
        {
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.RoastedDuck && r.requiredTile.Contains(TileID.CookingPots)), out IEnumerable<Recipe> recipe1))
            {
                foreach (Recipe rec in recipe1)
                {
                    rec.requiredTile.RemoveAll(new Predicate<int>((tile) => tile == TileID.CookingPots));
                    rec.requiredTile.Add(ModContent.TileType<烤肉篝火>());
                }
            }
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.RoastedBird && r.requiredTile.Contains(TileID.CookingPots)), out IEnumerable<Recipe> recipe2))
            {
                foreach (Recipe rec in recipe2)
                {
                    rec.requiredTile.RemoveAll(new Predicate<int>((tile) => tile == TileID.CookingPots));
                    rec.requiredTile.Add(ModContent.TileType<烤肉篝火>());
                }
            }
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.GrilledSquirrel && r.requiredTile.Contains(TileID.CookingPots)), out IEnumerable<Recipe> recipe3))
            {
                foreach (Recipe rec in recipe3)
                {
                    rec.requiredTile.RemoveAll(new Predicate<int>((tile) => tile == TileID.CookingPots));
                    rec.requiredTile.Add(ModContent.TileType<烤肉篝火>());
                }
            }
            base.PostAddRecipes();
        }
    }
}