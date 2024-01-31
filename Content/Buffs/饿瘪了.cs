namespace SAA.Content.Buffs
{
    public class 饿瘪了 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("饿瘪了");
            // Description.SetDefault("就快饿死了");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.hungry = true;
            player.statDefense -= 4;
            player.GetCritChance(DamageClass.Generic) -= 4;
            player.GetDamage(DamageClass.Generic) -= 0.1f;
            player.GetAttackSpeed(DamageClass.Melee) -= 0.1f;
            player.GetKnockback(DamageClass.Summon) -= 1f;
            player.pickSpeed += 0.15f;
        }
    }
}