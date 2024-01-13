using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 烤鱼 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("烤鱼");
        }
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 5, 0);
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