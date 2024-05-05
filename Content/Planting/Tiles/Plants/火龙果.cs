using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 火龙果 : Plant
    {
        public override bool CanBeReapedBySickle => false;
        protected override short FrameWidth => 18;
        protected override int Height => 3;
        protected override int GrowthRate => 12;
        public override bool CanPick => true;
        protected override bool CanSwayInWind => false;
        protected override bool FlipHorizontally => false;
        protected override int HerbItemType => ItemID.Dragonfruit;
        protected override int SeedItemType => ModContent.ItemType<火龙果种子>();
        protected override void ModifyDropSeedCount(ref int seedItemType, ref int seedItemStack, Player player, PlantStage stage)
        {
            if (stage != PlantStage.Planted)
            {
                seedItemType = ItemID.Cactus;
                seedItemStack = Main.rand.Next(5, 9);
            }
        }
    }
}
