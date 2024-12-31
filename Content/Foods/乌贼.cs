namespace SAA.Content.Foods;

public class 乌贼 : ModItem
{
    public override void SetStaticDefaults()
    {
        // DisplayName.SetDefault("乌贼");
        // Tooltip.SetDefault("眼疾手快！");
    }
    public override void SetDefaults()
    {
        Item.SetFoodMaterials(26, 42, 0, 9, true);
    }
    public override bool? UseItem(Player player)
    {
        Vector2 pos = player.Center + new Vector2(player.direction * Main.rand.Next(30, 40), Main.rand.Next(-10, 10));
        NPC.NewNPC(null, (int)pos.X, (int)pos.Y, NPCID.Squid);
        return true;
    }
}
internal class 乌贼物品获取 : GlobalNPC
{
    public override void SetDefaults(NPC npc)
    {
        if (npc.type == NPCID.Squid)
        {
            npc.catchItem = (short)ModContent.ItemType<乌贼>();
        }
    }
}