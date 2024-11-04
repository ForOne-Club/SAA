using NetSimplified;
using SAA.Content.Sys;
using System.IO;

namespace SAA.Content.Packages
{
    public class CookFirstRequest : NetModule
    {
        public override void Send(ModPacket p)
        {
            if (Main.dedServ)
            {
                p.Write(CookSystem.Cook.Count);
                foreach (var c in CookSystem.Cook)
                {
                    p.Write(c.CookTile);
                    p.Write(c.CookItems);
                    p.Write(c.BurnTime);
                    p.Write(c.MaxBurnTime);
                    p.Write(c.FinishTime);
                    p.Write(c.MaxFinishTime);
                    p.Write(c.CreateItem);
                    p.Write(c.PlayerUse);
                }
            }
        }
        public override void Read(BinaryReader r)
        {
            if (!Main.dedServ)
            {
                CookSystem.Cook = new();
                int count = r.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    CookSystem.Cook.Add(new CookStore(r.ReadPoint(), r.ReadItemArray(), r.ReadInt32(), r.ReadInt32(), r.ReadInt32(), r.ReadInt32(), r.ReadPoint(), r.ReadBoolean()));
                }
            }
        }
        public override void Receive()
        {
            if (Main.dedServ)
            {
                Send(Sender);
            }
        }
    }
}
