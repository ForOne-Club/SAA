using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 蓝莓种子 : Seed
    {
        protected override int TileType => ModContent.TileType<蓝莓>();
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Foods.蓝莓>()
            .Register();
            CreateRecipe()
            .AddIngredient(ItemID.BlueBerries)
            .AddIngredient(ItemID.VileMushroom)
            .Register();
            CreateRecipe()
            .AddIngredient(ItemID.BlueBerries)
            .AddIngredient(ItemID.ViciousMushroom)
            .Register();
        }
    }
}
