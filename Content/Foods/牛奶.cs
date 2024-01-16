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
            Item.SetOriginFood(20, 26, 26, 18000, true);
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