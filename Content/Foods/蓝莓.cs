using Terraria.Enums;

namespace SAA.Content.Foods
{
    public class 蓝莓 : CanHoldAndPlaceFood
    {
        protected override void SetFoodDust()
        {
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[2] {
                Color.Purple,
                Color.Blue,
            };
        }
        public override void SetDefaults()
        {
            Item.DefaultToFood(26, 28, BuffID.WellFed, 18000);
            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(0, 0, 20, 0));
        }
    }
}