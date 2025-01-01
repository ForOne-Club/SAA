using SAA.Content.Sys;

namespace SAA.Content.Buffs
{
    public class 吃撑 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        { 
            var hp = player.GetModPlayer<HungerforPlayer>();
            float k = 1 - hp.Hunger/ hp.HungerMax;
            if (k < -0.5f) k = -0.5f;//限制上限
            player.GetDamage(DamageClass.Generic) += k;
            player.GetAttackSpeed(DamageClass.Melee) += 0.5f * k;
            player.pickSpeed -= k;
            player.moveSpeed += k;
        }
    }
}