using SAA.Content.Sys;

namespace SAA.Content.Items
{
    [AutoloadEquip(EquipType.Body)]
    public class 采摘衣 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 18;
            Item.value = Item.sellPrice(0, 0, 55, 0);
            Item.rare = 1;
            Item.defense = 1;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<HungerforPlayer>().CropHarvest += 0.05f;
        }
        public override void AddRecipes()//需要棉花做的布匹
        {
            CreateRecipe()
            .AddIngredient(ItemID.Silk, 25)
            .AddTile(TileID.Loom)
            .Register();
        }
    }
}
