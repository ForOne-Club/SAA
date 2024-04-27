namespace SAA.Content.Sys
{
    public class HungerforProj : GlobalProjectile
    {
        public override void PostAI(Projectile projectile)
        {
            if (projectile.type == 1019)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (projectile.ai[0] == 1)
                    {
                        projectile.ai[0] = 2;
                        Helper.Fertilize(projectile.width, projectile.Center, projectile.velocity, 2);
                    }
                }
            }
        }
    }
}
