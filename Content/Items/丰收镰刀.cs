using SAA.Content.Planting.System;
using SAA.Content.Planting.Tiles.Plants;

namespace SAA.Content.Items
{
    public class 丰收镰刀 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("丰收镰刀");
            // Tooltip.SetDefault("帮助玩家快速收集种子但是无法收集干草" + "\n用它收集野果野菜将得到更高产量");
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.scale = 1f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sickle, 1);
            recipe.AddIngredient(ItemID.GoldBar, 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
            Recipe recipe1 = CreateRecipe();
            recipe1.AddIngredient(ItemID.Sickle, 1);
            recipe1.AddIngredient(ItemID.PlatinumBar, 8);
            recipe1.AddTile(TileID.Anvils);
            recipe1.Register();
        }
    }
}