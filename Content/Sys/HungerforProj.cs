namespace SAA.Content.Sys
{
    public class HungerforProj : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.type == 1019)
            {
                Helper.Fertilize(projectile.width, projectile.Center, projectile.velocity, 2);
            }
        }
    }
}
