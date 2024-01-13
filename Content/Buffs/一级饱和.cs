namespace SAA.Content.Buffs
{
    public class 一级饱和 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("一级饱和");
            // Description.SetDefault("所有属性小幅度提升");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.lightPet[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
            Main.vanityPet[Type] = false;
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if (player.buffTime[buffIndex] < 2)
            {
                player.buffTime[buffIndex] += 2;
            }

            return true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.wellFed = true;
            player.statDefense += 2;
            player.GetCritChance(DamageClass.Generic) += 2;
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
            player.GetKnockback(DamageClass.Summon) += 0.5f;
            player.moveSpeed += 0.2f;
            player.pickSpeed -= 0.05f;
        }
    }
}