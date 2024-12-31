using SAA.Content.Buffs;
using SAA.Content.Foods;
using SAA.Content.Planting.Tiles.Plants;
using SAA.Content.Sys;
using Terraria.Enums;
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
                return bufftime > 0 && (bufftype == 26 || bufftype == 206 || bufftype == 207||bufftype == ModContent.BuffType<腹泻>());
            }
            return false;
        }
        /// <summary>
        /// 设置食材的属性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="level">0/1/2</param>
        /// <param name="hunger"></param>
        public static void SetFoodMaterials(this Item item, int width, int height, int level, int hunger, bool canEat = false, bool useGolpSound = false)
        {
            if (canEat)
            {
                item.DefaultToFood(width, height, ModContent.BuffType<腹泻>(), hunger * 900 * (int)Math.Pow(2, 2 - level), useGolpSound);
            }
            else
            {
                item.width = width;
                item.height = height;
                item.maxStack = 9999;
            }
            item.SetShopValues((ItemRarityColor)level, hunger * 300 * (level + 1));
        }
        /// <summary>
        /// 设置食品的属性，包括使用，堆叠，buff，time，value等
        /// </summary>
        /// <param name="item"></param>
        /// <param name="width">长</param>
        /// <param name="height">宽</param>
        /// <param name="level">饱食度buff等级:0/1/2</param>
        /// <param name="hunger">饱食度</param>
        /// <param name="useGolpSound">是否使用饮用音效</param>
        public static void SetFood(this Item item, int width, int height, int level, int hunger, bool useGolpSound = false)
        {
            int buffType = level switch
            {
                0 => 26,
                1 => 206,
                2 => 207,
                _ => 0
            };
            item.DefaultToFood(width, height, buffType, hunger * 900 * (int)Math.Pow(2, 2 - level), useGolpSound);
            item.SetShopValues((ItemRarityColor)level, hunger * 300 * (level + 1));
        }
        public static void SetOriginFood(this Item item, int width, int height, int buffType, int buffTime, bool useGolpSound = false)
        {
            int level = buffType switch
            {
                26 => 0,
                206 => 1,
                207 => 2,
                _ => 0
            };
            int hunger = HungerforItem.QuickBuff_FindFoodPriority(buffType) * buffTime / 3600;
            item.DefaultToFood(width, height, buffType, hunger * 900 * (int)Math.Pow(2, 2 - level), useGolpSound);
            item.SetShopValues((ItemRarityColor)level, hunger * 300 * (level + 1));
        }
        /// <summary>
        /// 油炸食材，包含合成表注册
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ingredient">食材的id</param>
        /// <param name="amount">单次份数</param>
        /// <param name="wheatamount">海麦用量</param>
        /// <returns></returns>
        public static Recipe Fried(this ModItem item, int ingredient, int amount = 1, int wheatamount = 1, bool bottle = false)
        {
            if (bottle)
            {
                return item.CreateRecipe(amount)
                    .AddIngredient(ingredient, amount)
                    .AddIngredient(ModContent.ItemType<海麦>(), wheatamount)
                    .AddIngredient(ModContent.ItemType<油罐>())
                    .AddTile(TileID.CookingPots)
                    .Register();
            }
            else
            {
                return item.CreateRecipe(amount)
                    .AddIngredient(ingredient, amount)
                    .AddIngredient(ModContent.ItemType<海麦>(), wheatamount)
                    .AddIngredient(ModContent.ItemType<油果>())
                    .AddTile(TileID.CookingPots)
                    .Register();
            }
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
        }
        public static void RegisterTile(this ModTile tile, Color color)
        {
            tile.AddMapEntry(color, tile.CreateMapEntryName());
        }
        /// <summary>
        /// 施肥
        /// </summary>
        public static void Fertilize(int width, Vector2 origin, Vector2 unit, int growMagnification = 1, bool needDayTime = true, bool needWet = true)
        {
            int k = width / 2;
            Vector2 center = origin + k * unit.SafeNormalize(Vector2.One);
            Rectangle hitbox = new Rectangle((int)center.X - k, (int)center.Y - k, width, width);
            int minX = hitbox.X / 16;
            int maxX = (hitbox.X + hitbox.Width) / 16 + 1;
            int minY = hitbox.Y / 16;
            int maxY = (hitbox.Y + hitbox.Height) / 16 + 1;
            Utils.ClampWithinWorld(ref minX, ref minY, ref maxX, ref maxY);
            for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.HasTile && TileLoader.GetTile(tile.TileType) is Plant plant)
                    {
                        plant.TryGrow(i, j, growMagnification, needDayTime, needWet);
                    }
                }
            }
        }
    }
}