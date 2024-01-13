using SAA.Content.Planting.Tiles;

namespace SAA.Content.Planting.Items
{
    public class Hoe : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.StaffofRegrowth);
            Item.createTile = -1;
        }
        public override bool? UseItem(Player player)
        {
            int i = Player.tileTargetX, j = Player.tileTargetY;
            Tile tile = Main.tile[i, j];
            if (tile.HasTile && tile.TileType is TileID.Dirt or 2)
            {
                WorldGen.KillTile(i, j, noItem: true);
                WorldGen.PlaceTile(i, j, ModContent.TileType<Arable>());
                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, i, j);
            }
            return base.UseItem(player);
        }
    }
}
