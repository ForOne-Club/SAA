global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using Terraria;
global using Terraria.Audio;
global using Terraria.DataStructures;
global using Terraria.ID;
global using Terraria.Localization;
global using Terraria.ModLoader;
global using Terraria.ObjectData;
global using static SAA.CalculateRarity;
using SAA.Content.Sys;

namespace SAA
{
    public class SAA : Mod
    {
        public override void PostSetupContent()
        {
            ModLoader.TryGetMod("ForOne", out Mod mod);
            if (mod != null)
            {
                HungerSetting.ForOne = true;
            }
        }
        public override void Load()
        {
            //消耗饱食度生成臭臭
            On_Player.TryToPoop += On_Player_TryToPoop;
        }

        private void On_Player_TryToPoop(On_Player.orig_TryToPoop orig, Player self)
        {
            HungerforPlayer player = self.GetModPlayer<HungerforPlayer>();
            if (player.Hunger > 10 && player.PoopTime == 0)
            {
                player.Hunger--;
                player.PoopTime = 300;
                int num4 = Item.NewItem(null, self.MountedCenter, Vector2.Zero, 5395, 1, noBroadcast: false, 0, noGrabDelay: true);
                if (Main.netMode == NetmodeID.SinglePlayer)
                    Main.item[num4].noGrabDelay = 100;

                if (Main.netMode == NetmodeID.MultiplayerClient)
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num4);
            }
        }
    }
}