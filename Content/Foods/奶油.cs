using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 奶油 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("奶油");
            // Tooltip.SetDefault("\"简直是甜品爱好者的福音！\"");
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 24;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 4, 0);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.EatFood;
            // 喝药的声音
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = 206;
            Item.buffTime = 18000;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 2);
            recipe.AddTile(ModContent.TileType<牛奶发酵桶>());
            recipe.Register();
        }
    }
}