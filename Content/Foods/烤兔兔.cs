using SAA.Content.Placeable.Tiles;
using Terraria.Enums;
using Terraria.ID;

namespace SAA.Content.Foods
{
    public class 烤兔兔 : CanHoldAndPlaceFood
    {
        protected override void SetFoodDust()
        {
            ItemID.Sets.FoodParticleColors[Item.type] = new Color[2] {
                Color.Yellow,
                Color.Orange,
            };
        }
        public override void SetDefaults()
        {
            Item.SetFood(28, 26, 0, 10);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bunny);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}