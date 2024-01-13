using Terraria.GameContent.ObjectInteractions;
namespace SAA.Base
{
    public abstract class 篝火 : ModTile
    {
        public virtual string name => "篝火";
        public virtual int ItemType => 0;
        public virtual float Distance => 1500f;//原版篝火的影响范围
        public virtual Vector3 lightcolor => new(0.9f, 0.3f, 0.1f);
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.HasOutlines[Type] = true;//外沿高光选择
            TileID.Sets.DisableSmartCursor[Type] = true;
            AdjTiles = new int[] { 215 };
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.DrawYOffset = 2;//下沉一像素，不会影响PreDraw
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(254, 121, 2), CreateMapEntryName());
            AnimationFrameHeight = 36;
        }
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;//智能选择
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (frame != 8)//给PreDraw用的
            {
                frameCounter++;
                if (frameCounter >= 4)
                {
                    frameCounter = 0;
                    frame++;
                    if (frame >= 8)
                    {
                        frame = 0;
                    }
                }
            }
        }
        public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
        {
            if (Main.tile[i, j].TileFrameY >= 288)
            {
                frameYOffset = 0;//给没有用PreDraw的时候用的
            }

            base.AnimateIndividualTile(type, i, j, ref frameXOffset, ref frameYOffset);
        }
        public virtual void GHMouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer;
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ItemType;//应该是显示物品
            base.MouseOver(i, j);
        }
        public override void MouseOverFar(int i, int j)
        {
            MouseOver(i, j);
            Player player = Main.LocalPlayer;
            if (player.cursorItemIconText == "")
            {
                player.cursorItemIconEnabled = false;
                player.cursorItemIconID = 0;
            }
        }
        public override bool RightClick(int i, int j)
        {
            Main.mouseRightRelease = false;
            //SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
            //通过数学找到左上方瓦片的坐标
            int g = Main.tile[i, j].TileFrameY / 18;
            int h = Main.tile[i, j].TileFrameX / 18;
            int y = j - g % 2;
            int x = i - h % 3;
            if (Main.tile[i, j].TileFrameY < 288)
            {
                for (int k = 0; k <= 2; k++)
                {
                    for (int f = 0; f <= 1; f++)
                    {
                        Main.tile[x + k, y + f].TileFrameY = (short)(f * 18 + 288);
                    }
                }
            }
            else
            {
                for (int k = 0; k <= 2; k++)
                {
                    for (int f = 0; f <= 1; f++)
                    {
                        Main.tile[x + k, y + f].TileFrameY = (short)(f * 18);
                    }
                }
            }
            return true;
        }
        public override void HitWire(int i, int j)
        {
            //SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
            //通过数学找到左上方瓦片的坐标
            int g = Main.tile[i, j].TileFrameY / 18;
            int y = j - g % 2;
            int x = i - Main.tile[i, j].TileFrameX / 18;
            if (Main.tile[i, j].TileFrameY < 288)
            {
                for (int k = 0; k <= 2; k++)
                {
                    for (int f = 0; f <= 1; f++)
                    {
                        Main.tile[x + k, y + f].TileFrameY = (short)(f * 18 + 288);
                    }
                }
                Main.tileFrame[Type] = 8;
            }
            else
            {
                for (int k = 0; k <= 2; k++)
                {
                    for (int f = 0; f <= 1; f++)
                    {
                        Main.tile[x + k, y + f].TileFrameY = (short)(f * 18);
                    }
                }
                Main.tileFrame[Type] = 0;
            }
        }
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
            //Buff写在player的Update
            /*Player player = Main.player[Main.myPlayer];
            bool closer = Vector2.Distance(player.Center, new Vector2(i * 16, j * 16)) < Distance;
            if (closer)
            {
                for (int k = 0; k < Buff.Length; k++)
                {
                    player.AddBuff(Buff[k], 2, false, false);
                    //player.AddBuff(Buff[k], 2);会显示秒数，怪
                }
            }*/
            //亮度
            r = lightcolor.X;
            g = lightcolor.Y;
            b = lightcolor.Z;
            base.ModifyLight(i, j, ref r, ref g, ref b);
        }
        /*public override void NearbyEffects(int i, int j, bool closer)//信号不持续,不能用来加短时间buff,不能用于篝火,我写到player里
        {
            Player player = Main.player[Main.myPlayer];
            closer = Vector2.Distance(player.Center, new Vector2(i * 16, j * 16)) < Distance;
            if (closer && Main.tile[i, j].TileFrameY<36)
            {
                for (int k = 0; k < Buff.Length; k++)
                {
                    player.AddBuff(Buff[k], 2, false, false);
                    //player.AddBuff(Buff[k], 2);会显示秒数，怪
                }
            }
        }*/
        public virtual void GHPostDraw(int i, int j, SpriteBatch spriteBatch, Texture2D texture2D, float alpha, Color color)
        {
            //Helper.TileHelper.GlowDraw(i, j, spriteBatch, TextureAssets.Tile[base.Type].Value, 1f);
            if (Main.tile[i, j].TileFrameY < 288)
            {
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
                color.A = 0;//达到高亮效果
                spriteBatch.Draw(texture2D, new Vector2(i * 16 - (int)Main.screenPosition.X,
                    j * 16 + 2//往下画一个像素，治标不治本，因为预览不会变
                    - (int)Main.screenPosition.Y) + zero, new Rectangle(frameX, frameY, width, height), color * alpha, 0f,
                    default, 1f, SpriteEffects.None, 0f);
            }
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 48, 32, ItemType);
        }
    }
}