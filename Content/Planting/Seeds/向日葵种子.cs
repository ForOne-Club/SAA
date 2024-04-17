using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Planting.Seeds
{
    public class 向日葵种子 : 种子袋
    {
        protected override int TileType => ModContent.TileType<向日葵>();
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.value = 500;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine text = new(Mod, "描述", Language.GetTextValue("Mods.SAA.Tooltips.3"));
            tooltips.Insert(tooltips.Count - 1, text);
        }
        public override bool CanUseItem(Player player)
        {
            int i = Player.tileTargetX, j = Player.tileTargetY;
            Tile tile = Main.tile[i, j + 1];
            if (tile.HasTile && tile.TileType == TileID.Grass && !Main.tile[i, j].HasTile)
            {
                return true;
            }
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe(3)
            .AddIngredient(ItemID.Sunflower)
            .Register();
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<Foods.瓜子>())
            .AddCondition(Condition.NearWater)
            .Register();
        }
    }
}