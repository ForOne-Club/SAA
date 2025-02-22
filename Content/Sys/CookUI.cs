﻿using SAA.Content.Packages;
using Terraria.GameInput;
using Terraria.UI;

namespace SAA.Content.Sys
{
    public class CookUI : UIState
    {
        public static bool Open = false;
        public static new float Width = 0.65f;
        public override void Draw(SpriteBatch spriteBatch)
        {
            Player player = Main.LocalPlayer;
            var cp = player.GetModPlayer<CookPlayer>();
            if (cp.CookInfo < 0)
            {
                Open = false;//无需同步因为根本没打开
                return;
            }
            CookStore a = CookSystem.Cook[cp.CookInfo];
            CookSystem.FindRecipe(cp.CookInfo);
            Vector2 worldPosition = new Vector2(a.CookTile.X, a.CookTile.Y).ToWorldCoordinates();
            if (Vector2.Distance(worldPosition, player.Center) > 200 || !Main.playerInventory)//距离超过或关闭背包
            {
                Open = false;
                SoundEngine.PlaySound(SoundID.MenuClose);
                Cook.NetSend(a.CookTile.X, a.CookTile.Y, false);
                return;
            }
            if (a.CookItems.Length == 8)//烹饪锅
            {
                Texture2D texture1 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI1").Value;
                Texture2D texture2 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI2").Value;
                Texture2D texture3 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI3").Value;
                Texture2D texture4 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI4").Value;
                Texture2D texture5 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI_Close").Value;

                float scale = 1f;
                float height = 0.12f;
                int w = (int)(Main.screenWidth * Width);// + HungerSetting.UIoffsetX;
                Rectangle Bar1 = new Rectangle(w, (int)(Main.screenHeight * height), (int)(texture1.Width * scale), (int)(texture1.Height * scale));
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Rectangle Bar2 = new(0, 0, texture1.Width, texture1.Height);
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture1, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(texture2, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                if(Main.LocalPlayer.chest>-1) Main.inventoryScale *= 1.3128f;
                else Main.inventoryScale *= 1.652f;//第一次进游戏打开背包前这个值貌似不对劲，所以我只需要打开UI的同时打开背包即可
                Vector2 pos = new Vector2(Bar1.X, Bar1.Y);
                for (int i = 0; i < 6; i++)
                {
                    if (Utils.FloatIntersect(Main.mouseX, Main.mouseY, 0f, 0f, Bar1.X + 10 + 64 * (i % 3), Bar1.Y + 10 + 64 * (i / 3), 52 * scale, 52 * scale) && !PlayerInput.IgnoreMouseInterface)
                    {
                        Main.LocalPlayer.mouseInterface = true;
                        ItemSlot.Handle(ref a.CookItems[i]);
                    }
                    ItemSlot.Draw(Main.spriteBatch, ref a.CookItems[i], 14, pos + new Vector2(10, 10) + new Vector2(64 * (i % 3), 64 * (i / 3)));
                }

                if (Utils.FloatIntersect(Main.mouseX, Main.mouseY, 0f, 0f, Bar1.X + 10, Bar1.Y + 176, 52 * scale, 52 * scale) && !PlayerInput.IgnoreMouseInterface)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    ItemSlot.Handle(ref a.CookItems[6]);
                }
                ItemSlot.Draw(Main.spriteBatch, ref a.CookItems[6], 14, pos + new Vector2(10, 176));

                if (Utils.FloatIntersect(Main.mouseX, Main.mouseY, 0f, 0f, Bar1.X + 138, Bar1.Y + 176, 52 * scale, 52 * scale) && !PlayerInput.IgnoreMouseInterface)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    if (Main.mouseItem.IsAir)//手上没有东西才能获取成品，因为成品槽不能放置其他东西
                    {
                        ItemSlot.Handle(ref a.CookItems[7]);
                    }
                }
                ItemSlot.Draw(Main.spriteBatch, ref a.CookItems[7], 14, pos + new Vector2(138, 176));
                if (Main.LocalPlayer.chest > -1) Main.inventoryScale /= 1.3128f;
                else Main.inventoryScale /= 1.652f;

                float factor1;
                if (a.MaxBurnTime <= 0) factor1 = 0;
                else factor1 = (float)a.BurnTime / a.MaxBurnTime;
                int newheight1 = (int)(31 * (1 - factor1));
                Bar1 = new Rectangle(w, (int)(Main.screenHeight * height) + 136 + newheight1, (int)(texture3.Width * scale), (int)(texture3.Height * scale) - newheight1);
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Bar2 = new(0, newheight1, texture3.Width, texture3.Height - newheight1);
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture3, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                float factor2;
                if (a.MaxFinishTime <= 0) factor2 = 0;
                else factor2 = (float)a.FinishTime / a.MaxFinishTime;
                int newheight2 = (int)(30 * (1 - factor2));
                Bar1 = new Rectangle(w, (int)(Main.screenHeight * height), (int)(texture4.Width * scale), (int)(texture4.Height * scale) - newheight2);
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Bar2 = new(0, 0, texture4.Width, texture4.Height - newheight2);
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture4, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

                Bar1 = new Rectangle(w + (int)(texture1.Width * scale), (int)(Main.screenHeight * height) - 10, (int)(texture5.Width * scale), (int)(texture5.Height / 2 * scale));
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Bar2 = new(0, 0, 18, 14);
                if (Bar1.Intersects(new Rectangle(Main.mouseX, Main.mouseY, 1, 1)))
                {
                    Bar2 = new(0, 14, 18, 14);
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Open = false;
                        SoundEngine.PlaySound(SoundID.MenuClose);
                        Cook.NetSend(a.CookTile.X, a.CookTile.Y, false);
                    }
                }
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture5, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
            else if (a.CookItems.Length == 4)//配种机
            {
                Texture2D texture1 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI1_H").Value;
                Texture2D texture2 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI2_H").Value;
                Texture2D texture3 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI3_H").Value;
                Texture2D texture4 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI4").Value;
                Texture2D texture5 = ModContent.Request<Texture2D>("SAA/Content/Sys/CookUI_Close").Value;

                float scale = 1f;
                float height = 0.12f;
                int w = (int)(Main.screenWidth * Width);// + HungerSetting.UIoffsetX;
                Rectangle Bar1 = new Rectangle(w, (int)(Main.screenHeight * height), (int)(texture1.Width * scale), (int)(texture1.Height * scale));
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Rectangle Bar2 = new(0, 0, texture1.Width, texture1.Height);
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture1, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(texture2, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

                if (Main.LocalPlayer.chest > -1) Main.inventoryScale *= 1.3128f;
                else Main.inventoryScale *= 1.652f;//第一次进游戏打开背包前这个值貌似不对劲，所以我只需要打开UI的同时打开背包即可
                Vector2 pos = new Vector2(Bar1.X, Bar1.Y);
                for (int i = 0; i < 2; i++)
                {
                    if (Utils.FloatIntersect(Main.mouseX, Main.mouseY, 0f, 0f, Bar1.X + 10 + 64 * (i == 1 ? 2 : 0), Bar1.Y + 10, 52 * scale, 52 * scale) && !PlayerInput.IgnoreMouseInterface)
                    {
                        Main.LocalPlayer.mouseInterface = true;
                        if (Main.mouseItem.stack == 1 || Main.mouseItem.IsAir)//一公一母每个物品栏当然不能超过一个
                        {
                            ItemSlot.Handle(ref a.CookItems[i]);
                        }
                    }
                    ItemSlot.Draw(Main.spriteBatch, ref a.CookItems[i], 14, pos + new Vector2(10, 10) + new Vector2(64 * (i == 1 ? 2 : 0), 0));
                }

                if (Utils.FloatIntersect(Main.mouseX, Main.mouseY, 0f, 0f, Bar1.X + 10, Bar1.Y + 176, 52 * scale, 52 * scale) && !PlayerInput.IgnoreMouseInterface)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    ItemSlot.Handle(ref a.CookItems[2]);
                }
                ItemSlot.Draw(Main.spriteBatch, ref a.CookItems[2], 14, pos + new Vector2(10, 176));

                if (Utils.FloatIntersect(Main.mouseX, Main.mouseY, 0f, 0f, Bar1.X + 138, Bar1.Y + 176, 52 * scale, 52 * scale) && !PlayerInput.IgnoreMouseInterface)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    if (Main.mouseItem.IsAir)//手上没有东西才能获取成品，因为成品槽不能放置其他东西
                    {
                        ItemSlot.Handle(ref a.CookItems[3]);
                    }
                }
                ItemSlot.Draw(Main.spriteBatch, ref a.CookItems[3], 14, pos + new Vector2(138, 176));
                if (Main.LocalPlayer.chest > -1) Main.inventoryScale /= 1.3128f;
                else Main.inventoryScale /= 1.652f;

                float factor1;
                if (a.MaxBurnTime <= 0) factor1 = 0;
                else factor1 = (float)a.BurnTime / a.MaxBurnTime;
                int newheight1 = (int)(25 * (1 - factor1));
                Bar1 = new Rectangle(w, (int)(Main.screenHeight * height) + 140 + newheight1, (int)(texture3.Width * scale), (int)(texture3.Height * scale) - newheight1);
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Bar2 = new(0, newheight1, texture3.Width, texture3.Height - newheight1);
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture3, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                float factor2;
                if (a.MaxFinishTime <= 0) factor2 = 0;
                else factor2 = (float)a.FinishTime / a.MaxFinishTime;
                int newheight2 = (int)(30 * (1 - factor2));
                Bar1 = new Rectangle(w, (int)(Main.screenHeight * height), (int)(texture4.Width * scale), (int)(texture4.Height * scale) - newheight2);
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Bar2 = new(0, 0, texture4.Width, texture4.Height - newheight2);
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture4, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

                Bar1 = new Rectangle(w + (int)(texture1.Width * scale), (int)(Main.screenHeight * height) - 10, (int)(texture5.Width * scale), (int)(texture5.Height / 2 * scale));
                //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
                Bar2 = new(0, 0, 18, 14);
                if (Bar1.Intersects(new Rectangle(Main.mouseX, Main.mouseY, 1, 1)))
                {
                    Bar2 = new(0, 14, 18, 14);
                    if (Main.mouseLeft && Main.mouseLeftRelease)
                    {
                        Open = false;
                        SoundEngine.PlaySound(SoundID.MenuClose);
                        Cook.NetSend(a.CookTile.X, a.CookTile.Y, false);
                    }
                }
                //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
                Main.spriteBatch.Draw(texture5, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }
    }
}
