namespace SAA.Content.Foods
{
    public abstract class 水果面包 : ModItem
    {
        protected virtual int FruitType => 1;
        protected virtual int BuffTime => 46800;
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 32;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = BuffTime;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<海麦面包>(), 1);
            recipe.AddIngredient(FruitType, 1);
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
    public class 蓝莓面包 : 水果面包
    {
        protected override int FruitType => ModContent.ItemType<蓝莓>();
    }
}