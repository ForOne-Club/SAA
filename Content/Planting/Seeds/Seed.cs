using SAA.Content.Planting.Tiles;
using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public abstract class Seed : ModItem
    {
        protected virtual int Width => 18;
        protected virtual int Height => 18;
        protected virtual int TileType => ModContent.TileType<海麦作物>();
        public override void SetDefaults()
        {
            Item.width = Width;
            Item.height = Height;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.createTile = TileType;
            Item.placeStyle = 0;
            Item.value = 20;
            Item.autoReuse = true;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine text = new(Mod, "描述", Language.GetTextValue("Mods.SAA.Tooltips.2") + $"{TileLoader.GetTile(TileType).Name}");
            tooltips.Insert(tooltips.Count - 1, text);
        }
        public override bool CanUseItem(Player player)
        {
            int i = Player.tileTargetX, j = Player.tileTargetY;
            Tile tile = Main.tile[i, j + 1];
            if (tile.HasTile && tile.TileType == ModContent.TileType<Arable>() && !Main.tile[i, j].HasTile)
            {
                return true;
            }
            return false;
        }
    }
}
