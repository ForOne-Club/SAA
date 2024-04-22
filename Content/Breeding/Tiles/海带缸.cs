using SAA.Content.Foods;

namespace SAA.Content.Breeding.Tiles
{
    public class 海带缸 : Breed
    {
        protected override int Height => 3;
        protected override int GrowthRate => 16;
        protected override int NeedItemType => -1;
        protected override int ProductItemType => ModContent.ItemType<海带>();
        protected override int DropItemType => ModContent.ItemType<Items.海带缸>();
    }
}
