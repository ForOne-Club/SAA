using ReLogic.Content;
using SAA.Content.Planting.System;
using static SAA.Content.Planting.System.PlantSystem;

namespace SAA.Content.Planting.Tiles
{
    public class Arable : ModTile
    {
        internal static Texture2D tex;
        internal static Texture2D tex_wet;
        public override void Load()
        {
            tex = ModContent.Request<Texture2D>(GetType().Namespace.Replace(".", "/") + "/Arable", AssetRequestMode.ImmediateLoad).Value;
            tex_wet = ModContent.Request<Texture2D>(GetType().Namespace.Replace(".", "/") + "/ArableWet", AssetRequestMode.ImmediateLoad).Value;
        }
        public override void SetStaticDefaults()
        {
            this.RegisterAsCommonTile(Color.Brown);
            RegisterItemDrop(ItemID.DirtBlock);
        }
        public override void RandomUpdate(int i, int j)
        {
            Predicate<Tile> cd = new(x => x.HasTile && (x.TileType is TileID.Dirt or 2 || x.TileType == ModContent.TileType<Arable>()));
            Predicate<Tile> water = new(x => x.LiquidType == LiquidID.Water && x.LiquidAmount > 0);
            if (Helper.TileChain(cd, water, i, j, i, j, 4))
            {
                if (!wet.Contains((i, j)))
                {
                    wet.Add((i, j));
                }
            }
            else wet.Remove((i, j));
            if (wet.Contains((i, j)))
            {
                Tile plant = Main.tile[i, j - 1];
                if (PlantData.HasPlant(plant, out int grow) && plant.TileFrameX < grow * 18 && PlantData.Growing(plant))
                {
                    plant.TileFrameX += 18;
                }
            }
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
    }
}
