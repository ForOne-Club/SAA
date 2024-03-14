using SAA.Content.Foods;
using SAA.Content.Items;
using SAA.Content.Planting.Seeds;
using Terraria.GameContent.ItemDropRules;

namespace SAA.Content.Sys
{
    public class HungerforNPC : GlobalNPC
    {
        public override void SetDefaults(NPC entity)
        {
            entity.value /= 10;
        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Vulture)
            {
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生翅尖>(), 0.17f));
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生翅根>(), 0.12f));
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生鸡腿>(), 0.08f));
                npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<蛋>(), 0.02f));
            }
            if (npc.type == NPCID.Crab)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<蟹棒>(), 5, 1, 1));
            }
            if (npc.type == NPCID.WallCreeper || npc.type == NPCID.WallCreeperWall || npc.type == NPCID.BlackRecluse || npc.type == NPCID.BlackRecluseWall)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<棉花种子>(), 5, 1, 1));
            }
            int[] animals = new int[] { 46, 303, 337, 540, 443, 299, 538, 539, 639, 640, 641, 642, 643, 644, 645, 646, 647, 648, 649, 650, 651, 652, 74, 297, 298 };
            if (animals.Contains(npc.type))
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<小肉>(), 2, 1, 1));
            }
        }
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.Name == "Shop")
            {
                if (shop.NpcType == 20)
                {
                    shop.Add(ModContent.ItemType<咬人甘蓝种子>());
                }
                //    if (shop.NpcType == NPCID.Merchant)
                //    {
                //        shop.Add(ModContent.ItemType<锄头>());
                //        if (shop.TryGetEntry(ModContent.ItemType<锄头>(), out NPCShop.Entry entry))
                //        {
                //            _ = entry.Disable();//禁止售卖
                //        }
                //    }
            }
        }
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type == NPCID.Merchant)
            {
                for (int i = items.Length - 1; i > 16; i--)
                {
                    if (items[i - 1] != null) items[i] = items[i - 1];
                }
                items[16] = new Item(ModContent.ItemType<锄头>());
            }
            base.ModifyActiveShop(npc, shopName, items);
        }
        public override bool PreAI(NPC npc)
        {
            int[] animals = new int[] { 46, 303, 337, 540, 443, 299, 538, 539, 639, 640, 641, 642, 643, 644, 645, 646, 647, 648, 649, 650, 651, 652 };
            if (animals.Contains(npc.type))
            {
                float distance = Helper.ModeNum(200, 250, 300);
                Player t = null;
                foreach (Player p in Main.player)
                {
                    if (p != null && p.active && !p.dead)
                    {
                        float d = Vector2.Distance(npc.Center, p.Center);
                        if (d < distance)
                        {
                            distance = d;
                            t = p;
                        }
                    }
                }
                if (t != null)
                {
                    if (distance > Helper.ModeNum(190, 240, 290) && t.velocity.X == 0 && Math.Abs(npc.velocity.X) < 0.5f)
                    {
                        npc.velocity.X = 0;
                    }
                    else
                    {
                        Vector2 d = npc.Center - Main.player[npc.target].Center;
                        if (d.X > 0)
                        {
                            if (npc.velocity.X > -1)
                                npc.velocity.X += 0.05f;
                            else
                                npc.velocity.X -= 0.05f;
                        }
                        else
                        {
                            if (npc.velocity.X > 1)
                                npc.velocity.X += 0.05f;
                            else
                                npc.velocity.X -= 0.05f;
                        }
                        if (npc.velocity.X > 5) npc.velocity.X = 5;
                        else if (npc.velocity.X < -5) npc.velocity.X = -5;
                        if (npc.velocity.X > 0.5f) npc.direction = 1;
                        else if (npc.velocity.X < 0.5f) npc.direction = -1;
                        if (npc.collideX)
                        {
                            npc.Center -= new Vector2(npc.direction * 8, 8);
                            npc.velocity.X = npc.direction * 5;
                            npc.velocity.Y -= 2;
                            //return true;
                        }
                    }
                    return false;
                }
            }
            return base.PreAI(npc);
        }
    }
}
