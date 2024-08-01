using NetSimplified;
using SAA.Content.Packages;

namespace SAA.Content.Sys;

public class CookPlayer : ModPlayer
{
    public int CookInfo = -1;
    public override void OnEnterWorld()
    {
        NetModuleLoader.Get<CookFirstRequest>().Send();
    }
}
public class CookTile : GlobalTile
{
    public static int[] CookTileType = { 96 };
    public override void MouseOver(int i, int j, int type)
    {
        if (type == 96)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = 345;
        }
        base.MouseOver(i, j, type);
    }
    public override void RightClick(int i, int j, int type)
    {
        if (type == 96)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            if (CookSystem.Cook.Exists(a => a.CookTile == new Point(x, y)))
            {
                if (!CookSystem.Cook.Find(a => a.CookTile == new Point(x, y)).PlayerUse)
                {
                    if (CookUI.Open && CookSystem.Cook.FindIndex(a => a.CookTile == new Point(x, y)) == Main.LocalPlayer.GetModPlayer<CookPlayer>().CookInfo)
                    {
                        CookUI.Open = false;
                        SoundEngine.PlaySound(SoundID.MenuClose);
                        Cook.NetSend(x, y, false);//同步
                    }
                    else
                    {
                        int index = CookSystem.Cook.FindIndex(a => a.CookTile == new Point(x, y));
                        Cook.NetSend(x, y, true);//同步
                        Main.LocalPlayer.GetModPlayer<CookPlayer>().CookInfo = index;
                        CookUI.Open = true;
                        Main.playerInventory = true;
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                    }
                }
            }
            else
            {
                CookSystem.Cook.Add(new CookStore(new Point(x, y), [new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0), new Item(0)], 0, 0, 0, 0, Point.Zero));
                Cook.NetSend(x, y, true);//创建并同步
                Main.LocalPlayer.GetModPlayer<CookPlayer>().CookInfo = CookSystem.Cook.Count - 1;
                CookUI.Open = true;
                Main.playerInventory = true;
                SoundEngine.PlaySound(SoundID.MenuOpen);
            }
        }
        base.RightClick(i, j, type);
    }
    public override bool CanExplode(int i, int j, int type)
    {
        if (type == 96)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            if (CookSystem.Cook.Exists(a => a.CookTile == new Point(x, y)))
            {
                var c = CookSystem.Cook.Find(a => a.CookTile == new Point(x, y));
                foreach (Item item in c.CookItems)
                {
                    if (item != null && item.type > 0 && item.stack > 0)
                    {
                        return false;//有东西存放则不可破坏，但是只这样写可以通过破坏底部物块进行间接破坏，所以要下面加上判断
                    }
                }
            }
        }
        else
        {
            int g = Main.tile[i, j - 1].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - 1 - Main.tile[i, j - 1].TileFrameY / 18;
            if (CookSystem.Cook.Exists(a => a.CookTile == new Point(x, y)))
            {
                var c = CookSystem.Cook.Find(a => a.CookTile == new Point(x, y));
                foreach (Item item in c.CookItems)
                {
                    if (item != null && item.type > 0 && item.stack > 0)
                    {
                        return false;
                    }
                }
            }
        }
        return base.CanExplode(i, j, type);
    }
    public override bool CanKillTile(int i, int j, int type, ref bool blockDamaged)
    {
        if (type == 96)
        {
            int g = Main.tile[i, j].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - Main.tile[i, j].TileFrameY / 18;
            if (CookSystem.Cook.Exists(a => a.CookTile == new Point(x, y)))
            {
                var c = CookSystem.Cook.Find(a => a.CookTile == new Point(x, y));
                foreach (Item item in c.CookItems)
                {
                    if (item != null && item.type > 0 && item.stack > 0)
                    {
                        return false;//有东西存放则不可破坏，但是只这样写可以通过破坏底部物块进行间接破坏，所以要下面加上判断
                    }
                }
            }
        }
        else
        {
            int g = Main.tile[i, j - 1].TileFrameX / 18;
            int x = i - g % 2;
            int y = j - 1 - Main.tile[i, j - 1].TileFrameY / 18;
            if (CookSystem.Cook.Exists(a => a.CookTile == new Point(x, y)))
            {
                var c = CookSystem.Cook.Find(a => a.CookTile == new Point(x, y));
                foreach (Item item in c.CookItems)
                {
                    if (item != null && item.type > 0 && item.stack > 0)
                    {
                        return false;
                    }
                }
            }
        }
        return base.CanKillTile(i, j, type, ref blockDamaged);
    }
}
public class CookItem : GlobalItem
{
    public int BurnTime = 0;
    public override void SetDefaults(Item entity)
    {
        switch (entity.type)
        {
            case 9://木材
                entity.GetGlobalItem<CookItem>().BurnTime = 3 * 60;
                break;
            case 2503://针叶木
            case 2504://棕榈木
            case 2260://王朝木
            case 5215://灰烬木
                entity.GetGlobalItem<CookItem>().BurnTime = 4 * 60;
                break;
            case 620://红木
            case 619://乌木
            case 911://暗影木
                entity.GetGlobalItem<CookItem>().BurnTime = 5 * 60;
                break;
            case 621://珍珠木
                entity.GetGlobalItem<CookItem>().BurnTime = 6 * 60;
                break;
            case 1729://阴森木
                entity.GetGlobalItem<CookItem>().BurnTime = 7 * 60;
                break;
        }
    }
    public override bool InstancePerEntity => true;
}
