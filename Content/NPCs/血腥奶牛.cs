using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
namespace SAA.Content.NPCs
{
    public class 血腥奶牛 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("奶蜗牛");
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[220];
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 0.4f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }
        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 30;
            NPC.aiStyle = 67;
            NPC.damage = 5;
            NPC.defense = 0;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.knockBackResist = 0.5f;
            NPC.DeathSound = SoundID.NPCDeath1;
            AnimationType = 220;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.catchItem = (short)ModContent.ItemType<血腥奶蜗牛>();
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundCrimson,
                new FlavorTextBestiaryInfoElement("奶蜗牛的体液具有一定的腐蚀性，但是它却有着能把血腥之地的污水净化成鲜奶的能力，真是了不起！")
            });
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return (spawnInfo.Player.ZoneDirtLayerHeight || spawnInfo.Player.ZoneRockLayerHeight) && spawnInfo.Player.ZoneCrimson ? 0.08f : 0;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Foods.牛奶>(), 1));
        }
    }
}