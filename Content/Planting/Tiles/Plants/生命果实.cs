using SAA.Content.Foods;
using SAA.Content.Planting.Seeds;

namespace SAA.Content.Planting.Tiles.Plants
{
    public class 生命果实 : Plant
    {
        protected override int GrowthRate => 5;
        protected override int HerbItemType => ModContent.ItemType<迷你生命果>();
        protected override int SeedItemType => ModContent.ItemType<生命果实种子>();
        protected override void ModifyDropHerbCount(ref int herbItemStack, Player player, PlantStage stage)
        {
            if (stage == PlantStage.Grown)
            {
                herbItemStack = 1;
            }
        }
    }
}
