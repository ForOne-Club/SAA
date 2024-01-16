using SAA.Content.Placeable.Tiles;
using Terraria.ID;

namespace SAA.Content.Foods
{
    public class 烤鱼 : ModItem
    {
        public override void SetDefaults()
        {
            Item.SetOriginFood(34, 34, BuffID.WellFed, 28800);
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Bass, 1)
            .AddTile(ModContent.TileType<烤肉篝火>())
            .Register();
            CreateRecipe()
            .AddIngredient(ItemID.Trout, 1)
            .AddTile(ModContent.TileType<烤肉篝火>())
            .Register();
            CreateRecipe()
            .AddIngredient(ItemID.AtlanticCod, 1)
            .AddTile(ModContent.TileType<烤肉篝火>())
            .Register();
        }
    }
}