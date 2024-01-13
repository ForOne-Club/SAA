namespace SAA.Content.Buffs
{
    public class 酸甜可口 : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("酸甜可口");
            // Description.SetDefault("多吃酸的帮助消化");
            Main.buffNoSave[Type] = false;
            Main.debuff[Type] = true;
        }
    }
}