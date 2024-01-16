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
            Item.SetOriginFood(26, 28, 26, 18000);
        }
    }
}