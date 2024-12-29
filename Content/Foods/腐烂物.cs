namespace SAA.Content.Foods;

public class 腐烂物 : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 42;
        Item.height = 18;
        Item.maxStack = 9999;
        Item.value = 0;
        Item.rare = ItemRarityID.Gray;
    }
}
