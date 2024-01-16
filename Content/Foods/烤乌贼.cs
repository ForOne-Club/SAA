using SAA.Content.Placeable.Tiles;
using Terraria.Enums;

namespace SAA.Content.Foods
{
    public class 烤乌贼 : CanHoldAndPlaceFood
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
            Item.SetOriginFood(36, 36, 206, 18000);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<乌贼>(), 1);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}