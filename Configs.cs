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
        public override void OnChanged()
        {
            HungerSetting.HungerDown = HungerCausePlayerDown;
            base.OnChanged();
        }
    }
}
