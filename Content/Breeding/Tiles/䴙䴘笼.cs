namespace SAA.Content.Breeding.Tiles
{
    public class 䴙䴘笼 : Breed
    {
        protected override int Height => 3;
        protected override int GrowthRate => 7;
        protected override bool NeedSun => false;
        protected override int DropItemType => ModContent.ItemType<Items.䴙䴘笼>();
    }
}
