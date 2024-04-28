namespace SAA.Content.Items;

public class 农业百科 : ModItem
{
    public int Page = 0;
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 30;
        Item.maxStack = 1;
        Item.useTurn = true;
        Item.autoReuse = true;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.value = Item.sellPrice(0, 0, 10, 0);
        Item.rare = ItemRarityID.Blue;
    }
    public override bool? UseItem(Player player)
    {
        if (player.whoAmI == Main.myPlayer)
        {
            Main.NewText(Language.GetTextValue($"Mods.SAA.Book.{Page}"), Color.Yellow);
            Page++;
            Page %= 5;
        }
        return true;
    }
    public override bool CanRightClick() => true;
    public override bool ConsumeItem(Player player) => false;
    public override void RightClick(Player player)
    {
        Page += 1;
        Page %= 5;
    }
    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        tooltips.Insert(1, new(Mod, "文本", Language.GetTextValue($"Mods.SAA.Book.{Page}")));
        base.ModifyTooltips(tooltips);
    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Book)
            .AddIngredient<锄头>()
            .AddTile(TileID.Bookcases)
            .Register();
    }
}