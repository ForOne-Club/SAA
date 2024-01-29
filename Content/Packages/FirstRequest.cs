using NetSimplified;
using System.IO;
using static SAA.Content.Planting.System.PlowlandSystem;

namespace SAA.Content.Packages
{
    public class FirstRequest : NetModule
    {
        public override void Send(ModPacket p)
        {
            if (Main.dedServ)
            {
                if (wet == null) return;
                p.Write(wet.Count);
                foreach (var (x, y) in wet)
                {
                    p.Write(x);
                    p.Write(y);
                }
            }
        }
        public override void Read(BinaryReader r)
        {
            if (!Main.dedServ)
            {
                wet = new();
                int count = r.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    wet.Add((r.ReadInt32(), r.ReadInt32()));
                }
            }
        }
        public override void Receive()
        {
            if (Main.dedServ) Send(Sender);
        }
    }
}
