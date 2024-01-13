using SAA.Content.Foods;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;

namespace SAA
{
    public static class Helper
    {
        public static int[] GrassAndThorny = new int[]
        {
            3,24,32,52,61,62,69,71,73,74,110,113,115,184,201,205,352,382,636,637,638,655,
        };
        /// <summary>
        /// 判断能否放置顺带清除这里的可覆盖物块，左下角为基准
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool CanPlaceOnIt(int i, int j, int width, int height, bool nowall = true, bool toptile = false)
        {
            bool flag = true;
            for (int k = 0; k < width; k++)
            {
                for (int l = 0; l < height; l++)
                {
                    if (l == (toptile ? height - 1 : 0))
                    {
                        flag = flag && WorldGen.SolidTile(i + k, j + (toptile ? -l - 1 : 1));//下方有物块碰撞
                    }
                    flag = flag && Main.tile[i + k, j - l].LiquidAmount == 0 && (!Main.tile[i + k, j - l].HasTile || GrassAndThorny.Contains(Main.tile[i + k, j - l].TileType));
                    if (nowall) flag = flag && Main.tile[i + k, j - l].WallType == 0;
                    if (!flag) break;
                }
            }
            if (flag)
            {
                for (int k = 0; k < width; k++)
                {
                    for (int l = 0; l < height; l++)
                    {
                        Main.tile[i + k, j - l].ClearTile();
                    }
                }
            }
            return flag;
        }
        /// <summary>
        /// 在方形范围内寻找相同的物块，有则返回false
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static bool HasNotAnySameOne(int i, int j, int width, int height, int type)
        {
            bool flag = true;
            for (int k = -width; k < width; k++)
            {
                for (int l = -height; l < height; l++)
                {
                    if (WorldGen.InWorld(i + k, j + l))
                    {
                        Tile tile = Main.tile[i + k, j + l];
                        if (tile != null && tile.HasTile && tile.TileType == type)
                        {
                            flag = false;
                            break;
                        }
                    }
                }
            }
            return flag;
        }
        //普通掉落
        public static CommonDrop PercentageDrop(int itemID, float precent, int min = 1, int max = 1)
        {
            int denominater = 1;
            while (precent % 1 != 0)
            {
                precent *= 10;
                denominater *= 10;
            }
            return new(itemID, denominater, min, max, (int)precent);
        }
        /// <summary>
        /// 判断物品是否为食物
        /// </summary>
        public static bool IsFoods(Item item, out int bufftype, out int bufftime)
        {
            bufftype = 0;
            bufftime = 0;
            if (item != null && item.consumable)
            {
                bufftype = item.buffType;
                bufftime = item.buffTime;
                return bufftime > 0 && (bufftype == 26 || bufftype == 206 || bufftype == 207);
            }
            return false;
        }
        /// <summary>
        /// 设置食材的属性，包括使用，堆叠，buff，time，value
        /// </summary>
        /// <param name="item"></param>
        /// <param name="level">饱食buff 0-2</param>
        /// <param name="hunger">饱食度</param>
        public static void SetFood(this Item item, int level, int hunger)
        {
            item.maxStack = 9999;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useStyle = ItemUseStyleID.EatFood;
            item.UseSound = SoundID.Item2;
            item.consumable = true;
            item.useTurn = false;
            item.buffType = level switch
            {
                0 => 26,
                1 => 206,
                2 => 207,
                _ => 0
            };
            item.buffTime = hunger * 900 * (int)Math.Pow(2, 2 - level);
            item.value = hunger * 10;
        }
        /// <summary>
        /// 油炸食材，包含合成表注册
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ingredient">食材的id</param>
        /// <param name="amount">单次份数</param>
        /// <returns></returns>
        public static Recipe Fried(this ModItem item, int ingredient, int amount = 1)
        {
            return item.CreateRecipe(amount)
                .AddIngredient(ingredient, amount)
                .AddIngredient(ModContent.ItemType<海麦>(), amount)
                .AddIngredient(ModContent.ItemType<油罐>())
                .AddTile(TileID.CookingPots)
                .Register();
        }
        /// <summary>
        /// 模式时间间隔区分
        /// </summary>
        public static int ModeNum(int commontime, int experttime, int mastertime)
        {
            return Main.expertMode ? (Main.masterMode ? mastertime : experttime) : commontime;
        }
        public static float ModeNum(float commontime, float experttime, float mastertime)
        {
            return Main.expertMode ? (Main.masterMode ? mastertime : experttime) : commontime;
        }
        /// <summary>
        /// 模式npc弹幕伤害区分
        /// </summary>
        public static int ModeDamage(int commondamage, int expertdamage, int masterdamage, bool isProjectile = true, bool ignoreFTW = false)
        {
            float mul = (Main.GameModeInfo.EnemyDamageMultiplier + (ignoreFTW ? 0 : Main.getGoodWorld.ToInt())) * (isProjectile ? 2 : 1);
            switch (Main.GameMode)
            {
                case GameModeID.Creative:
                    {
                        CreativePowers.DifficultySliderPower power = CreativePowerManager.Instance.GetPower<CreativePowers.DifficultySliderPower>();
                        if (power.GetIsUnlocked())
                        {
                            return (int)(commondamage / (power.StrengthMultiplierToGiveNPCs * 2));
                        }
                        return (int)(commondamage / mul);
                    }
                default:
                case GameModeID.Normal:
                    {
                        return (int)(commondamage / mul);
                    }
                case GameModeID.Expert:
                    {
                        return (int)(expertdamage / mul);
                    }
                case GameModeID.Master:
                    {
                        return (int)(masterdamage / mul);
                    }
            }
        }
        public static List<NPC> FindNPC(Predicate<NPC> predicate)
        {
            List<NPC> list = new();
            foreach (NPC npc in Main.npc)
            {
                if (npc.active && predicate(npc))
                {
                    list.Add(npc);
                }
            }
            return list;
        }/// <summary>
         ///  请在ModTile.SSD()中使用，设置物块基本属性
         /// </summary>
         /// <param name="type"></param>
         /// <param name="solid">是否实体（能否不可穿过）</param>
         /// <param name="solidTop">是否顶部允许站立</param>
         /// <param name="noAttach">是否不允许其他物块攀附</param>
         /// <param name="table">是否视为桌子（顶上可以放瓶子存钱罐）</param>
         /// <param name="lavaDeath">会被岩浆做掉</param>
         /// <param name="frameImportant">帧对齐</param>
         /// <param name="cut">会被弹幕做掉</param>
         /// <param name="blockLight">是否阻挡光传播</param>
         /// <param name="mergeDirt">是否与土相连</param>
         /// <param name="light">是否发光，需要配合ModifyLight使用</param>
         /// <param name="dust">挖掘时粒子</param>
        public static void SetTileBase(this ModTile tile, bool solid, bool solidTop, bool noAttach, bool table,
            bool lavaDeath, bool frameImportant, bool cut, bool blockLight, bool mergeDirt, bool light, int dust = 0)
        {
            int type = tile.Type;
            Main.tileSolid[type] = solid;
            Main.tileSolidTop[type] = solidTop;
            Main.tileNoAttach[type] = noAttach;
            Main.tileTable[type] = table;
            Main.tileLavaDeath[type] = lavaDeath;
            Main.tileFrameImportant[type] = frameImportant;
            Main.tileCut[type] = cut;
            Main.tileBlockLight[type] = blockLight;
            Main.tileMergeDirt[type] = mergeDirt;
            Main.tileLighted[type] = true;
            tile.DustType = dust;
        }
        /// <summary>
        /// 设置物块价值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="ore">是否为矿石</param>
        /// <param name="spelunker">是否被洞穴探险药水高亮</param>
        /// <param name="oreFinderPriority">金属探测器优先级
        /// <br/>任意罐子=100 坚固化石=150 沙漠化石=150
        /// <br/>铜锡铁铅银钨金铂 200~270（+10）
        /// <br/>魔矿=300 猩红矿=310 陨石=400
        /// <br/>任意宝箱=500
        /// <br/>钴蓝 钯金 秘银 山铜 精金 钛金 600~650（+10）
        /// <br/>叶绿矿=700 奇异植物=750
        /// <br/>水晶之心=800 生命果=810</param>
        /// <param name="glowStick">洞穴探险荧光棒高亮</param>
        /// <param name="shine">闪烁亮点的频率，越大越快</param>
        /// <param name="mineResist">挖掘抗性</param>
        /// <param name="minPick">最低挖掘力</param>
        public static void SetTileValue(this ModTile tile, bool ore, bool spelunker,
            short oreFinderPriority, bool glowStick, int shine, float mineResist, int minPick)
        {
            int type = tile.Type;
            TileID.Sets.Ore[type] = ore;
            Main.tileSpelunker[type] = spelunker;
            Main.tileOreFinderPriority[type] = oreFinderPriority;
            Main.tileShine2[type] = glowStick;
            Main.tileShine[type] = shine;
            tile.MineResist = mineResist;
            tile.MinPick = minPick;
        }
        public static void RegisterTile(this ModTile tile, Color color)
        {
            tile.AddMapEntry(color, tile.CreateMapEntryName());
        }
        public static void RegisterAsCommonTile(this ModTile tile, Color color)
        {
            tile.SetTileBase(true, true, false, false, false, true, false, true, false, false, DustID.Mud);
            tile.RegisterTile(color);
        }
        public static bool TileChain(Predicate<Tile> condition, Predicate<Tile> breakCd, int oriX, int oriY, int nowX, int nowY, int maxDis, HashSet<(int, int)> ignoreTiles = null)
        {
            if (WorldGen.InWorld(nowX, nowY))
            {
                ignoreTiles = new();
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int x = nowX + i, y = nowY + j;
                        if (ignoreTiles.Contains((x, y))) continue;
                        Tile tile = Main.tile[x, y];
                        if (breakCd.Invoke(tile)) return true;
                        bool bk = false;
                        if (condition.Invoke(tile) && Math.Abs(oriX - x) <= maxDis && Math.Abs(oriY - y) <= maxDis)
                        {
                            ignoreTiles.Add((x, y));
                            bk = TileChain(condition, breakCd, oriX, oriY, x, y, maxDis, ignoreTiles);
                            if (bk) return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}