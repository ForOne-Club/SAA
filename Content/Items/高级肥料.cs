using SAA.Content.Projectiles;

namespace SAA.Content.Items
{
    public class 高级肥料 : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 24;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.noMelee = true;
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.shoot = ModContent.ProjectileType<施肥>();
            Item.shootSpeed = 3;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity, type, 0, 0, player.whoAmI, 0, 3);
            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(5438)
            .AddIngredient(ItemID.GlowingMushroom)
            .AddIngredient(ItemID.MudBlock)
            .AddTile(TileID.Bottles)
            .Register();
        }
    }
}