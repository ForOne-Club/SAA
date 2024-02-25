using SAA.Content.Sys;
using Terraria.ID;

namespace SAA.Content.Items;

[AutoloadEquip(EquipType.Head)]
public class 草帽 : ModItem
{
    public override void SetStaticDefaults()
    {
        // ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // 根本不要画头部。被太空生物面具使用
        ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // 画头发，就好像一顶帽子盖住了头顶。巫师帽使用
        // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // 画出所有的头发。使用Mime面具，太阳镜
        // ArmorIDs.Head.Sets.DrawBackHair[Item.headSlot] = true;
        // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true; 
    }
    public override void SetDefaults()
    {
        Item.width = 24;
        Item.height = 14;
        Item.value = Item.sellPrice(0, 0, 50, 0);
        Item.rare = 1;
    }
    public override void UpdateEquip(Player player)
    {
        player.GetModPlayer<HungerforPlayer>().CropHarvest += 0.1f;
    }
    public override void AddRecipes()
    {
        CreateRecipe()
        .AddIngredient(ItemID.Hay, 50)
        .AddTile(TileID.WorkBenches)
        .Register();
    }
    //public override bool IsArmorSet(Item head, Item body, Item legs)
    //{
    //    return body.type == YZIDs.Armor.Body_GA && legs.type == YZIDs.Armor.Legs_GA;
    //}
    //public override void UpdateArmorSet(Player player)
    //{
    //    player.setBonus = "最大召唤栏+1\n" + "召唤伤害增加120%\n" + "鞭子范围增加20%\n增加80最大护盾";
    //    player.maxMinions++;
    //    player.GetDamage<SummonDamageClass>() += 1.2f;
    //    player.whipRangeMultiplier += 0.2f;
    //    var spp = player.SPP();
    //    spp.PowerShieldMax += 80;
    //    int max = spp.SpiritualPowerMax / 10;
    //    if (max >= 8)
    //        player.GetDamage(DamageClass.Summon) += 0.2f;
    //    if (max >= 9)
    //        spp.PowerShieldMax += 20;
    //    if (max >= 10)
    //        player.GetDamage(DamageClass.Summon) += 0.6f;
    //}
    //public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
    //{
    //    glowMask = ItemHelper.FindGlowMask(Texture + "_Head_Glow");
    //    glowMaskColor = Color.White;
    //}
}
