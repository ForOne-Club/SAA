using SAA.Content.Projectiles;
using Terraria.ID;

namespace SAA.Content.Items
{
    public class 作物采集器 : ModItem
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.Pink;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.width = 50;
            Item.height = 26;
            Item.scale = 1f;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<采集>();
            Item.shootSpeed = 5f;
            Item.UseSound = SoundID.Item43;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, +0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sickle);
            recipe.AddIngredient<长棍>();
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}