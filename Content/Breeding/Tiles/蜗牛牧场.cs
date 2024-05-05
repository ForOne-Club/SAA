using SAA.Content.Foods;

namespace SAA.Content.Breeding.Tiles;
public class 蜗牛牧场 : Breed
{
    protected override int Width => 6;
    protected override int Height => 4;
    protected override bool NeedSun => false;
    protected override int GrowthRate => 10;
    protected override int[] NeedItemType => new int[] { ItemID.Hay };
    protected override int ProductItemType => ModContent.ItemType<牛奶>();
    protected override int DropItemType => ModContent.ItemType<Items.蜗牛牧场>();
    protected override void ModifyTileObjectData()
    {
        TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
    }
}
