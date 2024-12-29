using SAA.Content.Sys;
using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SAA
{
    internal class Configs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [BackgroundColor(55, 120, 175)]
        [DefaultValue(false)]
        [Label("饱食度扣血机制开关")]
        [Tooltip("是, 在饱食度为零时扣血, 否, 以取消. 默认为否")]
        public bool HungerCausePlayerDown;

        [BackgroundColor(55, 120, 175)]
        [Increment(0.1f)]
        [Range(0.1f, 10f)]
        [DefaultValue(1)]
        [Label("饱食度消耗速度")]
        public float HungerCutIndex;

        [BackgroundColor(55, 120, 175)]
        [DefaultValue(true)]
        [Label("食物腐败开关")]
        [Tooltip("是, 食物将无法长期保存, 否, 以取消. 默认为是")]
        public bool FoodSpoilt;

        [BackgroundColor(55, 120, 175)]
        [Increment(1)]
        [Range(1, 100)]
        [DefaultValue(1)]
        [Label("农作物与牧场生长生产倍率")]
        [Tooltip("生长生产速度存在上限, 调整至足够大的值会使所有的农作物与牧场生长生产速度一致")]
        [Slider]
        public int Magnification;

        [BackgroundColor(55, 120, 175)]
        [Increment(0.1f)]
        [Range(0.1f, 10f)]
        [DefaultValue(1)]
        [Label("繁殖速度倍率")]
        [Slider]
        public float ReproductiveRate;

        [BackgroundColor(55, 120, 175)]
        [DefaultValue(false)]
        [Label("农作物成熟收割")]
        [Tooltip("是, 镰刀只会收割成熟的农作物, 否, 以取消. 默认为否")]
        public bool GrownCut;

        public override void OnChanged()
        {
            HungerSetting.HungerDown = HungerCausePlayerDown;
            HungerSetting.GrowMagnification = Magnification;
            HungerSetting.ReproductiveRate = ReproductiveRate;
            HungerSetting.GrownCut = GrownCut;
            HungerSetting.FoodSpoilt = FoodSpoilt;
            HungerforPlayer.BaseHungerCut = 0.001f * HungerCutIndex;
            base.OnChanged();
        }
    }
    internal class ClientConfigs : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        //[BackgroundColor(55, 120, 175)]
        [Increment(1)]
        [Range(-500, 500)]
        [DefaultValue(0)]
        [Label("饱食度UI横向位置调整")]
        [Tooltip("减小此值会使UI向左挪, 反之向右")]
        [Slider]
        public int UIoffsetX;

        public override void OnChanged()
        {
            HungerSetting.UIoffsetX = UIoffsetX;
            base.OnChanged();
        }
    }
}
