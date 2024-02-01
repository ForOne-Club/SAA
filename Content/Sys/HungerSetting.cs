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
    }
}
