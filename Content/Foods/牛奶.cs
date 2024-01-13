using SAA.Content.NPCs;

namespace SAA.Content.Foods
{
    public class 牛奶 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("鲜牛奶");
            // Tooltip.SetDefault("\"泰拉世界居然有牛，嗯，一定不是什么正常的牛\"");
        }
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.maxStack = 99;
            Item.value = Item.sellPrice(0, 0, 2, 0);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.scale = 0.75f;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.useTurn = false;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 18000;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bottle)
                .AddNPC(ModContent.NPCType<奶牛>())
                .Register();
        }
    }
}