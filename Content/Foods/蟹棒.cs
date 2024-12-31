using Terraria.GameContent.ItemDropRules;
namespace SAA.Content.Foods
{
    public class 蟹棒 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("蟹棒");
            // Tooltip.SetDefault("你确定要生吃这玩意？");
        }
        public override void SetDefaults()
        {
            Item.SetFoodMaterials(40, 28, 0, 9, true);
        }
    }
}