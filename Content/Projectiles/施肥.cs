using System.IO;

namespace SAA.Content.Projectiles;
public class 施肥 : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 64;
        Projectile.height = 64;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 180;
    }
    public override void AI()
    {
        if (Projectile.ai[0] == 0)
        {
            Projectile.ai[0] = 1;
            int dusttype = 0;
            int count = 40;
            for (int i = 0; i < count; i++)
            {
                Dust dust7 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, dusttype, Projectile.velocity.X, Projectile.velocity.Y, 50)];
                dust7.noGravity = i % 3 != 0;
                if (!dust7.noGravity)
                {
                    Dust dust2 = dust7;
                    dust2.scale *= 1.25f;
                    dust2 = dust7;
                    dust2.velocity /= 2f;
                    dust7.velocity.Y -= 2.2f;
                }
                else
                {
                    Dust dust2 = dust7;
                    dust2.scale *= 1.75f;
                    dust2 = dust7;
                    dust2.velocity += Projectile.velocity * 0.65f;
                }
            }
        }
        if (Main.netMode != NetmodeID.MultiplayerClient)
        {
            int num92 = (int)(Projectile.position.X / 16f) - 1;
            int num93 = (int)((Projectile.position.X + (float)Projectile.width) / 16f) + 2;
            int num94 = (int)(Projectile.position.Y / 16f) - 1;
            int num95 = (int)((Projectile.position.Y + (float)Projectile.height) / 16f) + 2;
            if (num92 < 0)
                num92 = 0;

            if (num93 > Main.maxTilesX)
                num93 = Main.maxTilesX;

            if (num94 < 0)
                num94 = 0;

            if (num95 > Main.maxTilesY)
                num95 = Main.maxTilesY;

            Vector2 vector15 = default(Vector2);
            for (int num96 = num92; num96 < num93; num96++)
            {
                for (int num97 = num94; num97 < num95; num97++)
                {
                    vector15.X = num96 * 16;
                    vector15.Y = num97 * 16;
                    if (!(Projectile.position.X + (float)Projectile.width > vector15.X) || !(Projectile.position.X < vector15.X + 16f) || !(Projectile.position.Y + (float)Projectile.height > vector15.Y) || !(Projectile.position.Y < vector15.Y + 16f) || !Main.tile[num96, num97].HasTile)
                        continue;

                    Tile tile = Main.tile[num96, num97];
                    if (tile.TileType >= 0 && tile.TileType < TileID.Count && TileID.Sets.CommonSapling[tile.TileType])
                    {
                        if (Main.remixWorld && num97 >= (int)Main.worldSurface - 1 && num97 < Main.maxTilesY - 20)
                            WorldGen.AttemptToGrowTreeFromSapling(num96, num97, underground: false);

                        WorldGen.AttemptToGrowTreeFromSapling(num96, num97, num97 > (int)Main.worldSurface - 1);
                    }
                }
            }
            if (Projectile.ai[0] == 1)
            {
                Projectile.ai[0] = 2;
                Helper.Fertilize(Projectile.width, Projectile.Center, Projectile.velocity, (int)Projectile.ai[1]);
            }
        }
        Projectile.velocity *= 0.95f;
        if (Projectile.velocity.Length() < 0.5f) Projectile.Kill();
    }
}
