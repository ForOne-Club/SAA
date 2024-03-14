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
            float k = player.GetModPlayer<HungerforPlayer>().HungerMax - player.GetModPlayer<HungerforPlayer>().Hunger;
            player.GetDamage(DamageClass.Generic) += 0.01f * k;
            player.GetAttackSpeed(DamageClass.Melee) += 0.005f * k;
            player.pickSpeed -= 0.01f * k;
            player.moveSpeed += 0.01f * k;
        }
    }
}