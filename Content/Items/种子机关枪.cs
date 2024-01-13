namespace SAA.Content.Items
{
    public class 种子机关枪 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("种子机枪");
            // Tooltip.SetDefault("\"上吧！妙蛙种子，使用种子机枪\"");
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 0.5f;
            Item.rare = ItemRarityID.Green;
            Item.crit -= 4;
            Item.useTime = 9;
            Item.useAnimation = 9;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(0, 2, 0, 0);
            Item.width = 72;
            Item.height = 26;
            Item.scale = 1f;
            Item.noMelee = true;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 18f;
            Item.useAmmo = AmmoID.Dart;
            Item.UseSound = SoundID.Item63;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, +0);
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.Blowpipe, 4);
            recipe.AddIngredient(ItemID.BambooBlock, 50);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}