using NetSimplified;
using NetSimplified.Syncing;
using SAA.Content.Sys;

namespace SAA.Content.Packages
{
    [AutoSync]
    public class Cook : NetModule
    {
        private Point CookTile;
        private Item[] CookItems;
        private Point BurnTime;
        private Point FinishTime;
        private bool PlayerUse;
        private Point CreateItem;
        //private int Context;
        public static void NetSend(int tileX, int tileY, bool playerUse = false)//, int context = 0)
        {
            var p = NetModuleLoader.Get<Cook>();
            p.CookTile = new Point(tileX, tileY);
            //p.Context = context;
            //if (context != 0)//为0则添加或清除
            //{
            //    if (CookSystem.Cook.Exists(a => a.Item1 == p.CookTile))
            //    {
            //        var c = CookSystem.Cook.Find(a => a.Item1 == p.CookTile);
            //        if (context == 1) //同步
            //        {
            //            p.CookItems = c.Item2;
            //            p.BurnTime = c.Item3;
            //            p.FinishTime = c.Item4;
            //            //p.PlayerUse = c.Item5;
            //        }
            //        else if (context == 2)//使用同步
            //        {
            //            p.PlayerUse = c.Item5;
            //        }
            //    }
            //}
            if (CookSystem.Cook.Exists(a => a.CookTile == p.CookTile))
            {
                var c = CookSystem.Cook.Find(a => a.CookTile == p.CookTile);
                p.CookItems = c.CookItems;
                p.BurnTime = new Point(c.BurnTime, c.MaxBurnTime);
                p.FinishTime = new Point(c.FinishTime, c.MaxFinishTime);
                p.PlayerUse = playerUse;//其他端需要根据这个判断玩家是否可以打开，本端不需要
                p.CreateItem = c.CreateItem;
                p.Send();
            }
        }
        public override void Receive()
        {
            if (Main.dedServ)
            {
                base.Send(-1, Sender);
            }
            if (CookSystem.Cook.Exists(a => a.CookTile == CookTile))
            {
                //switch (Context)
                //{
                //    case 0:
                //        CookSystem.Cook.Remove(CookSystem.Cook.Find(a => a.Item1 == CookTile));直接在Cook更新中清除
                //        break;
                //    case 1:
                var c = CookSystem.Cook.Find(a => a.CookTile == CookTile);
                c.CookItems = CookItems;
                c.BurnTime = BurnTime.X;
                c.MaxBurnTime = BurnTime.Y;
                c.FinishTime = FinishTime.X;
                c.MaxFinishTime = FinishTime.Y;
                //    break;
                //case 2:
                c.PlayerUse = PlayerUse;
                c.CreateItem = CreateItem;
                //        break;
                //}
            }
            else
            {
                //switch (Context)
                //{
                //    case 0:
                //        CookSystem.Cook.Add((CookTile, new Item[8] { new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0) }, Point.Zero, Point.Zero, false));
                //        break;
                //    case 2:
                CookSystem.Cook.Add(new CookStore(CookTile, [new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0)], 0, 0, 0, 0, Point.Zero, PlayerUse));
                //        break;
                //}
            }
        }
    }
}
