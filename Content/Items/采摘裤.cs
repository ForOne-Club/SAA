using SAA.Content.Sys;

namespace SAA.Content.Items;

[AutoloadEquip(EquipType.Legs)]
public class 采摘裤 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 18;
        Item.value = Item.sellPrice(0, 0, 50, 0);
        Item.rare = 1;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<HungerforPlayer>().CropHarvest += 0.05f;
        player.moveSpeed += 0.05f;
        //player.GetDamage(ModContent.GetInstance<BotanistDamageClass>()) += 1;
    }
    public override void AddRecipes()//需要棉花做的布匹
    {
        CreateRecipe()
        .AddIngredient(ItemID.Silk, 20)
        .AddTile(TileID.Loom)
        .Register();
    }
}
