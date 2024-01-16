using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 烤蛆虫 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烤蛆虫");
        }
        public override void SetDefaults()
        {
            Item.SetOriginFood(40, 22, 26, 28800);
            Item.height = 22;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 4, 0);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 28800;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Grubby, 2);
            recipe.AddTile(ModContent.TileType<烤肉篝火>());
            recipe.Register();
        }
    }
}