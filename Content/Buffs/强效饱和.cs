namespace SAA.Content.Buffs
{
    public class 强效饱和 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("强效饱和");
            // Description.SetDefault("所有属性很大幅度提升");
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
            player.lifeRegen += 1;
            player.statDefense += 5;
            player.GetCritChance(DamageClass.Generic) += 5;
            player.GetDamage(DamageClass.Generic) += 0.125f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.125f;
            player.GetKnockback(DamageClass.Summon) += 1.25f;
            player.moveSpeed += 0.5f;
            player.pickSpeed -= 0.2f;
        }
    }
}