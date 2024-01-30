namespace SAA.Content.Breeding.Items;
public abstract class PlaceItem : ModItem
{
    protected virtual int Width => 48;
    protected virtual int Height => 48;
    protected virtual int TileType => ModContent.TileType<Tiles.鸭笼>();
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
        Item.value = 1000;
        Item.autoReuse = true;
    }
}
