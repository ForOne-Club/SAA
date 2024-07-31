using SAA.Content.Placeable.Tiles;
using SAA.Content.Sys;
using Terraria.ID;

namespace SAA.Content.Foods
{
    public class OriginFoodAmend : ModSystem
    {
        public override void AddRecipes()
        {
            //烤棉花糖
            Recipe.Create(969)
            .AddIngredient(ItemID.Marshmallow, 1)
            .AddTile(ModContent.TileType<烤肉篝火>())
            .Register();
            //烤猫头鹰, 不吃了, 尊重原版(猫头鹰、鹦鹉不可食用)
            //Recipe.Create(4031)
            //.AddIngredient(ItemID.Owl, 1)
            //.AddTile(ModContent.TileType<烤肉篝火>())
            //.Register();
            //披萨
            Recipe.Create(4029, 3)
            .AddIngredient<海麦>(4)
            .AddIngredient<短爬兽排>()
            .AddIngredient<奶酪>()
            .AddTile(TileID.Furnaces)//熔炉
            .Register();
            //苹果派
            Recipe.Create(4011)
            .AddIngredient<海麦>(8)
            .AddIngredient<黄油>()
            .AddIngredient(ItemID.Apple)
            .AddTile(TileID.Furnaces)
            .Register();
            //鲜虾三明治
            Recipe.Create(4035)
            .AddIngredient<油果>()
            .AddIngredient(ItemID.Shrimp)
            .AddIngredient<海麦>(2)
            .AddTile(TileID.CookingPots)
            .Register();
            Recipe.Create(4035, 5)
            .AddIngredient<油罐>()
            .AddIngredient(ItemID.Shrimp, 5)
            .AddIngredient<海麦>(10)
            .AddTile(TileID.CookingPots)
            .Register();
            //盒装牛奶
            Recipe.Create(5041)
            .AddIngredient<牛奶>(4)
            .Register();
            //香蕉船
            Recipe.Create(4012)
            .AddIngredient<酸奶>()
            .AddIngredient(ItemID.Banana)
            .Register();
            //煎蛋
            Recipe.Create(4020)
            .AddIngredient<油果>()
            .AddIngredient<蛋>(2)
            .AddTile(TileID.CookingPots)
            .Register();
            Recipe.Create(4020, 5)
            .AddIngredient<油罐>()
            .AddIngredient<蛋>(10)
            .AddTile(TileID.CookingPots)
            .Register();
            //冰淇淋
            Recipe.Create(4026)
            .AddRecipeGroup(RecipeGroupID.Fruit)
            .AddIngredient<牛奶>()
            .AddIngredient<蛋>()
            .AddTile(ModContent.TileType<冰箱>())
            .Register();
            //奶昔
            Recipe.Create(4027)
            .AddIngredient<奶油>()
            .AddIngredient<酸奶>()
            .AddIngredient<牛奶>(2)
            .AddRecipeGroup(RecipeGroupID.Fruit, 2)
            .AddTile(ModContent.TileType<冰箱>())
            .Register();
        }
        public override void AddRecipeGroups()
        {
            //直接修改原版合成组
            RecipeGroup rec = new RecipeGroup(() => Lang.misc[37].Value + " " + Language.GetTextValue("Misc.Fruit"), 4009, 4282, 4283, 4284, 4285, 4286, 4287, 4288, 4289, 4290, 4291, 4292, 4293, 4294, 4295, 4296, 4297, 5277, 5278, ModContent.ItemType<蓝莓>());
            RecipeGroupID.Fruit = RecipeGroup.RegisterGroup("Fruit", rec);
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
            //冰冻香蕉
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.createItem.type == ItemID.BananaDaiquiri), out IEnumerable<Recipe> recipe5))
            {
                foreach (Recipe rec in recipe5)
                {
                    rec.requiredTile.Clear();
                    rec.requiredTile.Add(ModContent.TileType<冰箱>());
                }
            }
            SetCookRecipe();//添加烹饪合成
            base.PostAddRecipes();
        }
        private static void SetCookRecipe()
        {
            if (RecipeSupport.TryFindRecipes(new Predicate<Recipe>((r) => r.requiredTile.Contains(TileID.CookingPots)), out IEnumerable<Recipe> recipe))
            {
                foreach (Recipe rec in recipe)
                {
                    if (rec.Conditions.Count == 0)
                    {
                        if (rec.requiredItem.Count < 7 && rec.requiredItem.Count > 0)
                        {
                            rec.AddCondition(Language.GetText("Mods.SAA.Cook"), () => false);//锁死配方并告知使用烹饪锅烹饪

                            List<Point> require = new();
                            List<Point> requiregroup = new();
                            for (int i = 0; i < rec.requiredItem.Count; i++)
                            {
                                bool add = false;
                                for (int j = 0; j < rec.acceptedGroups.Count; j++)
                                {
                                    if (RecipeGroup.recipeGroups[rec.acceptedGroups[j]].IconicItemId == rec.requiredItem[i].type)//认定这是一个材料组
                                    {
                                        requiregroup.Add(new Point(rec.acceptedGroups[j], rec.requiredItem[i].stack));
                                        add = true;
                                        break;
                                    }
                                }
                                if (!add)
                                {
                                    require.Add(new Point(rec.requiredItem[i].type, rec.requiredItem[i].stack));
                                }
                            }
                            CookSystem.PotCookRecipe.Add(new RecipeStore(require, requiregroup, new Point(rec.createItem.type, rec.createItem.stack)));
                        }
                    }
                }
            }
        }
    }
}