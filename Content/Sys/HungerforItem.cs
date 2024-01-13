namespace SAA.Content.Sys
{
    public class HungerforItem : GlobalItem
    {
        public int HealHunger = 0;
        public override void Load()
        {
            On_Player.QuickBuff += QuickBuffFixForHunger;//变成了快速吃饱，悲
            base.Load();
        }
        private void QuickBuffFixForHunger(On_Player.orig_QuickBuff orig, Player self)
        {
            if (self.cursed || self.CCed || self.dead)
            {
                return;
            }
            SoundStyle? legacySoundStyle = null;
            if (self.CountBuffs() == Player.MaxBuffs)
            {
                return;
            }
            Item item = QuickBuff_PickBestFoodItem(self);
            if (item != null)
            {
                legacySoundStyle = item.UseSound;
                //int num = item.buffTime;
                //if (num == 0)
                //{
                //    num = 3600;
                //}
                //self.AddBuff(item.buffType, num, true, false);
                if (item.consumable && ItemLoader.ConsumeItem(item, self))
                {
                    item.stack--;
                    if (item.stack <= 0)
                    {
                        item.TurnToAir();
                    }
                }
            }
            if (self.CountBuffs() != Player.MaxBuffs)
            {
                for (int i = 0; i < 58; i++)
                {
                    Item item2 = self.inventory[i];
                    if (item2.stack > 0 && item2.type > ItemID.None && item2.buffType > 0 && item2.DamageType != DamageClass.Summon)
                    {
                        int num2 = item2.buffType;
                        bool flag = CombinedHooks.CanUseItem(self, item2) && QuickBuff_ShouldBotherUsingThisBuff(self, num2) && num2 != BuffID.WellFed && num2 != 206 && num2 != 207;
                        if (item2.mana > 0 && flag && self.CheckMana(item2, -1, true, true))
                        {
                            self.manaRegenDelay = (int)self.maxRegenDelay;
                        }
                        if (self.whoAmI == Main.myPlayer && item2.type == ItemID.Carrot && !Main.runningCollectorsEdition)
                        {
                            flag = false;
                        }
                        if (num2 == 27)
                        {
                            num2 = Main.rand.Next(3);
                            if (num2 == 0)
                            {
                                num2 = 27;
                            }
                            if (num2 == 1)
                            {
                                num2 = 101;
                            }
                            if (num2 == 2)
                            {
                                num2 = 102;
                            }
                        }
                        if (flag)
                        {
                            ItemLoader.UseItem(item2, self);
                            legacySoundStyle = item2.UseSound;
                            int num3 = item2.buffTime;
                            if (num3 == 0)
                            {
                                num3 = 3600;
                            }
                            self.AddBuff(num2, num3, true, false);
                            if (item2.consumable && ItemLoader.ConsumeItem(item2, self))
                            {
                                item2.stack--;
                                if (item2.stack <= 0)
                                {
                                    item2.TurnToAir();
                                }
                            }
                            if (self.CountBuffs() == Player.MaxBuffs)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (legacySoundStyle != null)
            {
                SoundEngine.PlaySound(legacySoundStyle, new Vector2?(self.position));
                Recipe.FindRecipes(false);
            }
            return;
        }
        private int QuickBuff_FindFoodPriority(int buffType)
        {
            return buffType == 26 ? 1 : buffType == 206 ? 2 : buffType == 207 ? 3 : 0;
        }
        public override void SetDefaults(Item entity)
        {
            if (Helper.IsFoods(entity, out int buff, out int time))
            {
                entity.GetGlobalItem<HungerforItem>().HealHunger = QuickBuff_FindFoodPriority(buff) * time / 3600;
            }
            base.SetDefaults(entity);
        }
        private Item QuickBuff_PickBestFoodItem(Player player)//如果饱食度机制开了应该选最合适的食物
        {
            Item item = null;
            HungerforPlayer modplayer = player.GetModPlayer<HungerforPlayer>();
            float hunger = modplayer.HungerMax - modplayer.Hunger;
            int hungerheal0 = 0;
            for (int j = 0; j < 58; j++)
            {
                Item item2 = player.inventory[j];
                if (!item2.IsAir)
                {
                    int hungerheal = item2.GetGlobalItem<HungerforItem>().HealHunger;
                    if (hungerheal <= hunger && (item == null || hungerheal0 < hungerheal))
                    {
                        item = item2;
                        hungerheal0 = hungerheal;
                    }
                }
            }
            return item;
        }
        private bool QuickBuff_ShouldBotherUsingThisBuff(Player player, int attemptedType)
        {
            bool result = true;
            for (int i = 0; i < Player.MaxBuffs; i++)
            {
                if (attemptedType == 27 && (player.buffType[i] == 27 || player.buffType[i] == 101 || player.buffType[i] == 102))
                {
                    result = false;
                    break;
                }
                if (BuffID.Sets.IsWellFed[attemptedType] && BuffID.Sets.IsWellFed[player.buffType[i]])
                {
                    result = false;
                    break;
                }
                if (player.buffType[i] == attemptedType)
                {
                    result = false;
                    break;
                }
                if (Main.meleeBuff[attemptedType] && Main.meleeBuff[player.buffType[i]])
                {
                    result = false;
                    break;
                }
            }
            if (Main.lightPet[attemptedType] || Main.vanityPet[attemptedType])
            {
                for (int j = 0; j < Player.MaxBuffs; j++)
                {
                    if (Main.lightPet[player.buffType[j]] && Main.lightPet[attemptedType])
                    {
                        result = false;
                    }
                    if (Main.vanityPet[player.buffType[j]] && Main.vanityPet[attemptedType])
                    {
                        result = false;
                    }
                }
            }
            return result;
        }
        public override void OnConsumeItem(Item item, Player player)
        {
            if (Helper.IsFoods(item, out _, out _))
            {
                float hungerheal = item.GetGlobalItem<HungerforItem>().HealHunger;
                HungerforPlayer hungerforPlayer = player.GetModPlayer<HungerforPlayer>();
                float hunger = hungerforPlayer.HungerMax - hungerforPlayer.Hunger;
                if (hunger >= 10 && hungerforPlayer.HungerReduce > 0) hungerforPlayer.HungerReduce--;
                hungerforPlayer.Hunger += hungerheal > hunger ? hunger : hungerheal;
                hungerheal = hungerheal > hunger ? hunger - (int)hunger - 0.5f > 0 ? hunger + 1 : hunger : hungerheal;
                HungerHeal((int)hungerheal, player);
                Gluttony(player, (int)hungerheal, 1);
            }
        }
        //暴食Buff吃东西回血
        private static void Gluttony(Player player, int hungerheal, float open)
        {
            //YZPlayer yzp = player.YZP();
            //float convert = 0.2f * yzp.Relicparam;
            //var (ex, type) = yzp.relic;
            //if (type != YZRelicSystem.RelicType.Gluttony) return;
            //if (ex)
            //{
            //    if (player.statLife == player.statLifeMax2)
            //    {
            //        int value = (int)(hungerheal * convert * open);
            //        yzp.gluttonyHeal += value;
            //        player.statLifeMax2 += value;
            //        player.Cure(value, CombatText.HealLife);
            //        return;
            //    }
            //    convert = 0.3f * yzp.Relicparam;
            //}
            //player.Cure((int)(hungerheal * convert * open), CombatText.HealLife);
        }
        public static void HungerHeal(int hungerheal, Player player)
        {
            CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(240, 220, 30, 255), hungerheal, false, false);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int heal = item.GetGlobalItem<HungerforItem>().HealHunger;
            if (heal > 0)
            {
                TooltipLine text = new(Mod, "饱食度", Language.GetTextValue("Mods.SAA.Tooltips") + $":{heal}");
                tooltips.Insert(2, text);//第几行插入，1在名称下面
            }
            base.ModifyTooltips(item, tooltips);
        }
        public override bool InstancePerEntity => true;
    }
}