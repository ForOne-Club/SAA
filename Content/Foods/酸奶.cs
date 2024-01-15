using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 酸奶 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("酸奶");
            // Tooltip.SetDefault("\"放心喝，它不会帮助消化\"");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 22;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 5, 50);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            // 喝药的声音
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 9000;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 1);
            recipe.AddTile(ModContent.TileType<牛奶发酵桶>());
            recipe.Register();
        }
    }
}