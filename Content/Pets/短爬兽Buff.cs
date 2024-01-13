namespace SAA.Content.Pets
{
    public class 短爬兽Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("短爬兽宝宝");
            // Description.SetDefault("短爬兽宝宝将跟着你");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        { // This method gets called every frame your buff is active on your player.
            player.buffTime[buffIndex] = 18000;
            int projType = ModContent.ProjectileType<短爬兽宝宝>();
            //If the player is local, and there hasn't been a pet projectile spawned yet - spawn it.
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[projType] <= 0)
            {
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, Vector2.Zero, projType, 0, 0f, player.whoAmI);
            }
        }
    }
}