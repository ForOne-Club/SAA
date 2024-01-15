using SAA.Content.Placeable.Tiles;

namespace SAA.Content.Foods
{
    public class 奶酪 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("奶酪");
            // Tooltip.SetDefault("\"某些人可能不太喜欢它的味道\"");
        }
        public override void SetDefaults()
        {
            Item.width = 46;
            Item.height = 26;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 0, 30);
            Item.rare = ItemRarityID.Green;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.7f;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.UseSound = SoundID.Item2;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 90000;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<牛奶>(), 5);
            recipe.AddTile(ModContent.TileType<牛奶发酵桶>());
            recipe.Register();
        }
    }
}