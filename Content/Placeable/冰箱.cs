using Terraria.ID;

namespace SAA.Content.Placeable
{
    public class 冰箱 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.maxStack = 9999;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.createTile = ModContent.TileType<Tiles.冰箱>();
            Item.rare = ItemRarityID.Blue;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IceBlock, 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}