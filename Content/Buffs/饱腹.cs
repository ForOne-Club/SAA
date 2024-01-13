namespace SAA.Content.Buffs
{
    public class 饱腹 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("饱腹");
            // Description.SetDefault("饱食度下降小幅度减缓" + "\n\"有点撑\"");
            Main.buffNoSave[Type] = false;
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time / 2;
            return true;
        }
    }
}