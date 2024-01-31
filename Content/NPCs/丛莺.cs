using SAA.Content.Foods;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
namespace SAA.Content.NPCs
{
    public class 丛莺 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("报丧女妖");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[152];
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Position = new Vector2(0f, 18f),//展示框内的图形移动(不移动应该是中心)
                PortraitPositionXOverride = 0f,//图像在具体展示框内的移动(不移动应该是图片全貌)
                PortraitPositionYOverride = 0f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }
        public override void SetDefaults()
        {
            NPC.width = 46;
            NPC.height = 60;
            NPC.aiStyle = 14;
            NPC.damage = 50;
            NPC.defense = 6;
            NPC.lifeMax = 120;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.knockBackResist = 0.5f;
            NPC.DeathSound = SoundID.NPCDeath4;
            AnimationType = 152;
            NPC.value = 500;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
                new FlavorTextBestiaryInfoElement("生活在地下丛林的鸟类, 靠着其健壮的身躯和坚硬的喙存在于丛林生态链之上。")
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.UndergroundJungle.Chance * 0.3f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(320, 2, 1, 2));
            npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生翅尖>(), 0.2f));
            npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生翅根>(), 0.15f));
            npcLoot.Add(Helper.PercentageDrop(ModContent.ItemType<生鸡腿>(), 0.1f));
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                return;
            }
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ModContent.Find<ModGore>("SAA/丛莺_1").Type);
                Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ModContent.Find<ModGore>("SAA/丛莺_2").Type);
                return;
            }
        }
    }
}