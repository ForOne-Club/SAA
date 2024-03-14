using SAA.Content.DamageClasses;
using System.IO;
using Terraria.GameContent;

namespace SAA.Content.Projectiles;
public class 咬人甘蓝 : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 4;
    }
    public override void SetDefaults()
    {
        Projectile.width = 28;
        Projectile.height = 28;
        Projectile.timeLeft = 3600;
        Projectile.tileCollide = true;
        Projectile.friendly = true;
        Projectile.ignoreWater = false;
        Projectile.penetrate = 5;
        Projectile.Opacity = 0;
        Projectile.DamageType = ModContent.GetInstance<BotanistDamageClass>();
        Projectile.rotation = Main.rand.NextFloat(MathHelper.Pi);
    }
    public override bool? CanDamage()
    {
        return Projectile.frame == 2 || Projectile.ai[0] > 0;
    }
    public override void AI()
    {
        Projectile.Opacity = MathHelper.Lerp(0, 30, Projectile.timeLeft);
        Projectile.velocity.Y += 0.15f;
        Projectile.rotation += Projectile.velocity.X * 0.1f;
        Projectile.localAI[0] = 0;
        if (Projectile.ai[0] <= 0)
        {
            float distance = 500f;
            NPC target = null;
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy(Projectile))
                {
                    float D = Vector2.Distance(Projectile.Center, npc.Center);
                    if (D < distance)
                    {
                        distance = D;
                        target = npc;
                    }
                }
            }
            if (target != null)
            {
                float k = target.Center.X - Projectile.Center.X;
                if (Math.Abs(k) > 50)
                {
                    k = Math.Abs(k) / k / 5;
                    Projectile.velocity.X += k;
                    if (Math.Abs(Projectile.velocity.X) > 6) Projectile.velocity.X = Math.Abs(Projectile.velocity.X) / Projectile.velocity.X * 6;
                    Projectile.localAI[0] = 1;
                }
                else
                {
                    float l = target.Center.Y - Projectile.Center.Y;
                    if (l < 0)
                    {
                        if (Projectile.ai[1] == 1)
                        {
                            Projectile.velocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.One) * 7;
                            Projectile.ai[1] = 0;
                            Projectile.netUpdate = true;
                        }
                    }
                }
                int w = target.width;
                int h = target.height;
                float td = (float)Math.Sqrt(w * w + h * h);
                if (distance < (50 + td))
                {
                    if (distance < (25 + td)) Projectile.frame = 2;
                    else Projectile.frame = 1;
                }
            }
        }
        else
        {
            NPC target = Main.npc[(int)Projectile.ai[0] - 1];
            if (target == null || !target.CanBeChasedBy(Projectile))
            {
                Projectile.Kill();
                return;
            }
            Projectile.velocity *= 0;
            Projectile.frame = 3;
            Projectile.Center = target.Center + new Vector2(Projectile.ai[2], Projectile.localAI[2]);
            Projectile.rotation = (target.Center - Projectile.Center).ToRotation();
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        if (oldVelocity.Y != Projectile.velocity.Y)
        {
            Projectile.velocity.Y = -oldVelocity.Y * 0.5f;
            Projectile.velocity.X *= 0.5f;
        }
        if (oldVelocity.X != Projectile.position.X && Projectile.localAI[0] == 1)
        {
            Projectile.velocity.Y = -3;
        }
        Projectile.ai[1] = 1;
        Projectile.netUpdate = true;
        return false;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.ai[0] = target.whoAmI + 1;
        Projectile.ai[2] = Projectile.Center.X - target.Center.X;
        Projectile.localAI[2] = Projectile.Center.Y - target.Center.Y;
        Projectile.netUpdate = true;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        Texture2D texture2D = TextureAssets.Projectile[Type].Value;
        int num = texture2D.Height / Main.projFrames[Type];
        int y = num * Projectile.frame;
        Main.spriteBatch.Draw(texture2D, Projectile.Center - Main.screenPosition, new Rectangle?(new Rectangle(0, y, texture2D.Width, num)), lightColor, Projectile.rotation, new Vector2(texture2D.Width / 2f, num / 2f), Projectile.scale, SpriteEffects.None, 0f);
        return false;
    }
    public override void OnKill(int timeLeft)
    {
        if (Main.netMode == NetmodeID.Server) return;
        for (int i = 0; i < Main.rand.Next(3, 7); i++)
        {
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center + Main.rand.NextVector2Unit() * i, Projectile.velocity, GoreID.TreeLeaf_Normal);
        }
    }
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(Projectile.localAI[2]);
    }
    public override void ReceiveExtraAI(BinaryReader reader)
    {
        Projectile.localAI[2] = reader.ReadSingle();
    }
}
