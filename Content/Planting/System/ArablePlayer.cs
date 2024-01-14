using NetSimplified;
using SAA.Content.Packages;

namespace SAA.Content.Planting.System
{
    public class ArablePlayer : ModPlayer
    {
        public override void OnEnterWorld()
        {
            NetModuleLoader.Get<FirstRequest>().Send();
        }
    }
}
