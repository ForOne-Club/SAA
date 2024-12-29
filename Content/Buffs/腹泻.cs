namespace SAA.Content.Buffs
{
    public class 腹泻 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.hungry = true;
            player.statDefense -= 2;
            player.GetCritChance(DamageClass.Generic) -= 2;
            player.GetDamage(DamageClass.Generic) -= 0.05f;
            player.GetAttackSpeed(DamageClass.Melee) -= 0.05f;
            player.GetKnockback(DamageClass.Summon) -= 0.5f;
            player.pickSpeed += 0.05f;
        }
    }
}