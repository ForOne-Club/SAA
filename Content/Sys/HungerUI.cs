using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace SAA.Content.Sys
{
    public class HungerUI : UIState
    {
        public static new float Width = 0.46f;
        public override void Draw(SpriteBatch spriteBatch)
        {
            Texture2D texture1 = ModContent.Request<Texture2D>("SAA/Content/Sys/UI1").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>("SAA/Content/Sys/UI2").Value;
            Texture2D texture3 = ModContent.Request<Texture2D>("SAA/Content/Sys/UI3").Value;
            float scale = 1f;

            Player Player = Main.player[Main.myPlayer];
            HungerforPlayer modPlayer = Player.GetModPlayer<HungerforPlayer>();
            int progress = (int)modPlayer.Hunger;
            int HungerMax = (int)modPlayer.HungerMax;
            int k = modPlayer.HungerCount;
            int truemax;
            if (k > 20)
            {
                truemax = (k - 20) * 5;
            }
            else
            {
                truemax = k * 20;
            }
            Rectangle Bar1 = new Rectangle((int)(Main.screenWidth * Width), (int)(Main.screenHeight * 0.02f), (int)(texture1.Width * scale), (int)(texture1.Height * scale));
            //距离屏幕左上角的宽，距离屏幕左上角的高（绘制位置），图片的宽，图片的高（缩放）
            Rectangle Bar2 = new(0, 0, texture1.Width, texture1.Height);
            //图片内距左上角的宽，图片内距左上角的高，取的图片的宽，取的图片的高
            Main.spriteBatch.Draw(texture1, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            float factor1 = (float)progress / truemax;
            int newwidth = (int)(66 * factor1);
            Bar1.Width = 28 + newwidth;
            Bar2.Width = 28 + newwidth;
            Main.spriteBatch.Draw(texture2, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            float factor2 = (float)HungerMax / truemax;
            newwidth = (int)(66 * factor2);
            Bar1.X = Bar1.X + 28 + newwidth;
            Bar1.Width = texture1.Width - 28 - newwidth;
            Bar2.X = 28 + newwidth;
            Bar2.Width = texture1.Width - 28 - newwidth;
            Main.spriteBatch.Draw(texture3, Bar1, Bar2, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            //文字
            Bar1 = new Rectangle((int)(Main.screenWidth * Width), (int)(Main.screenHeight * 0.02f), (int)(texture1.Width * scale), (int)(texture1.Height * scale));
            if (Bar1.Intersects(new Rectangle(Main.mouseX, Main.mouseY, 1, 1)))
            {
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, $"{progress}/{HungerMax}",
                    new Vector2(Main.mouseX + 20, Main.mouseY + 20), Color.White, 0f, Vector2.Zero, new Vector2(1f));
            }
        }
    }
    public class HungerSystem : ModSystem
    {
        internal HungerUI 饱食度UI;
        internal UserInterface 饱食度UserInterface;
        public override void Load()
        {
            饱食度UI = new HungerUI();
            饱食度UI.Activate();
            饱食度UserInterface = new UserInterface();
            饱食度UserInterface.SetState(饱食度UI);
        }
        public override void UpdateUI(GameTime gameTime)
        {
            if (!HungerSetting.ForOne)
                饱食度UserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            Player player = Main.player[Main.myPlayer];
            HungerforPlayer mplayer = player.GetModPlayer<HungerforPlayer>();
            //寻找一个名字为Vanilla: Mouse Text的绘制层，也就是绘制鼠标字体的那一层，并且返回那一层的索引
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            //寻找到索引时
            if (MouseTextIndex != -1)
            {
                //往绘制层集合插入一个成员，第一个参数是插入的地方的索引，第二个参数是绘制层
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                   //这里是绘制层的名字
                   "I : 饱食度UI",
                   //这里是匿名方法
                   delegate
                   {
                       if (!HungerSetting.ForOne)
                           饱食度UI.Draw(Main.spriteBatch);
                       return true;
                   },
                   //这里是绘制层的类型
                   InterfaceScaleType.UI)
               );
            }
        }
    }
}
