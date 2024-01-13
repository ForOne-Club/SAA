namespace SAA
{
    internal static class RecipeSupport
    {
        #region RecipeExtensionMethods
        #region RecipeInfoStruct
        public struct AddIngredientInfo
        {
            public AddIngredientInfo(int type, int stack = 1)
            {
                Type = type;
                Stack = stack < 0 ? 0 : stack;
            }
            public readonly int Type;
            public readonly int Stack;
            public bool IsAvailable => Stack > 0 && Type == Math.Clamp(Type, 0, ItemLoader.ItemCount - 1);
            public static implicit operator AddIngredientInfo(int type)
            {
                return type < ItemLoader.ItemCount ? new AddIngredientInfo(type) : default;
            }
            public static implicit operator AddIngredientInfo(Type type)
            {
                if (type.IsSubclassOf(typeof(ModItem)))
                {
                    string[] str = type.FullName.Split(".");
                    string fullname = str[0] + "/" + str[^1];
                    if (ModContent.TryFind(fullname, out ModItem modItem))
                    {
                        return new AddIngredientInfo(modItem.Type);
                    }
                }
                return default;
            }
            public static implicit operator AddIngredientInfo(ModItem modItem)
            {
                return modItem.GetType();
            }
            public static implicit operator AddIngredientInfo(Item item)
            {
                return new AddIngredientInfo(item.type, item.stack);
            }
            public static implicit operator AddIngredientInfo(int[] param)
            {
                return param.Length > 2 && param[0] < ItemLoader.ItemCount ? new AddIngredientInfo(param[0], param[1]) : default;
            }
            public static implicit operator AddIngredientInfo((int, int) param)
            {
                return param.Item1 < ItemLoader.ItemCount ? new AddIngredientInfo(param.Item1, param.Item2) : default;
            }
            public static implicit operator AddIngredientInfo((Type, int) param)
            {
                if (param.Item1.IsSubclassOf(typeof(ModItem)))
                {
                    string[] str = param.Item1.FullName.Split(".");
                    string fullname = str[0] + "/" + str[^1];
                    if (ModContent.TryFind(fullname, out ModItem modItem))
                    {
                        return new AddIngredientInfo(modItem.Type, param.Item2);
                    }
                }
                return default;
            }
            public static implicit operator AddIngredientInfo((ModItem, int) param)
            {
                return (param.Item1.GetType(), param.Item2);
            }
            public static implicit operator AddIngredientInfo((Item, int) param)
            {
                return new AddIngredientInfo(param.Item1.type, param.Item2);
            }
            public static implicit operator AddIngredientInfo((Mod, string) param)
            {
                return ModContent.TryFind(param.Item1.Name, param.Item2, out ModItem modItem) ? new AddIngredientInfo(modItem.Type) : default;
            }
            public static implicit operator AddIngredientInfo((Mod, string, int) param)
            {
                return ModContent.TryFind(param.Item1.Name, param.Item2, out ModItem modItem)
                    ? new AddIngredientInfo(modItem.Type, param.Item3)
                    : default;
            }
            public static implicit operator Item(AddIngredientInfo info)
            {
                return new Item(info.Type, info.Stack);
            }
            public static implicit operator int(AddIngredientInfo info)
            {
                return info.Type;
            }
            public static implicit operator string(AddIngredientInfo info)
            {
                return Lang.GetItemNameValue(info.Type);
            }
            public static implicit operator ModItem(AddIngredientInfo info)
            {
                return ItemLoader.GetItem(info.Type) is ModItem modItem ? (ModItem)Activator.CreateInstance(modItem.GetType()) : null;
            }
            public static AddIngredientInfo operator +(AddIngredientInfo orig, AddIngredientInfo value)
            {
                return orig.Type != value.Type ? orig : new AddIngredientInfo(orig.Type, orig.Stack + value.Stack);
            }
            public static AddIngredientInfo operator +(AddIngredientInfo orig, int value)
            {
                return new AddIngredientInfo(orig.Type, orig.Stack + value);
            }
            public static AddIngredientInfo operator -(AddIngredientInfo orig, AddIngredientInfo value)
            {
                return orig.Type != value.Type ? orig : new AddIngredientInfo(orig.Type, orig.Stack - value.Stack);
            }
            public static AddIngredientInfo operator -(AddIngredientInfo orig, int value)
            {
                return new AddIngredientInfo(orig.Type, orig.Stack - value);
            }
            public static AddIngredientInfo operator *(AddIngredientInfo orig, double value)
            {
                return new AddIngredientInfo(orig.Type, (int)(orig.Stack * value));
            }
            public static AddIngredientInfo operator /(AddIngredientInfo orig, double value)
            {
                return new AddIngredientInfo(orig.Type, (int)(orig.Stack / value));
            }
        }
        public struct AddGroupInfo
        {
            public AddGroupInfo(int groupid, int stack = 1)
            {
                GroupID = groupid;
                Stack = stack < 0 ? 0 : stack;
            }
            public readonly int GroupID;
            public readonly int Stack;
            public bool IsAvailable => Stack > 0 && GroupID == Math.Clamp(GroupID, 0, RecipeGroup.nextRecipeGroupIndex - 1);
            public static implicit operator AddGroupInfo(int id)
            {
                if (RecipeGroup.recipeGroups.TryGetValue(id, out RecipeGroup group))
                {
                    return new AddGroupInfo(group.RegisteredId);
                }
                return default;
            }
            public static implicit operator AddGroupInfo(string name)
            {
                return RecipeGroup.recipeGroupIDs.TryGetValue(name, out int id) ? new AddGroupInfo(id) : default;
            }
            public static implicit operator AddGroupInfo(RecipeGroup group)
            {
                return new AddGroupInfo(group.RegisteredId);
            }
            public static implicit operator AddGroupInfo(int[] param)
            {
                return param.Length > 2 && param[0] < RecipeGroup.nextRecipeGroupIndex ? new AddGroupInfo(param[0], param[1]) : default;
            }
            public static implicit operator AddGroupInfo((int, int) param)
            {
                if (RecipeGroup.recipeGroups.TryGetValue(param.Item1, out RecipeGroup group))
                {
                    return new AddGroupInfo(group.RegisteredId, param.Item2);
                }
                return default;
            }
            public static implicit operator AddGroupInfo((string, int) param)
            {
                return RecipeGroup.recipeGroupIDs.TryGetValue(param.Item1, out int id) ? new AddGroupInfo(id, param.Item2) : default;
            }
            public static implicit operator AddGroupInfo((RecipeGroup, int) param)
            {
                return new AddGroupInfo(param.Item1.RegisteredId, param.Item2);
            }
            public static implicit operator int(AddGroupInfo info)
            {
                return info.GroupID;
            }
            public static implicit operator string(AddGroupInfo info)
            {
                using (var keys = RecipeGroup.recipeGroupIDs.Keys.GetEnumerator())
                {
                    while (keys.MoveNext())
                    {
                        if (RecipeGroup.recipeGroupIDs[keys.Current] == info.GroupID)
                        {
                            return keys.Current;
                        }
                    }
                }
                return string.Empty;
            }
            public static implicit operator RecipeGroup(AddGroupInfo info)
            {
                return RecipeGroup.recipeGroups.TryGetValue(info.GroupID, out RecipeGroup group) ? group : null;
            }
            public static AddGroupInfo operator +(AddGroupInfo orig, AddGroupInfo value)
            {
                return orig.GroupID != value.GroupID ? orig : new AddGroupInfo(orig.GroupID, orig.Stack + value.Stack);
            }
            public static AddGroupInfo operator +(AddGroupInfo orig, int value)
            {
                return new AddGroupInfo(orig.GroupID, orig.Stack + value);
            }
            public static AddGroupInfo operator -(AddGroupInfo orig, AddGroupInfo value)
            {
                return orig.GroupID != value.GroupID ? orig : new AddGroupInfo(orig.GroupID, orig.Stack - value.Stack);
            }
            public static AddGroupInfo operator -(AddGroupInfo orig, int value)
            {
                return new AddGroupInfo(orig.GroupID, orig.Stack - value);
            }
            public static AddGroupInfo operator *(AddGroupInfo orig, double value)
            {
                return new AddGroupInfo(orig.GroupID, (int)(orig.Stack * value));
            }
            public static AddGroupInfo operator /(AddGroupInfo orig, double value)
            {
                return new AddGroupInfo(orig.GroupID, (int)(orig.Stack / value));
            }
        }
        public struct AddTileInfo
        {
            public AddTileInfo(int type)
            {
                Type = type;
            }
            public readonly int Type;
            public bool IsAvailable => Type == Math.Clamp(Type, 0, TileLoader.TileCount - 1);
            public static implicit operator AddTileInfo(int type)
            {
                return type < TileLoader.TileCount ? new AddTileInfo(type) : default;
            }
            public static implicit operator AddTileInfo(Type type)
            {
                if (type.IsSubclassOf(typeof(ModTile)))
                {
                    string[] str = type.FullName.Split(".");
                    string fullname = str[0] + "/" + str[^1];
                    if (ModContent.TryFind(fullname, out ModTile modTile))
                    {
                        return new AddTileInfo(modTile.Type);
                    }
                }
                return default;
            }
            public static implicit operator AddTileInfo(ModTile modTile)
            {
                return modTile.GetType();
            }
            public static implicit operator AddTileInfo((Mod, string) param)
            {
                return ModContent.TryFind(param.Item1.Name, param.Item2, out ModTile modTile) ? new AddTileInfo(modTile.Type) : default;
            }
            public static implicit operator int(AddTileInfo info)
            {
                return info.Type;
            }
            public static implicit operator string(AddTileInfo info)
            {
                return Lang.GetItemNameValue(info.Type);
            }
            public static implicit operator ModTile(AddTileInfo info)
            {
                return TileLoader.GetTile(info.Type) is ModTile modTile ? (ModTile)Activator.CreateInstance(modTile.GetType()) : null;
            }
            public static implicit operator TileObjectData(AddTileInfo info)
            {
                return TileObjectData.GetTileData(info.Type, 0);
            }
        }
        #endregion
        public static Recipe AddIngredient(this Recipe recipe, IEnumerable<AddIngredientInfo> infos)
        {
            foreach (AddIngredientInfo info in infos)
            {
                if (info.IsAvailable)
                {
                    recipe.AddIngredient(info.Type, info.Stack);
                }
            }
            return recipe;
        }
        public static Recipe AddRecipeGroup(this Recipe recipe, IEnumerable<AddGroupInfo> infos)
        {
            foreach (AddGroupInfo info in infos)
            {
                if (info.IsAvailable)
                {
                    recipe.AddRecipeGroup(info.GroupID, info.Stack);
                }
            }
            return recipe;
        }
        public static Recipe AddTile(this Recipe recipe, IEnumerable<AddTileInfo> infos)
        {
            foreach (AddTileInfo info in infos)
            {
                if (info.IsAvailable)
                {
                    recipe.AddTile(info.Type);
                }
            }
            return recipe;
        }
        public static Recipe AddLiquid(this Recipe recipe, int liquidtype, int liquidamount, byte leastamount = 200)
        {
            recipe.Conditions.RemoveAll(new Predicate<Condition>((condition) =>
            {
                switch (liquidtype)
                {
                    case 0:
                        {
                            return condition.Description.Value == "water";
                        }
                    case 1:
                        {
                            return condition.Description.Value == "lava";
                        }
                    case 2:
                        {
                            return condition.Description.Value == "honey";
                        }
                }
                return false;
            }));
            string text;
            switch (liquidtype)
            {
                case 0:
                    {
                        text = "water";
                        break;
                    }
                case 1:
                    {
                        text = "lava";
                        break;
                    }
                case 2:
                    {
                        text = "honey";
                        break;
                    }
                default:
                    {
                        return recipe;
                    }
            }
            float v = liquidamount / 255f;
            recipe.AddCondition(Language.GetText("Mods.I.Recipe.AddCondition").WithFormatArgs(Math.Round(v, 1), text),
                () =>
                {
                    int x = (int)(Main.LocalPlayer.Center.X / 16);
                    int y = (int)Main.LocalPlayer.Center.Y;
                    int r = 0;
                    for (int i = x - 4; i <= x + 4; i++)
                    {
                        for (int j = y - 4; j <= y + 4; j++)
                        {
                            int lt = Main.tile[i, j].LiquidType;
                            byte la = Main.tile[i, j].LiquidAmount;
                            switch (liquidtype)
                            {
                                case 0:
                                    {
                                        if (lt == 0 && lt >= leastamount)
                                        {
                                            r += la;
                                        }
                                        else if (TileID.Sets.CountsAsWaterSource[lt])
                                        {
                                            r += 255;
                                        }
                                        break;
                                    }
                                case 1:
                                    {
                                        if (lt == 1 && lt >= leastamount)
                                        {
                                            r += la;
                                        }
                                        else if (TileID.Sets.CountsAsLavaSource[lt])
                                        {
                                            r += 255;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        if (lt == 2 && lt >= leastamount)
                                        {
                                            r += la;
                                        }
                                        else if (TileID.Sets.CountsAsHoneySource[lt])
                                        {
                                            r += 255;
                                        }
                                        break;
                                    }
                            }
                            if (r > liquidamount)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                });
            return recipe;
        }
        public static Recipe AddNPC(this Recipe recipe, int npctype, int amount = 1, float range = 64, bool killnpc = false)
        {
            recipe.AddCondition(Language.GetText("Mods.I.Recipe.AddCondition").WithFormatArgs(amount, Lang.GetNPCNameValue(npctype), Math.Round(range / 16, 1)),
                () =>
                {
                    List<NPC> list = Helper.FindNPC(new Predicate<NPC>((n) =>
                    {
                        return n.type == npctype && Vector2.Distance(n.Center, Main.LocalPlayer.Center) <= range;
                    }));
                    return list.Count >= amount;
                });
            if (killnpc)
            {
                recipe.AddOnCraftCallback(new Recipe.OnCraftCallback((recipe, item, list, stack) =>
                {
                    List<NPC> selected = Helper.FindNPC(new Predicate<NPC>((n) =>
                    {
                        return n.type == npctype && Vector2.Distance(n.Center, Main.LocalPlayer.Center) <= range;
                    }));
                    for (int i = 0; i < Math.Min(selected.Count, amount); i++)
                    {
                        selected[i].life = 0;
                        selected[i].checkDead();
                    }
                }));
            }
            return recipe;
        }
        public static Recipe AddConsumeItemCallback(this Recipe recipe, IEnumerable<Recipe.ConsumeItemCallback> callbacks)
        {
            foreach (Recipe.ConsumeItemCallback callback in callbacks)
            {
                recipe.AddConsumeItemCallback(callback);
            }
            return recipe;
        }
        public static Recipe AddOnCraftCallback(this Recipe recipe, IEnumerable<Recipe.OnCraftCallback> callbacks)
        {
            foreach (Recipe.OnCraftCallback callback in callbacks)
            {
                recipe.AddOnCraftCallback(callback);
            }
            return recipe;
        }
        public static Recipe AddCondition(this Recipe recipe, IEnumerable<(LocalizedText, Func<bool>)> conditions)
        {
            foreach ((LocalizedText description, Func<bool> condition) in conditions)
            {
                recipe.AddCondition(description, condition);
            }
            return recipe;
        }
        public static Dictionary<Condition, bool> RemoveCondition(this Recipe recipe, IEnumerable<Condition> conditions)
        {
            Dictionary<Condition, bool> result = new();
            foreach (Condition condition in conditions)
            {
                result[condition] = recipe.RemoveCondition(condition);
            }
            return result;
        }
        public static Dictionary<Predicate<Condition>, IEnumerable<Condition>> RemoveCondition(this Recipe recipe, IEnumerable<Predicate<Condition>> predicates)
        {
            Dictionary<Predicate<Condition>, IEnumerable<Condition>> result = new();
            foreach (Predicate<Condition> predicate in predicates)
            {
                var conditions = from Condition c in recipe.Conditions where predicate(c) select c;
                recipe.RemoveCondition(conditions);
                result[predicate] = conditions;
            }
            return result;
        }
        public static bool[] RemoveIngredient(this Recipe recipe, IEnumerable<Item> items)
        {
            bool[] result = new bool[items.Count()];
            int index = 0;
            foreach (Item item in items)
            {
                result[index] = recipe.RemoveIngredient(item);
                index++;
            }
            return result;
        }
        public static bool[] RemoveRecipeGroup(this Recipe recipe, IEnumerable<int> groupids)
        {
            bool[] result = new bool[groupids.Count()];
            int index = 0;
            foreach (int id in groupids)
            {
                result[index] = recipe.RemoveRecipeGroup(id);
                index++;
            }
            return result;
        }
        public static bool[] RemoveTile(this Recipe recipe, IEnumerable<int> tileids)
        {
            bool[] result = new bool[tileids.Count()];
            int index = 0;
            foreach (int id in tileids)
            {
                result[index] = recipe.RemoveTile(id);
                index++;
            }
            return result;
        }
        public static bool TryFindRecipe(Predicate<Recipe> predicate, out Recipe recipe)
        {
            recipe = null;
            for (int i = 0; i < Main.recipe.Length; i++)
            {
                if (Main.recipe[i] is null)
                {
                    break;
                }
                if (predicate(Main.recipe[i]))
                {
                    recipe = Main.recipe[i];
                    return true;
                }
            }
            return false;
        }
        public static bool TryFindRecipes(Predicate<Recipe> predicate, out IEnumerable<Recipe> recipes)
        {
            recipes = from Recipe r in Main.recipe where r is not null && predicate(r) select r;
            return recipes.Any();
        }
        public static Dictionary<TKey, List<Recipe>> TryFindRecipes<TKey>(IEnumerable<TKey> keys, Func<TKey, Recipe, bool> predicate)
        {
            Dictionary<TKey, List<Recipe>> result = new();
            for (int i = 0; i < Main.recipe.Length; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe is null)
                {
                    break;
                }
                foreach (TKey key in keys)
                {
                    if (predicate(key, recipe))
                    {
                        if (result.TryGetValue(key, out List<Recipe> rs))
                        {
                            rs.Add(recipe);
                        }
                        else
                        {
                            result.Add(key, new List<Recipe>() { recipe });
                        }
                    }
                }
            }
            return result;
        }
        public static bool TryRemoveRecipe(Predicate<Recipe> predicate, out Recipe removed)
        {
            removed = null;
            if (TryFindRecipe(predicate, out Recipe recipe))
            {
                if (RemoveRecipe(recipe))
                {
                    removed = recipe;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static bool TryRemoveRecipe(Predicate<Recipe> predicate)
        {
            return TryRemoveRecipe(predicate, out Recipe recipe);
        }
        public static bool RemoveRecipe(Recipe recipe)
        {
            for (int j = 0; j < Recipe.numRecipes; j++)
            {
                if (Main.recipe[j] == recipe)
                {
                    for (int i = j; i < Recipe.numRecipes - 1; i++)
                    {
                        Main.recipe[i] = Main.recipe[i + 1];
                    }
                    //Main.recipe[Recipe.numRecipes - 1] = null;
                    Recipe.numRecipes--;
                    return true;
                }
            }
            return false;
        }
        public static bool TryRemoveRecipes(Predicate<Recipe> predicate, out IEnumerable<Recipe> removed)
        {
            removed = null;
            if (TryFindRecipes(predicate, out IEnumerable<Recipe> recipes))
            {
                List<Recipe> list = new();
                foreach (Recipe recipe in recipes)
                {
                    if (RemoveRecipe(recipe))
                    {
                        list.Add(recipe);
                    }
                }
                if (list.Count == 0)
                {
                    return false;
                }
                else
                {
                    removed = list.AsEnumerable();
                    return true;
                }
            }
            return false;
        }
        public static int TryRemoveRecipes(Predicate<Recipe> predicate)
        {
            return TryRemoveRecipes(predicate, out IEnumerable<Recipe> recipes) ? recipes.Count() : 0;
        }
        public static void TryModifyRecipes(Predicate<Recipe> predicate, Action<Recipe> action)
        {
            if (TryFindRecipes(predicate, out IEnumerable<Recipe> recipes))
            {
                foreach (Recipe recipe in recipes)
                {
                    action(recipe);
                }
            }
        }
        public static bool UseWood(this Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.Wood) && RecipeGroup.recipeGroups[RecipeGroupID.Wood].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.Wood].ContainsItem(reqType);
        }
        public static bool UseIronBar(this Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.IronBar) && RecipeGroup.recipeGroups[RecipeGroupID.IronBar].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.IronBar].ContainsItem(reqType);
        }
        public static bool UseSand(this Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.Sand) && RecipeGroup.recipeGroups[RecipeGroupID.Sand].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.Sand].ContainsItem(reqType);
        }
        public static bool UseFragment(this Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.Fragment) && RecipeGroup.recipeGroups[RecipeGroupID.Fragment].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.Fragment].ContainsItem(reqType);
        }
        public static bool UsePressurePlate(this Recipe recipe, int invType, int reqType)
        {
            return recipe.HasRecipeGroup(RecipeGroupID.PressurePlate) && RecipeGroup.recipeGroups[RecipeGroupID.PressurePlate].ContainsItem(invType) && RecipeGroup.recipeGroups[RecipeGroupID.PressurePlate].ContainsItem(reqType);
        }
        public static bool CheckTileRequire(this Recipe recipe)
        {
            int index = 0;
            while (index < recipe.requiredTile.Count && recipe.requiredTile[index] > 0 && recipe.requiredTile[index] < TileLoader.TileCount)
            {
                if (!Main.LocalPlayer.adjTile[recipe.requiredTile[index]])
                {
                    return false;
                }
                index++;
            }
            return true;
        }
        public static bool CheckCondition(this Recipe recipe)
        {
            if (!(!recipe.HasCondition(Condition.NearWater) || Main.LocalPlayer.adjWater &
                            !recipe.HasCondition(Condition.NearHoney) || recipe.HasCondition(Condition.NearHoney) == Main.LocalPlayer.adjHoney &
                            !recipe.HasCondition(Condition.NearLava) || recipe.HasCondition(Condition.NearLava) == Main.LocalPlayer.adjLava &
                            !recipe.HasCondition(Condition.InSnow) || Main.LocalPlayer.ZoneSnow &
                            !recipe.HasCondition(Condition.InGraveyard) || Main.LocalPlayer.ZoneGraveyard))
            {
                return false;
            }
            foreach (Condition condition in recipe.Conditions)
            {
                // RecipeAvaliable
                if (!condition.IsMet())
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}