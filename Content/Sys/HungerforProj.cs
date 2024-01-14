using SAA.Content.Planting.System;
using SAA.Content.Planting.Tiles;
using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Sys
{
    public class HungerforProj : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.type == 1019)
            {
                Rectangle hitbox = projectile.Hitbox;
                int minX = hitbox.X / 16;
                int maxX = (hitbox.X + hitbox.Width) / 16 + 1;
                int minY = hitbox.Y / 16;
                int maxY = (hitbox.Y + hitbox.Height) / 16 + 1;
                Utils.ClampWithinWorld(ref minX, ref minY, ref maxX, ref maxY);
                for (int i = minX; i < maxX; i++)
                {
                    for (int j = minY; j < maxY; j++)
                    {
                        Tile tile = Framing.GetTileSafely(i, j);
                        if (tile.HasTile && TileLoader.GetTile(tile.TileType) is Plant)
                        {
                            PlantStage stage = Plant.GetStage(i, j);
                            Tile land = Framing.GetTileSafely(i, j + 1);
                            bool flag = land.TileType == ModContent.TileType<Arable>();
                            if (land.HasTile && flag)
                            {
                                if (PlowlandSystem.wet.Contains((i, j + 1)))
                                {
                                    if (stage != PlantStage.Grown && Main.rand.NextBool(3))
                                    {
                                        tile.TileFrameX += Plant.FrameWidth;
                                        if (tile.TileFrameY > 0) Main.tile[i, j - 1].TileFrameX += Plant.FrameWidth;
                                        if (Main.netMode != NetmodeID.SinglePlayer)
                                        {
                                            NetMessage.SendTileSquare(-1, i, j, 1);
                                            if (tile.TileFrameY > 0) NetMessage.SendTileSquare(-1, i, j - 1, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
