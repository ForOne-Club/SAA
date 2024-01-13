global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using Terraria;
global using Terraria.Audio;
global using Terraria.ID;
global using Terraria.Localization;
global using Terraria.ModLoader;
global using Terraria.DataStructures;
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
            if(mod != null) 
            {
                HungerSetting.ForOne = true;
            }
        }
    }
}