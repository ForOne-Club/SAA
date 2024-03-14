using SAA.Content.DamageClasses;

namespace SAA.Content.Items;

public class 咬人甘蓝 : ModItem
{
    public override void SetDefaults()
    {
        Item.useStyle = ItemUseStyleID.Swing;
        Item.autoReuse = true;
        Item.damage = 50;
        Item.DamageType = ModContent.GetInstance<BotanistDamageClass>();
        Item.knockBack = 0;
        Item.shoot = ModContent.ProjectileType<Projectiles.咬人甘蓝>();
        Item.shootSpeed = 6f;
        Item.width = 28;
        Item.height = 28;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.UseSound = SoundID.Item1;
        Item.useAnimation = 38;
        Item.useTime = 38;
        Item.noUseGraphic = true;
        Item.noMelee = true;
        Item.value = Item.sellPrice(0, 0, 0, 30);
        Item.rare = ItemRarityID.Blue;
    }
    public override void AddRecipes()
    {
        //Recipe recipe = CreateRecipe();
        //recipe.AddIngredient(ItemID.Grenade, 5);
        //recipe.AddRecipeGroup(RecipeGroupID.Wood, 1);
        //recipe.AddTile(ModID<重型组装台>.ID);
        //recipe.ReplaceResult(this, 5);
        //recipe.Register();
    }
}