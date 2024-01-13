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
    }
}