using NetSimplified;
using NetSimplified.Syncing;
using static SAA.Content.Planting.System.PlowlandSystem;

namespace SAA.Content.Packages
{
    [AutoSync]
    public class WetArable : NetModule
    {
        private int tileX;
        private int tileY;
        public static void Send(int tileX, int tileY)
        {
            var p = NetModuleLoader.Get<WetArable>();
            p.tileX = tileX;
            p.tileY = tileY;
            p.Send();
        }
        public override void Receive()
        {
            if (Main.dedServ)
            {
                Send(-1, Sender);
            }
            if (wet.Contains((tileX, tileY)))
            {
                wet.Remove((tileX, tileY));
            }
            else wet.Add((tileX, tileY));
        }
    }
}
