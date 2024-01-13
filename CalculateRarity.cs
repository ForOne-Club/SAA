namespace SAA
{
    public class CalculateRarity
    {
        public enum Phase
        {
            BeforeFleshWall,
            BeforePlantera,
            BeforeMoonLord,
            BeforeSY,
            AfterSY
        }
        public enum ItemType
        {
            Weapon,
            Acc,
            Armor,
            Another
        }
        public enum GetDiff
        {
            /// <summary>
            /// 普通材料合成（例：金银铜铁等地下大量可挖掘矿石），容易直接获取（例：长矛等地表开箱且很容易获取），廉价购买（例：镰刀）
            /// </summary>
            Base,
            /// <summary>
            /// 稀有材料合成（例：紫晶宝石类地下刷新较少的矿石），打败一阶Boss，昂贵购买（例：燧发枪），较难直接获取（例：雀杖等刷新率较低的物品）
            /// </summary>
            Normal,
            /// <summary>
            /// NPC的特殊掉落（例：彩弹枪），打败二阶Boss，很难直接获取（例：星怒，手枪等天空箱或地牢箱的物品），事件掉落（例：鱼叉枪）
            /// </summary>
            Rare,
            /// <summary>
            /// 特殊材料合成（例：小雪怪法杖，熔岩镐等材料不易获取的物品），打败三阶Boss，极难直接获取（例：暗黑长戟等地狱箱的物品）
            /// </summary>
            Epic
        }
        public struct Rarity
        {
            public int phase;
            public int type;
            public int diff;
            public bool ultraMade = false;
            public int[,] rating = new int[5, 4]
            {
                { 0, 0, 0, 0 },
                { 4, 4, 4, 1 },
                { 6, 4, 7, 3 },
                { 10, 7, 10, 7 },
                { 13, 10, 13, 10 }
            };
            /// <summary>
            /// 使用ToItemRarity()
            /// </summary>
            /// <param name="phase">物品属于什么阶段</param>
            /// <param name="type">物品是什么种类</param>
            /// <param name="diff">物品的获得难度</param>
            /// <param name="ultraMade">物品是否由一堆难搞的东西合成</param> 
            public Rarity(Phase phase, ItemType type, GetDiff diff, bool ultraMade)
            {
                this.phase = (int)phase;
                this.type = (int)type;
                this.diff = (int)diff;
                this.ultraMade = ultraMade;
            }
            public int ToItemRarity()
            {
                return rating[phase, type] + diff + (ultraMade ? 1 : 0);
            }
        }
    }
}