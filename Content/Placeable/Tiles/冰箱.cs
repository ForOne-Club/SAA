using SAA.Content.Base;

namespace SAA.Content.Placeable.Tiles
{
    public class 冰箱 : BaseChestTile
    {
        public override int ItemType => ModContent.ItemType<Placeable.冰箱>();

        public override void SetDust(ref int dustType)
        {
            dustType = DustID.Platinum;
        }
    }
}