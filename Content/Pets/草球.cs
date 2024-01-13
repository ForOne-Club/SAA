using Terraria.GameContent.Creative;
namespace SAA.Content.Pets
{
    public class 草球 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("草球");
            // Tooltip.SetDefault("成年短爬兽会将大量的食物做成球状交给即将成年的短爬兽宝宝，这表示催促它离开父母独自生活。" + "\n召唤异色短爬兽宝宝");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.ZephyrFish); // Copy the Defaults of the Zephyr Fish Item.
            Item.shoot = ModContent.ProjectileType<短爬兽宝宝>(); // "Shoot" your pet projectile.
            Item.buffType = ModContent.BuffType<短爬兽Buff>(); // Apply buff upon usage of the Item.
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.AddBuff(Item.buffType, 3600);
        }
    }
}