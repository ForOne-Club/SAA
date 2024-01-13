using SAA.Base;
using Terraria.DataStructures;

namespace SAA.Content.Placeable.Tiles
{
    public class 烤肉篝火 : 篝火
    {
        public override string name => "烤肉篝火";
        public override float Distance => 1700f;
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            //粒子效果
            Tile tile = Main.tile[i, j];
            if (tile.TileFrameY < 36 && Main.rand.NextBool(3) && ((Main.drawToScreen && Main.rand.NextBool(4)) || !Main.drawToScreen) && tile.TileFrameY == 0)
            {
                int num9 = Dust.NewDust(new Vector2(i * 16 + 2, j * 16 - 4), 4, 8, DustID.Smoke, 0f, 0f, 100, default, 1f);
                if (tile.TileFrameX == 0)
                {
                    Dust dust6 = Main.dust[num9];
                    dust6.position.X += Main.rand.Next(8);
                }
                if (tile.TileFrameX == 36)
                {
                    Dust dust7 = Main.dust[num9];
                    dust7.position.X -= Main.rand.Next(8);
                }
                Main.dust[num9].alpha += Main.rand.Next(100);
                Main.dust[num9].velocity *= 0.2f;
                Dust dust8 = Main.dust[num9];
                dust8.velocity.Y -= (0.5f + Main.rand.Next(10) * 0.1f);
                Main.dust[num9].fadeIn = 0.5f + Main.rand.Next(10) * 0.1f;
            }
            //亮度
            int o = Main.tile[i, j].TileFrameY / 18;
            int h = Main.tile[i, j].TileFrameX / 18;
            int y = j - o % 2;
            int x = i - h % 3;
            switch (Main.tile[x, y].TileFrameX / 54)
            {
                case 1:
                    r = 0.7f;
                    g = 1f;
                    b = 0.5f;
                    break;
                case 2:
                    r = 0.5f * Main.demonTorch + 1f * (1f - Main.demonTorch);
                    g = 0.3f;
                    b = 1f * Main.demonTorch + 0.5f * (1f - Main.demonTorch);
                    break;
                case 3:
                    r = 0.45f;
                    g = 0.75f;
                    b = 1f;
                    break;
                case 4:
                    r = 1.15f;
                    g = 1.15f;
                    b = 0.5f;
                    break;
                case 5:
                    r = Main.DiscoR / 255f;
                    g = Main.DiscoG / 255f;
                    b = Main.DiscoB / 255f;
                    break;
                case 6:
                    r = 0.75f;
                    g = 1.28249991f;
                    b = 1.2f;
                    break;
                case 7:
                    r = 0.95f;
                    g = 0.65f;
                    b = 1.3f;
                    break;
                case 8:
                    r = 1.4f;
                    g = 0.85f;
                    b = 0.55f;
                    break;
                case 9:
                    r = 0.25f;
                    g = 1.3f;
                    b = 0.8f;
                    break;
                case 10:
                    r = 0.95f;
                    g = 0.4f;
                    b = 1.4f;
                    break;
                case 11:
                    r = 1.4f;
                    g = 0.7f;
                    b = 0.5f;
                    break;
                case 12:
                    r = 1.25f;
                    g = 0.6f;
                    b = 1.2f;
                    break;
                case 13:
                    r = 0.75f;
                    g = 1.45f;
                    b = 0.9f;
                    break;
                default:
                    r = 0.9f;
                    g = 0.3f;
                    b = 0.1f;
                    break;
            }
            float num13 = Main.rand.Next(28, 42) * 0.005f;
            num13 += (270 - Main.mouseTextColor) / 700f;
            r += num13;
            g += num13;
            b += num13;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            int o = Main.tile[i, j].TileFrameY / 18;
            int h = Main.tile[i, j].TileFrameX / 18;
            int y = j - o % 2;
            int x = i - h % 3;
            if (Main.tile[i, j].TileFrameY < 288)
            {
                Texture2D texture2D = ModContent.Request<Texture2D>("SAA/Content/Placeable/Tiles/烤肉篝火_Glow").Value;
                int frameX = Main.tile[i, j].TileFrameX;
                int frameY = Main.tile[i, j].TileFrameY;
                frameY = frameY % 36 == 0 ? Main.tileFrame[Type] * 36 : Main.tileFrame[Type] * 36 + 18;
                int width = 20;
                int height = 20;
                Vector2 zero = new(Main.offScreenRange, Main.offScreenRange);
                if (Main.drawToScreen)
                {
                    zero = Vector2.Zero;
                }
                //Texture2D texture2D = TextureAssets.Tile[base.Type].Value;
                Color color = Color.White;
                if (Main.tile[x, y].TileFrameX / 54 == 5)
                {
                    color = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 0);
                }
                else
                {
                    color.A = 0;
                }

                spriteBatch.Draw(texture2D, new Vector2(i * 16 - (int)Main.screenPosition.X,
                    j * 16 + 2//往下画一个像素，治标不治本，因为预览不会变
                    - (int)Main.screenPosition.Y) + zero, new Rectangle(frameX, frameY, width, height), color, 0f,
                    default, 1f, SpriteEffects.None, 0f);
            }
        }
        public override void MouseOver(int i, int j)
        {
            int g = Main.tile[i, j].TileFrameY / 18;
            int h = Main.tile[i, j].TileFrameX / 18;
            int y = j - g % 2;
            int x = i - h % 3;
            int type = ModContent.ItemType<Placeable.烤肉篝火>();
            switch (Main.tile[x, y].TileFrameX / 54)
            {
                case 0:
                    {
                        type = ModContent.ItemType<Placeable.烤肉篝火>();
                        break;
                    }
                case 1:
                    {
                        type = ModContent.ItemType<烤肉诅咒篝火>();
                        break;
                    }
                case 2:
                    {
                        type = ModContent.ItemType<烤肉恶魔篝火>();
                        break;
                    }
                case 3:
                    {
                        type = ModContent.ItemType<烤肉冰冻篝火>();
                        break;
                    }
                case 4:
                    {
                        type = ModContent.ItemType<烤肉灵液篝火>();
                        break;
                    }
                case 5:
                    {
                        type = ModContent.ItemType<烤肉彩虹篝火>();
                        break;
                    }
                case 6:
                    {
                        type = ModContent.ItemType<烤肉超亮篝火>();
                        break;
                    }
                case 7:
                    {
                        type = ModContent.ItemType<烤肉骨头篝火>();
                        break;
                    }
                case 8:
                    {
                        type = ModContent.ItemType<烤肉沙漠篝火>();
                        break;
                    }
                case 9:
                    {
                        type = ModContent.ItemType<烤肉珊瑚篝火>();
                        break;
                    }
                case 10:
                    {
                        type = ModContent.ItemType<烤肉腐化篝火>();
                        break;
                    }
                case 11:
                    {
                        type = ModContent.ItemType<烤肉猩红篝火>();
                        break;
                    }
                case 12:
                    {
                        type = ModContent.ItemType<烤肉神圣篝火>();
                        break;
                    }
                case 13:
                    {
                        type = ModContent.ItemType<烤肉丛林篝火>();
                        break;
                    }
                default: break;
            }
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = type;//应该是显示物品
            base.MouseOver(i, j);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int type = ModContent.ItemType<Placeable.烤肉篝火>();
            switch (frameX / 54)
            {
                case 0:
                    {
                        type = ModContent.ItemType<Placeable.烤肉篝火>();
                        break;
                    }
                case 1:
                    {
                        type = ModContent.ItemType<烤肉诅咒篝火>();
                        break;
                    }
                case 2:
                    {
                        type = ModContent.ItemType<烤肉恶魔篝火>();
                        break;
                    }
                case 3:
                    {
                        type = ModContent.ItemType<烤肉冰冻篝火>();
                        break;
                    }
                case 4:
                    {
                        type = ModContent.ItemType<烤肉灵液篝火>();
                        break;
                    }
                case 5:
                    {
                        type = ModContent.ItemType<烤肉彩虹篝火>();
                        break;
                    }
                case 6:
                    {
                        type = ModContent.ItemType<烤肉超亮篝火>();
                        break;
                    }
                case 7:
                    {
                        type = ModContent.ItemType<烤肉骨头篝火>();
                        break;
                    }
                case 8:
                    {
                        type = ModContent.ItemType<烤肉沙漠篝火>();
                        break;
                    }
                case 9:
                    {
                        type = ModContent.ItemType<烤肉珊瑚篝火>();
                        break;
                    }
                case 10:
                    {
                        type = ModContent.ItemType<烤肉腐化篝火>();
                        break;
                    }
                case 11:
                    {
                        type = ModContent.ItemType<烤肉猩红篝火>();
                        break;
                    }
                case 12:
                    {
                        type = ModContent.ItemType<烤肉神圣篝火>();
                        break;
                    }
                case 13:
                    {
                        type = ModContent.ItemType<烤肉丛林篝火>();
                        break;
                    }
                default: break;
            }
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, type);
        }
    }
}