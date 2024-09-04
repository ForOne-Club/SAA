using SAA.Content.Breeding.Tiles;
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
        public Point CreateItem;
        public CookStore(Point cookTile, Item[] cookItems, int burnTime, int maxBurnTime, int finishTime, int maxFinishTime, Point createItem, bool playerUse = false)
        {
            CookTile = cookTile;
            CookItems = cookItems;
            BurnTime = burnTime;
            MaxBurnTime = maxBurnTime;
            FinishTime = finishTime;
            MaxFinishTime = maxFinishTime;
            PlayerUse = playerUse;
            CreateItem = createItem;
        }
    }
    public class RecipeStore
    {
        public List<Point> CookItems;
        public List<Point> CookItemGroups;
        public Point CreateItem;
        public int TileType;//制作物块类
        public int Amount => CookItems.Count + CookItemGroups.Count;
        public RecipeStore(List<Point> cookItems, List<Point> cookItemGroups, Point createItem, int tileType)
        {
            CookItems = cookItems;
            CookItemGroups = cookItemGroups;
            CreateItem = createItem;
            TileType = tileType;
        }
    }
    public class CookSystem : ModSystem
    {
        internal static List<CookStore> Cook = new();
        /// <summary>
        /// 所有满足条件的烹饪锅合成
        /// </summary>
        public static List<RecipeStore> PotCookRecipe = new();
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
                    ["CreateItem_type"] = c.CreateItem.X,
                    ["CreateItem_stack"] = c.CreateItem.Y,
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
                Cook.Add(new CookStore(new Point(ntc.GetInt("Cook_x"), ntc.GetInt("Cook_y")), items, ntc.GetInt("BurnTime_time"), ntc.GetInt("BurnTime_maxtime"), ntc.GetInt("FinishTime_time"), ntc.GetInt("FinishTime_maxtime"), new Point(ntc.GetInt("CreateItem_type"), ntc.GetInt("CreateItem_stack"))));
            }
        }
        public static void FindRecipe(int cookInfo)
        {
            List<RecipeStore> list = new();
            List<Point> Materials = new();
            for (int i = 0; i < Cook[cookInfo].CookItems.Length - 2; i++)//燃烧物和成品占据最后两格
            {
                if (Cook[cookInfo].CookItems[i].type > 0 && Cook[cookInfo].CookItems[i].stack > 0)
                {
                    Materials.Add(new Point(Cook[cookInfo].CookItems[i].type, Cook[cookInfo].CookItems[i].stack));
                    //原材料筛选
                    if (TryFindRecipes(new Predicate<RecipeStore>((r) => (r.TileType == Main.tile[Cook[cookInfo].CookTile.X, Cook[cookInfo].CookTile.Y].TileType && r.CookItems.Count > 0 && r.CookItems.Exists(item => item.X == Cook[cookInfo].CookItems[i].type && Cook[cookInfo].CookItems[i].stack % item.Y == 0)) || (r.CookItemGroups.Count > 0 && r.CookItemGroups.Exists(item => RecipeGroup.recipeGroups[item.X].ValidItems.Contains(Cook[cookInfo].CookItems[i].type) && Cook[cookInfo].CookItems[i].stack % item.Y == 0))), out List<RecipeStore> recipes))
                    {
                        if (list.Count == 0) list = recipes;
                        else list = (list.Intersect(recipes)).ToList();
                    }
                    else
                    {
                        Cook[cookInfo].CreateItem = Point.Zero;
                        return;
                    }
                }
            }
            if (list.Count == 0 || Materials.Count == 0)
            {
                Cook[cookInfo].CreateItem = Point.Zero;
                return;
            }
            foreach (RecipeStore r in list)
            {
                //原材料种数对应
                if (r.Amount != Materials.Count)
                {
                    continue;
                }
                //原材料排除
                if ((r.CookItems.Count > 0 && r.CookItems.Exists(i => !Materials.Exists(m => m.X == i.X && m.Y % i.Y == 0))) || (r.CookItemGroups.Count > 0 && r.CookItemGroups.Exists(i => !Materials.Exists(m => RecipeGroup.recipeGroups[i.X].ValidItems.Contains(m.X) && m.Y % i.Y == 0))))
                {
                    continue;
                }
                //数量统计
                int c1 = 0;
                List<int> cm1 = new();
                for (int i = 0; i < r.CookItems.Count; i++)
                {
                    c1 += r.CookItems[i].Y;
                    cm1.Add(r.CookItems[i].Y);
                }
                for (int i = 0; i < r.CookItemGroups.Count; i++)
                {
                    c1 += r.CookItemGroups[i].Y;
                    cm1.Add(r.CookItemGroups[i].Y);
                }
                int c2 = 0;
                List<int> cm2 = new();
                for (int i = 0; i < Materials.Count; i++)
                {
                    c2 += Materials[i].Y;
                    cm2.Add(Materials[i].Y);
                }
                int k = 0;
                if (c1 != c2 && c2 % c1 != 0)
                {
                    continue;
                }
                else
                {
                    k = c2 / c1;
                }
                cm1.Sort();
                cm2.Sort();
                bool flag = false;
                for (int i = 0; i < cm1.Count; i++)
                {
                    if (cm2[i] / cm1[i] != k) flag = true;
                }
                if (flag) continue;
                if(r.TileType == ModContent.TileType<配种机>())
                    Cook[cookInfo].MaxFinishTime = (int)(r.CreateItem.Y * (ContentSamples.ItemsByType[r.CreateItem.X].rare + 2) * 28800 / HungerSetting.ReproductiveRate);
                else
                    Cook[cookInfo].MaxFinishTime = r.Amount * (ContentSamples.ItemsByType[r.CreateItem.X].rare + 2) * 60 * k;
                Cook[cookInfo].CreateItem = r.CreateItem;
                Cook[cookInfo].CreateItem.Y *= k;
                return;
            }
            Cook[cookInfo].CreateItem = Point.Zero;
        }
        public static bool TryFindRecipes(Predicate<RecipeStore> predicate, out List<RecipeStore> recipes)
        {
            recipes = (from RecipeStore r in PotCookRecipe where r is not null && predicate(r) select r).ToList();
            return recipes.Any();
        }
        public override void PostUpdateTime()
        {
            for (int k = 0; k < Cook.Count; k++)
            {
                if (!WorldGen.InWorld(Cook[k].CookTile.X, Cook[k].CookTile.Y) || Main.tile[Cook[k].CookTile.X, Cook[k].CookTile.Y] == null || !Main.tile[Cook[k].CookTile.X, Cook[k].CookTile.Y].HasTile || !CookTile.CookTileType.Contains(Main.tile[Cook[k].CookTile].TileType))
                {
                    if (CookUI.Open && Main.LocalPlayer.GetModPlayer<CookPlayer>().CookInfo == k)
                    {
                        CookUI.Open = false;
                    }
                    Cook.Remove(Cook[k]);
                    continue;
                }
                bool Burn = false;
                if (Cook[k].BurnTime > 0)
                {
                    Cook[k].BurnTime--;
                    Burn = true;
                }
                if (Cook[k].CreateItem.X > 0 && Cook[k].CreateItem.Y > 0)
                {
                    if (Cook[k].CookItems[^1].stack > 0 && Cook[k].CreateItem.X != Cook[k].CookItems[^1].type)//产出与配方产出不符
                    {
                        break;
                    }

                    if (Burn)//尝试烹饪
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
                            if (Cook[k].CookItems.Length > 4)//等于4为配种机
                            {
                                for (int i = 0; i < Cook[k].CookItems.Length - 2; i++)//消耗
                                {
                                    Cook[k].CookItems[i].TurnToAir();//一锅一份所以直接清除就行
                                }
                            }
                            if (Cook[k].CookItems[^1].stack < 1)
                            {
                                Cook[k].CookItems[^1].SetDefaults(Cook[k].CreateItem.X);
                                Cook[k].CookItems[^1].stack = Cook[k].CreateItem.Y;
                            }
                            else
                            {
                                Cook[k].CookItems[^1].stack += Cook[k].CreateItem.Y;
                            }
                        }
                    }
                    else
                    {
                        if (Cook[k].CookItems[^2].type > 0 && Cook[k].CookItems[^2].stack > 0)
                        {
                            if (Cook[k].CookItems.Length > 4)
                            {
                                if (Cook[k].CookItems[^2].GetGlobalItem<CookItem>().BurnTime > 0)
                                {
                                    Cook[k].BurnTime = Cook[k].MaxBurnTime = Cook[k].CookItems[^2].GetGlobalItem<CookItem>().BurnTime;
                                    Cook[k].CookItems[^2].stack--;
                                    if (Cook[k].CookItems[^2].stack < 1)
                                    {
                                        Cook[k].CookItems[^2].TurnToAir();
                                    }
                                }
                            }
                            else//配种机消耗食物
                            {
                                if (Cook[k].CookItems[^2].GetGlobalItem<HungerforItem>().HealHunger > 0)
                                {
                                    Cook[k].BurnTime = Cook[k].MaxBurnTime = (int)(Cook[k].CookItems[^2].GetGlobalItem<HungerforItem>().HealHunger * 864/HungerSetting.ReproductiveRate);
                                    Cook[k].CookItems[^2].stack--;
                                    if (Cook[k].CookItems[^2].stack < 1)
                                    {
                                        Cook[k].CookItems[^2].TurnToAir();
                                    }
                                }
                            }
                        }
                    }
                    break;
                }
                else
                {
                    if (Cook[k].FinishTime > 0) Cook[k].FinishTime = 0;
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
