namespace SAA.Content.Pets
{
    public class 短爬兽宝宝 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("短爬兽宝宝");
            Main.projFrames[Projectile.type] = 3;
            Main.projPet[Projectile.type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(112); // Copy the stats of the Zephyr Fish
            Projectile.width = 46;
            Projectile.height = 30;
            AIType = 112; // Copy the AI of the Zephyr Fish.
            Projectile.scale = 0.7f;
        }
        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.penguin = false; // Relic from aiType
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            //Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(ModContent.BuffType<短爬兽Buff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}