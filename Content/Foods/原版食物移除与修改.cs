using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 原版食物移除与修改 : ModSystem
    {
        public override void AddRecipes()
        {
            //烤棉花糖
            Recipe.Create(969)
            .AddIngredient(ItemID.Marshmallow, 1)
            .AddTile(ModContent.TileType<烤肉篝火>())
            .Register();
            //烤猫头鹰
            Recipe.Create(4031)
            .AddIngredient(ItemID.Owl, 1)
            .AddTile(ModContent.TileType<烤肉篝火>())
            .Register();
            //披萨
            Recipe.Create(4029, 4)
            .AddIngredient<海麦>(4)
            .AddIngredient<短爬兽排>()
            .AddIngredient<奶酪>()
            .AddTile(TileID.Furnaces)//熔炉
            .Register();
            //苹果派
            Recipe.Create(4011)
            .AddIngredient<海麦>(2)
            .AddIngredient<黄油>()
            .AddIngredient(ItemID.Apple)
            .AddTile(TileID.Furnaces)
            .Register();
            //鲜虾三明治
            Recipe.Create(4035)
            .AddIngredient<海麦>(2)
            .AddIngredient(ItemID.Shrimp)
            .AddTile(TileID.CookingPots)
            .Register();
            //盒装牛奶
            Recipe.Create(5041)
            .AddIngredient<牛奶>(4)
            .Register();
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
            //南瓜派
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.PumpkinPie), out IEnumerable<Recipe> recipe4))
            {
                foreach (Recipe rec in recipe4)
                {
                    rec.AddIngredient(ModContent.ItemType<海麦>());
                }
            }
            base.PostAddRecipes();
        }
    }
}