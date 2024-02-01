namespace SAA.Content.Breeding.Tiles
{
    public class 野鸭笼 : Breed
    {
        protected override int Height => 3;
        protected override int GrowthRate => 6;
        protected override int DropItemType => ModContent.ItemType<Items.野鸭笼>();
    }
}
