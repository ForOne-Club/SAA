using SAA.Content.Sys;

namespace SAA.Content.Accessory
{
    public class 求生手册 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("求生手册");
            // Tooltip.SetDefault("免疫虚弱和中毒" + "\n你会饿的更慢而且不会被饿死" + "\n允许玩家切割藤蔓以获取藤蔓绳" + "\n\"鬼知道你吃了些什么\"");
        }
        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 36;
            Item.accessory = true;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 2, 0, 0);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<HungerforPlayer>().HungerBook = true;
        }
    }
}