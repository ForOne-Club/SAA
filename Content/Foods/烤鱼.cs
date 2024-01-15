using SAA.Content.Placeable.Tiles;
using Terraria.Enums;

namespace SAA.Content.Foods
{
    public class 烤鱼 : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToFood(34, 34, BuffID.WellFed, 28800);
            Item.SetShopValues(ItemRarityColor.Blue1, Item.sellPrice(0, 0, 5, 0));
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bass, 1);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.Trout, 1);
            recipe1.AddTile(ModContent.TileType<烤肉篝火>());
            recipe1.ReplaceResult(this);
            recipe1.Register();
        }
    }
}