using SAA.Content.Sys;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SAA
{
    internal class Configs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [BackgroundColor(53, 120, 178)]
        [DefaultValue(false)]
        [Label("饱食度扣血机制开关")]
        [Tooltip("是, 在饱食度为零时扣血, 否, 以取消. 默认为否")]
        public bool HungerCausePlayerDown;

        [Increment(1)]
        [Range(1, 100)]
        [DefaultValue(1)]
        [Label("农作物与牧场生长生产倍率")]
        [Tooltip("生长生产速度存在上限, 调整至足够大的值会使所有的农作物与牧场生长生产速度一致")]
        [Slider]
        public int Magnification;

        public override void OnChanged()
        {
            HungerSetting.HungerDown = HungerCausePlayerDown;
            HungerSetting.GrowMagnification = Magnification;
            base.OnChanged();
        }
    }
}
