namespace SAA.Content.Foods
{
    public abstract class 水果面包 : ModItem
    {
        protected virtual int FruitType => 1;
        protected virtual int BuffTime => 46800;
        public override void SetDefaults()
        {
            Item.SetOriginFood(38, 32, 206, BuffTime);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦面包>(), 1);
            recipe.AddIngredient(FruitType, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
    public class 菠萝面包 : 水果面包
    {
        protected override int FruitType => ItemID.Pineapple;
    }
    public class 黑醋栗面包 : 水果面包
    {
        protected override int FruitType => ItemID.BlackCurrant;
    }
    public class 接骨木果面包 : 水果面包
    {
        protected override int FruitType => ItemID.Elderberry;
    }
    public class 李子面包 : 水果面包
    {
        protected override int FruitType => ItemID.Plum;
    }
    public class 芒果面包 : 水果面包
    {
        protected override int FruitType => ItemID.Mango;
    }
    public class 柠檬面包 : 水果面包
    {
        protected override int FruitType => ItemID.Lemon;
    }
    public class 苹果面包 : 水果面包
    {
        protected override int FruitType => ItemID.Apple;
    }
    public class 葡萄柚面包 : 水果面包
    {
        protected override int FruitType => ItemID.Grapefruit;
    }
    public class 桃子面包 : 水果面包
    {
        protected override int FruitType => ItemID.Peach;
    }
    public class 杏子面包 : 水果面包
    {
        protected override int FruitType => ItemID.Apricot;
    }
    public class 血橙面包 : 水果面包
    {
        protected override int FruitType => ItemID.BloodOrange;
    }
    public class 杨桃面包 : 水果面包
    {
        protected override int FruitType => ItemID.Starfruit;
    }
    public class 樱桃面包 : 水果面包
    {
        protected override int FruitType => ItemID.Cherry;
    }
    public class 石榴面包 : 水果面包
    {
        protected override int FruitType => ItemID.Pomegranate;
    }
    public class 蓝莓面包 : 水果面包
    {
        protected override int FruitType => ModContent.ItemType<蓝莓>();
    }
}