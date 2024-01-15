namespace SAA.Content.Items
{
    public class 金坷垃 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.noMelee = true;
        }
        public override bool? UseItem(Player player)
        {
            int dusttype;
            int count = 40;
            Vector2 velocity = (Main.MouseWorld - player.MountedCenter).SafeNormalize(Vector2.One) * 3;
            for (int i = 0; i < count; i++)
            {
                if (i % 3 == 0) dusttype = 0;
                else dusttype = DustID.GoldCoin;
                Dust dust1 = Main.dust[Dust.NewDust(player.MountedCenter - new Vector2(32, 32), 64, 64, dusttype, velocity.X, velocity.Y, 50)];
                dust1.noGravity = i % 3 != 0;
                if (!dust1.noGravity)
                {
                    Dust dust2 = dust1;
                    dust2.scale *= 1.25f;
                    dust2 = dust1;
                    dust2.velocity /= 2f;
                    dust1.velocity.Y -= 2.2f;
                }
                else
                {
                    Dust dust2 = dust1;
                    dust2.scale *= 1.75f;
                    dust2 = dust1;
                    dust2.velocity += velocity * 0.65f;
                }
            }
            Helper.Fertilize(64, player.MountedCenter, velocity, 100, false, false);
            return true;
        }
    }
}