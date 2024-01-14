namespace SAA.Content.Placeable
{
    public abstract class 篝火物品 : ModItem
    {
        public virtual int placeStyle => 0;
        public virtual int ItemType => 0;
        public override void SetStaticDefaults()
        {
            //Tooltip.SetDefault("自带一个烧烤架的篝火" + "\n靠近篝火时生命再生提速，饱食度下降减速" + "\n比普通的篝火影响范围更广");
        }
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 12;
            Item.height = 12;
            Item.value = Item.sellPrice(0, 0, 0, 0);
            Item.createTile = ModContent.TileType<Tiles.烤肉篝火>();
            Item.placeStyle = placeStyle;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType, 1);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 3);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}