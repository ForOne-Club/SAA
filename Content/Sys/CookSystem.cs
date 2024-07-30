using Terraria.ModLoader.IO;
using Terraria.UI;

namespace SAA.Content.Sys
{
    public class CookStore
    {
        public Point CookTile;
        public Item[] CookItems;
        public int BurnTime;
        public int MaxBurnTime;
        public int FinishTime;
        public int MaxFinishTime;
        public bool PlayerUse;
        public CookStore(Point cookTile, Item[] cookItems, int burnTime, int maxBurnTime, int finishTime, int maxFinishTime, bool playerUse = false)
        {
            CookTile = cookTile;
            CookItems = cookItems;
            BurnTime = burnTime;
            MaxBurnTime = maxBurnTime;
            FinishTime = finishTime;
            MaxFinishTime = maxFinishTime;
            PlayerUse = playerUse;
        }
    }
    public class CookSystem : ModSystem
    {
        internal static List<CookStore> Cook = new();
        /// <summary>
        /// 材料(type,stack),成品,所需燃烧时间,材料组(grouptype,stack)
        /// </summary>
        public static HashSet<(Point[], Point, int, Point[])> PotCookRecipe = new();
        public override void SaveWorldData(TagCompound tag)
        {
            var cooks = new List<TagCompound>();
            foreach (CookStore c in Cook)
            {
                var list = new List<TagCompound>();
                for (int k = 0; k < c.CookItems.Length; k++)
                {
                    var tc = ItemIO.Save(c.CookItems[k]);
                    list.Add(tc);
                }
                var ntc = new TagCompound
                {
                    ["Cook_x"] = c.CookTile.X,
                    ["Cook_y"] = c.CookTile.Y,
                    ["CookItems"] = list,
                    ["BurnTime_time"] = c.BurnTime,
                    ["BurnTime_maxtime"] = c.MaxBurnTime,
                    ["FinishTime_time"] = c.FinishTime,
                    ["FinishTime_maxtime"] = c.MaxFinishTime,
                };
                cooks.Add(ntc);
            }
            tag["Cook"] = cooks;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            List<TagCompound> cooks = (List<TagCompound>)tag.GetList<TagCompound>("Cook");
            Cook = new();
            for (int k = 0; k < cooks.Count; k++)
            {
                var ntc = cooks[k];
                var list = (List<TagCompound>)ntc.GetList<TagCompound>("CookItems");
                Item[] items = new Item[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    items[i] = new Item(0);
                }
                for (int l = 0; l < list.Count; l++)
                {
                    ItemIO.Load(items[l], list[l]);
                }
                Cook.Add(new CookStore(new Point(ntc.GetInt("Cook_x"), ntc.GetInt("Cook_y")), items, ntc.GetInt("BurnTime_time"), ntc.GetInt("BurnTime_maxtime"), ntc.GetInt("FinishTime_time"), ntc.GetInt("FinishTime_maxtime")));
            }
        }
        public override void PostUpdateTime()
        {
            for (int k = 0; k < Cook.Count; k++)
            {
                //这样写相当于和烹饪锅绑定了，需要改
                if (!WorldGen.InWorld(Cook[k].CookTile.X, Cook[k].CookTile.Y) || Main.tile[Cook[k].CookTile.X, Cook[k].CookTile.Y] == null || !Main.tile[Cook[k].CookTile.X, Cook[k].CookTile.Y].HasTile || Main.tile[Cook[k].CookTile.X, Cook[k].CookTile.Y].TileType != 96)
                    Cook.Remove(Cook[k]);
                bool Burn = false;
                if (Cook[k].BurnTime > 0)
                {
                    Cook[k].BurnTime--;
                    Burn = true;
                }
                List<Point> require = new();
                for (int i = 0; i < Cook[k].CookItems.Length - 2; i++)//燃烧物和成品占据最后两格
                {
                    if (Cook[k].CookItems[i] != null && Cook[k].CookItems[i].type > 0 && Cook[k].CookItems[i].stack > 0)
                    {
                        require.Add(new Point(Cook[k].CookItems[i].type, Cook[k].CookItems[i].stack));
                    }
                }
                foreach (var c in PotCookRecipe)
                {
                    if (c.Item1.ToList().Count == require.Count && c.Item1.ToList().All(require.Contains))//配方匹配成功，目前写法需要材料数量完全一致并且为一份，不能多也不能少
                    {
                        if (Cook[k].CookItems[^1].stack > 0 && c.Item2.X != Cook[k].CookItems[^1].type)//产出与配方产出不符
                        {
                            break;
                        }
                        Cook[k].MaxFinishTime = c.Item3;//完成时间
                        //尝试烹饪
                        if (Burn)
                        {
                            if (Cook[k].FinishTime < 0)
                            {
                                Cook[k].FinishTime = 0;
                            }
                            if (Cook[k].FinishTime < Cook[k].MaxFinishTime)
                            {
                                Cook[k].FinishTime++;
                            }
                            else//完成此次烹饪
                            {
                                Cook[k].FinishTime = 0;
                                for (int i = 0; i < Cook[k].CookItems.Length - 2; i++)//消耗
                                {
                                    Cook[k].CookItems[i].stack -= c.Item1.ToList()
                                                                         .Find(a => a.X == Cook[k].CookItems[i].type).Y;
                                    if (Cook[k].CookItems[i].stack < 1)
                                    {
                                        Cook[k].CookItems[i].TurnToAir();
                                    }
                                }
                                if (Cook[k].CookItems[^1].stack < 1)
                                {
                                    Cook[k].CookItems[^1].SetDefaults(c.Item2.X);
                                    Cook[k].CookItems[^1].stack = c.Item2.Y;
                                }
                                else
                                {
                                    Cook[k].CookItems[^1].stack += c.Item2.Y;
                                }
                            }
                        }
                        else
                        {
                            if (Cook[k].CookItems[^2].type > 0 && Cook[k].CookItems[^2].stack > 0 && Cook[k].CookItems[^2].GetGlobalItem<CookItem>().BurnTime > 0)
                            {
                                Cook[k].BurnTime = Cook[k].MaxBurnTime = Cook[k].CookItems[^2].GetGlobalItem<CookItem>().BurnTime;
                                Cook[k].CookItems[^2].stack--;
                                if (Cook[k].CookItems[^2].stack < 1)
                                {
                                    Cook[k].CookItems[^2].TurnToAir();
                                }
                            }
                        }
                        break;
                    }
                }
            }
            base.PostUpdateTime();
        }

        internal CookUI CUI;
        internal UserInterface CUserInterface;
        public override void Load()
        {
            CUI = new CookUI();
            CUI.Activate();
            CUserInterface = new UserInterface();
            CUserInterface.SetState(CUI);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            CUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            //寻找一个名字为Vanilla: Mouse Text的绘制层，也就是绘制鼠标字体的那一层，并且返回那一层的索引
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            //寻找到索引时
            if (MouseTextIndex != -1)
            {
                //往绘制层集合插入一个成员，第一个参数是插入的地方的索引，第二个参数是绘制层
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   //这里是绘制层的名字
                   "I : CookUI",
                   //这里是匿名方法
                   delegate
                   {
                       if (CookUI.Open) CUI.Draw(Main.spriteBatch);
                       return true;
                   },
                   //这里是绘制层的类型
                   InterfaceScaleType.UI)
               );
            }
        }
    }
}
