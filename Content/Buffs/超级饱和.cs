namespace SAA.Content.Buffs
{
    public class 超级饱和 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("超级饱和");
            // Description.SetDefault("所有属性巨大幅度提升");
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
            player.lifeRegen += 2;
            player.statDefense += 6;
            player.GetCritChance(DamageClass.Generic) += 6;
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
            //player.minionKB += 1.5f;
            player.GetKnockback(DamageClass.Summon) += 1.5f;
            player.moveSpeed += 0.6f;
            player.pickSpeed -= 0.25f;
        }
    }
}