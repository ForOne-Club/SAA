namespace SAA.Content.Foods;

public class 白蘑菇 : ModItem
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("你能在树桩和木桩附近找到它们");
    }
    public override void SetDefaults()
    {
        Item.SetFoodMaterials(20, 20, 0, 4, true);
    }
}
