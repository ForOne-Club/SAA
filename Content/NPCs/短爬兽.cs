using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
namespace SAA.Content.NPCs
{
    public class 短爬兽 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("短爬兽");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Zombie];
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 0.6f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 30;
            NPC.aiStyle = 3;
            NPC.damage = 5;
            NPC.defense = 5;
            NPC.lifeMax = 30;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.knockBackResist = 0.5f;
            NPC.DeathSound = SoundID.NPCDeath1;
            AnimationType = NPCID.Zombie;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Jungle,
                new FlavorTextBestiaryInfoElement("成年的短爬兽会对它觉得打得过的生物发动攻击以此来展示给异性短爬兽自己强大的一面。")
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.dayTime)
            {
                if (SpawnCondition.SurfaceJungle.Chance > 0f)
                {
                    return SpawnCondition.SurfaceJungle.Chance * 0.6f;
                }

                if (SpawnCondition.UndergroundJungle.Chance > 0f)
                {
                    return SpawnCondition.UndergroundJungle.Chance * 0.5f;
                }
            }
            return 0.0f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Foods.短爬兽排>(), 1));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Pets.草球>(), 1000));
        }
    }
}