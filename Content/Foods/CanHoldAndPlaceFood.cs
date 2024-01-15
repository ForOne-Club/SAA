namespace SAA.Content.Foods
{
    public abstract class CanHoldAndPlaceFood : ModItem
    {
        public override void SetStaticDefaults()
        {
            //这是为了显示正确的框架在库存
            // MaxValue参数表示动画速度，我们希望它停留在第一帧
            //将其设置为最大值将导致到达下一帧需要414天
            //没有人会让游戏打开那么长时间，所以这是好的
            //第二个参数是帧数，为3
            //第一帧是库存纹理，第二帧是持有纹理，
            //第三帧是放置的纹理
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));
            SetFoodDust();
            ItemID.Sets.IsFood[Type] = true; //This allows it to be placed on a plate and held correctly
        }
        protected virtual void SetFoodDust()
        {
            //这允许你改变你吃的时候产生的面包屑的颜色。
            //数字为RGB (Red, Green, Blue)值，取值范围为0 ~ 255。
            //大多数食物的面包屑有3种颜色，但你可以根据自己的喜好多用或少用。
            //根据你是制作固体食物还是液体食物，切换出FoodParticleColors
            //使用DrinkParticleColors。不同之处在于食物颗粒是向外飞的
            //而饮料颗粒则是直接向下掉落，并且是微透明的
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[3] {
                new Color(249, 230, 136),
                new Color(152, 93, 95),
                new Color(174, 192, 192)
            };
        }
    }
}