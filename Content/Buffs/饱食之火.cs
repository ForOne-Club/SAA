namespace SAA.Content.Buffs
{
    public class 饱食之火 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("饱食之火");
            // Description.SetDefault("饱食度下降略有减缓");
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
        }
    }
}