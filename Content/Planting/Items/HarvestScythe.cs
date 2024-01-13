using SAA.Content.Planting.System;

namespace SAA.Content.Planting.Items
{
    public class HarvestScythe : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Sickle);
            Item.useTime /= 2;
            Item.useAnimation /= 2;
            Item.damage *= 2;
        }
        public override void MeleeEffects(Player player, Rectangle itemRectangle)
        {
            int minX = itemRectangle.X / 16;
            int maxX = (itemRectangle.X + itemRectangle.Width) / 16 + 1;
            int minY = itemRectangle.Y / 16;
            int maxY = (itemRectangle.Y + itemRectangle.Height) / 16 + 1;
            Utils.ClampWithinWorld(ref minX, ref minY, ref maxX, ref maxY);
            for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {
                    Tile tile = Main.tile[i, j];
                    if (tile.HasTile && PlantData.HasPlant(tile, out int grow) && tile.TileFrameX == grow * 18)
                    {
                        WorldGen.KillTile(i, j);
                        int item = Item.NewItem(player.GetSource_ItemUse(Item), new Vector2(i, j) * 16, PlantData.GetCrop(tile));
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item);
                            NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, i, j);
                        }
                    }
                }
            }
        }
    }
}
