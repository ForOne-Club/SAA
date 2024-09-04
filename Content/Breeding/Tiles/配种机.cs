using SAA.Content.Foods;

namespace SAA.Content.Breeding.Tiles;
public class 配种机 : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileObsidianKill[Type] = true;
        Main.tileNoFail[Type] = true;
        //TileID.Sets.ReplaceTileBreakUp[Type] = true;
        TileID.Sets.IgnoredInHouseScore[Type] = true;
        TileID.Sets.IgnoredByGrowingSaplings[Type] = true;
        this.RegisterTile(Color.Brown);
        TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16, 16 };
        TileObjectData.addTile(Type);
        HitSound = SoundID.Tink;
        DustType = DustID.WoodFurniture;
    }
}
