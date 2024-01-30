using SAA.Content.Planting.Tiles;

namespace SAA.Content.Items
{
    public class 锄头 : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Melee;
            Item.width = 34;
            Item.height = 34;
            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1f;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.scale = 1f;
        }
        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                int i = Player.tileTargetX, j = Player.tileTargetY;
                Tile tile = Main.tile[i, j];
                if (tile.HasTile && tile.TileType is TileID.Dirt or 2)
                {
                    WorldGen.KillTile(i, j, noItem: true);
                    WorldGen.PlaceTile(i, j, ModContent.TileType<Arable>(), true, false, player.whoAmI);
                    //WorldGen.SquareTileFrame(i, j);
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, i, j, ModContent.TileType<Arable>());
                }
            }
            return true;
        }
    }
}
