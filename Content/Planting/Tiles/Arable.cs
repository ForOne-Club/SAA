using ReLogic.Content;
using SAA.Content.Planting.Tiles.Plants;
using static SAA.Content.Planting.System.PlowlandSystem;

namespace SAA.Content.Planting.Tiles
{
    public class Arable : ModTile
    {
        internal static Texture2D tex;
        internal static Texture2D tex_wet;
        internal virtual int[] LiquidDeflectTile => new int[] { TileID.Dirt, TileID.Grass, Type };
        public override void Load()
        {
            tex = ModContent.Request<Texture2D>(GetType().Namespace.Replace(".", "/") + "/Arable", AssetRequestMode.ImmediateLoad).Value;
            tex_wet = ModContent.Request<Texture2D>(GetType().Namespace.Replace(".", "/") + "/ArableWet", AssetRequestMode.ImmediateLoad).Value;
        }
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            DustType = DustID.Dirt;
            this.RegisterTile(Color.Brown);
            RegisterItemDrop(ItemID.DirtBlock);
        }
        public override void RandomUpdate(int i, int j)
        {
            Tile tile = Main.tile[i, j - 1];
            if (tile.HasTile && TileLoader.GetTile(tile.TileType) is not Plant)//实体方块
            {
                if (wet.Contains((i, j))) wet.Remove((i, j));
                WorldGen.PlaceTile(i, j, TileID.Dirt, false, true);
            }
            List<Point> ignoreTiles = new List<Point>();
            if (wet.Contains((i, j)))
            {
                if (!CanLinkByLiquid(ref ignoreTiles, i, j, LiquidDeflectTile))
                {
                    wet.Remove((i, j));
                }
            }
            else//没湿
            {
                if (CanLinkByLiquid(ref ignoreTiles, i, j, LiquidDeflectTile))
                {
                    wet.Add((i, j));
                }
            }
        }
        public static bool CanLinkByLiquid(ref List<Point> ignoreTiles, int i, int j, int[] linkTileType, int maxDistance = 4, int liquidType = LiquidID.Water)
        {
            ignoreTiles.Add(new Point(i, j));
            Point origin = ignoreTiles[0];
            for (int m = -1; m <= 1; m++)
            {
                Point point = new Point(i + m, j);
                if (!ignoreTiles.Contains(point) && Math.Abs(point.X - origin.X) <= maxDistance && Math.Abs(point.Y - origin.Y) <= maxDistance)
                {
                    Tile tile = Main.tile[i + m, j];
                    if (tile.LiquidAmount > 0 && tile.LiquidType == liquidType) return true;
                    else if (tile.HasTile && linkTileType.Contains(tile.TileType))
                    {
                        if (CanLinkByLiquid(ref ignoreTiles, i + m, j, linkTileType, maxDistance, liquidType))
                            return true;
                    }
                }
            }
            for (int n = -1; n <= 1; n++)
            {
                Point point = new Point(i, j + n);
                if (!ignoreTiles.Contains(point) && Math.Abs(point.X - origin.X) <= maxDistance && Math.Abs(point.Y - origin.Y) <= maxDistance)
                {
                    Tile tile = Main.tile[i, j + n];
                    if (tile.LiquidAmount > 0 && tile.LiquidType == liquidType) return true;
                    else if (tile.HasTile && linkTileType.Contains(tile.TileType))
                    {
                        if (CanLinkByLiquid(ref ignoreTiles, i, j + n, linkTileType, maxDistance, liquidType))
                            return true;
                    }
                }
            }
            return false;
        }
        public override bool CanDrop(int i, int j) => true;
        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            yield return new Item(ItemID.DirtBlock);
        }
        public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
            drawData.drawTexture = wet.Contains((i, j)) ? tex_wet : tex;
        }
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            wet.Remove((i, j));
        }
    }
}
