namespace SAA.Content.Sys
{
    public class HungerSetting : ModSystem
    {
        /// <summary>
        /// 有没有加载ForOne模组
        /// </summary>
        public static bool ForOne = false;
        /// <summary>
        /// 是否可以被饿死
        /// </summary>
        public static bool HungerDown = false;
        /// <summary>
        /// 生长倍率调整
        /// </summary>
        public static int GrowMagnification = 1;
        /// <summary>
        /// 镰刀只收割成熟作物
        /// </summary>
        public static bool GrownCut = false;
        /// <summary>
        /// 饱食度UI横向位置调整
        /// </summary>
        public static int UIoffsetX = 1;
        /// <summary>
        /// 繁殖速度调整
        /// </summary>
        public static float ReproductiveRate = 1f;
        /// <summary>
        /// 食物是否腐败
        /// </summary>
        public static bool FoodSpoilt = true;

    }
}
