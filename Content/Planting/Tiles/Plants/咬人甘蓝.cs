using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 咬人甘蓝 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override short FrameWidth => 34;
        protected override int GrowthRate => 18;
        protected override bool CanPick => true;
        protected override bool PickJustOneTime => true;
        protected override int HerbItemType => ModContent.ItemType<Items.咬人甘蓝>();
        protected override int SeedItemType => ModContent.ItemType<咬人甘蓝种子>();
        protected override void ModifyTileObjectData()
        {
            TileObjectData.newTile.CoordinateHeights = new[] { 8, 28 };//每格最多显示16
            TileObjectData.newTile.CoordinateWidth = 32;
            TileObjectData.newTile.DrawYOffset = -12;//所以要上升12
        }
        protected override bool ModifyPickDrop(int i, int j, int herbItemStack)
        {
            Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
            int dropcount = 1;
            if (Main.rand.NextBool(10)) dropcount = 2;
            int item = Item.NewItem(new EntitySource_TileBreak(i, j), worldPosition, ModContent.ItemType<咬人甘蓝种子>(), dropcount);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            }
            return true;
        }
    }
}
