using SAA.Content.Breeding.Tiles;
using SAA.Content.Planting.Tiles.Plants;
using SAA.Content.Sys;

namespace SAA.Content.Projectiles;
public class 采集 : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.aiStyle = -1;
        Projectile.friendly = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.alpha = 255;
        Projectile.ignoreWater = true;
        Projectile.timeLeft = 5;
    }
    public override bool ShouldUpdatePosition()
    {
        return false;
    }
    public override void AI()
    {
        if (Projectile.owner == Main.myPlayer)
        {
            Projectile.Center = Main.MouseWorld;
            Player player = Main.LocalPlayer;
            if (!player.active || player.dead)
            {
                Projectile.Kill();
            }
            int minX = Projectile.Hitbox.X / 16;
            int maxX = (Projectile.Hitbox.X + Projectile.Hitbox.Width) / 16 + 1;
            int minY = Projectile.Hitbox.Y / 16;
            int maxY = (Projectile.Hitbox.Y + Projectile.Hitbox.Height) / 16 + 1;
            Utils.ClampWithinWorld(ref minX, ref minY, ref maxX, ref maxY);
            for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.HasTile)
                    {
                        if (TileLoader.GetTile(tile.TileType) is Plant plant)
                        {
                            if (plant.CanBeReapedBySickle)
                            {
                                if (HungerSetting.GrownCut)
                                {
                                    if (plant.GetStage(i, j) == PlantStage.Grown)
                                    {
                                        player.PickTile(i, j, 10000);
                                    }
                                }
                                else
                                {
                                    player.PickTile(i, j, 10000);
                                }
                            }
                            else if (plant.CanPick)// && !plant.PickJustOneTime)
                            {
                                if (HungerSetting.GrownCut)
                                {
                                    if (plant.GetStage(i, j) == PlantStage.Grown)
                                    {
                                        plant.TryPick(i, j);
                                    }
                                }
                                else
                                {
                                    plant.TryPick(i, j);
                                }
                            }
                        }
                        else if (TileLoader.GetTile(tile.TileType) is Breed breed)
                        {
                            if (breed.GetStage(i, j) == BreedStage.Product)
                            {
                                breed.TryPickOrFeed(i, j);
                            }
                        }
                    }
                }
            }
        }
    }
}
