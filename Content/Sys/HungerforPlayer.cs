using SAA.Content.Buffs;
using SAA.Content.Items;
using Terraria.ModLoader.IO;

namespace SAA.Content.Sys;

public class HungerforPlayer : ModPlayer
{
    public float HungerCut = 0.001f;//削减速度
    public int HungerCount = 5;//饱食数(秘制料理数)
    public int HungerKillTime = 0;//死亡饥饿计时，未死亡且增加足够的饱食度后清零，无需退出保存，没有必要
    public int HungerReduce = 0;//记录的饱食度缺额，通过吃东西去除(每次摄入饱食度大于10)
    public float Hunger = 100.5f;
    public float HungerMax = 100.5f;
    public bool HungerBook = false;
    public int PoopTime = 300;//拉臭臭间隔时间
    internal static int[] HungerBuff = { 26, 206, 207, 332, 333, 334, ModContent.BuffType<饿瘪了>(), ModContent.BuffType<一级饱和>(), ModContent.BuffType<二级饱和>(), ModContent.BuffType<三级饱和>(), ModContent.BuffType<强效饱和>(), ModContent.BuffType<超级饱和>() };
    /// <summary>
    /// 庄稼收成
    /// </summary>
    public float CropHarvest = 1;
    public override void SaveData(TagCompound tag)
    {
        tag["BaoShi"] = Hunger;
        tag["BaoShiShu"] = HungerCount;
        tag["BaoShiSuoJian"] = HungerReduce;
    }
    public override void LoadData(TagCompound tag)
    {
        Hunger = tag.GetFloat("BaoShi");
        HungerCount = tag.GetInt("BaoShiShu");
        HungerReduce = tag.GetInt("BaoShiSuoJian");
    }
    public override void ResetEffects()
    {
        HungerCut = 0.001f;
        HungerBook = false;
        if (Hunger < 20)
        {
            int lifecut = (int)((20 - Hunger) / 20f * Player.statLifeMax2 / 5);
            Player.statLifeMax2 -= lifecut;
        }

        CropHarvest = 1;
    }
    public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)//死亡惩罚
    {
        if (Hunger < 100.5f)
        {
            Hunger = 100.5f;
        }
        else
        {
            if (Main.expertMode)
            {
                if (Main.masterMode)
                {
                    Hunger -= 0.4f * HungerMax;
                }
                else
                {
                    Hunger -= 0.2f * HungerMax;
                }
            }
            else
            {
                Hunger -= 0.1f * HungerMax;
            }
            if (Hunger < 100.5f) Hunger = 100.5f;
        }
        if (HungerKillTime > 0)
        {
            int cut = HungerKillTime / 600;//每10s扣除一点
            if (cut <= 0) cut = 1;
            int hungerMax;
            if (HungerCount > 20)
            {
                hungerMax = (HungerCount - 20) * 5;
            }
            else
            {
                hungerMax = HungerCount * 20;
            }
            HungerReduce += cut;
            if (hungerMax - HungerReduce < 20) HungerReduce = hungerMax - 20;
        }
        float h = HungerMax - HungerReduce;
        if (Hunger > h) Hunger = h;
        base.Kill(damage, hitDirection, pvp, damageSource);
    }
    public override void UpdateLifeRegen()
    {
        if (Player.whoAmI == Main.myPlayer)
        {
            float addi = 0, mult = 1;
            if (HungerBook)
            {
                addi -= 0.0002f;
                Player.cordage = true;
                Player.buffImmune[BuffID.Poisoned] = true;//免疫中毒            
                Player.buffImmune[BuffID.Weak] = true;//免疫虚弱
            }
            foreach (int buff in Player.buffType)
            {
                if (buff == ModContent.BuffType<吃撑>())
                {
                    mult *= 0.75f;
                }
                if (buff == ModContent.BuffType<酸甜可口>())
                {
                    mult *= 1.5f;
                }
                if (buff == ModContent.BuffType<饱食之火>() || buff == ModContent.BuffType<饱腹>())
                {
                    addi -= 0.0002f;
                }
            }
            HungerCut = Math.Clamp((HungerCut + addi) * mult, 0.0005f, 0.01f);
            //以上是饱食度削减速度变化
            if (HungerCount < 5)
            {
                HungerCount = 5;
                Hunger = 100.5f;
            }
            HungerMax = HungerCount * 20 + 0.5f;
            if (HungerCount > 20)
            {
                HungerMax = (HungerCount - 20) * 5 + 400.5f;
            }
            HungerMax -= HungerReduce;
            //if (Hunger >= HungerMax)
            //{
            //    Hunger = HungerMax;//注意更新顺序
            //}

            Hunger -= Helper.ModeNum(1, 1.5f, 2) * HungerCut;

            int type = -1;
            if (Hunger <= 0)
            {
                Hunger = 0;
                type = ModContent.BuffType<饿瘪了>();
                if (HungerSetting.HungerDown && !HungerBook)
                {
                    type = 334;//原版能饿死的buff
                }
            }
            else if (HungerSetting.ForOne)
            {
                if (Hunger > HungerMax) type = ModContent.BuffType<吃撑>();
                else if (Hunger < 20) type = ModContent.BuffType<饥饿>();//原版饥饿333会持续发言
                else if (Hunger < 100) { }
                else if (Hunger < 180) type = ModContent.BuffType<一级饱和>();
                else if (Hunger < 260) type = ModContent.BuffType<二级饱和>();
                else if (Hunger < 340) type = ModContent.BuffType<三级饱和>();
                else if (Hunger < 420) type = ModContent.BuffType<强效饱和>();
                else if (Hunger <= 500.5f) type = ModContent.BuffType<超级饱和>();
            }
            else
            {
                if (Hunger > HungerMax) type = ModContent.BuffType<吃撑>();
                else if (Hunger < 20) type = ModContent.BuffType<饥饿>();
                else if (Hunger < 40) { }
                else if (Hunger < 60) type = ModContent.BuffType<一级饱和>();
                else if (Hunger < 80) type = ModContent.BuffType<二级饱和>();
                else if (Hunger <= 100.5f) type = ModContent.BuffType<三级饱和>();
            }
            HungerClear(HungerBuff, type);
            if (type >= 0) HungerBuffAdd(type);
            if (Player.HasBuff(334))
            {
                if (HungerKillTime < 18000) HungerKillTime++;
            }
            else
            {
                HungerKillTime = 0;
            }
            //臭臭
            if (PoopTime > 0) PoopTime--;
        }
    }
    public override void UpdateBadLifeRegen()
    {
        if (Player.HasBuff(ModContent.BuffType<饿瘪了>())) Player.lifeRegen = 0;
    }
    public override void UpdateDead()
    {
        //int hungerMax;
        //if (HungerCount > 20)
        //{
        //    hungerMax = (HungerCount - 20) * 5;
        //}
        //else
        //{
        //    hungerMax = HungerCount * 20;
        //}
        //int max = hungerMax - HungerReduce;
        //if (Hunger < 100) Hunger = 100;
        //if (max < 100) Hunger = max;
    }
    public virtual void HungerClear(int[] bufftype, int thisbufftype)
    {
        int[] buffs = Player.buffType;
        for (int i = buffs.Length - 1; i >= 0; i--)
        {
            if (thisbufftype == buffs[i]) continue;
            if (bufftype.Contains(buffs[i]))
            {
                Player.DelBuff(i);
            }
        }
        /*for (int i = 0; i < bufftype.Length; i++)
        {
            if (Player.HasBuff(bufftype[i]) || bufftype[i] != thisbufftype)
            {
                Player.ClearBuff(bufftype[i]);
            }
        }*/
    }
    public virtual void HungerBuffAdd(int type)
    {
        if (Player.HasBuff(type))
        {
            int i = Player.FindBuffIndex(type);
            Player.buffTime[i] = 2;
        }
        else
        {
            Player.AddBuff(type, 2);
        }
    }
    public override void PreUpdate()//烤肉篝火判定
    {
        int r = 1700;
        int tiler = r / 16;
        int x = (int)(Player.Center.X / 16);
        int y = (int)(Player.Center.Y / 16);
        for (int i = -tiler; i <= tiler; i++)
        {
            for (int j = -tiler; j <= tiler; j++)
            {
                if (Main.tile[x + i, y + j].TileType == ModContent.TileType<Placeable.Tiles.烤肉篝火>())
                {
                    Player.AddBuff(87, 2, false, false);
                    Player.AddBuff(ModContent.BuffType<饱食之火>(), 2, false, false);
                    break;
                }
            }
        }
        base.PreUpdate();
    }
    public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
    {
        itemsByMod["Terraria"].Add(new Item(ModContent.ItemType<农业百科>()));
    }
}