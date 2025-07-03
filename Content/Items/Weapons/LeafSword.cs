using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace test_mod.Content.Items.Weapons
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class LeafSword : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.test_mod.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 16;
			Item.DamageType = DamageClass.Melee;
			Item.width = 50;
			Item.height = 50;
			Item.useTime = 25;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
			Item.value = Item.buyPrice(silver: 75);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			RecipeGroup group1 = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.LeafWand)}", ItemID.LivingMahoganyLeafWand, ItemID.LeafWand);
            RecipeGroup.RegisterGroup(nameof(ItemID.LeafWand), group1);
            RecipeGroup group2 = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.LivingWoodWand)}", ItemID.LivingMahoganyWand, ItemID.LivingWoodWand);
            RecipeGroup.RegisterGroup(nameof(ItemID.LivingWoodWand), group2);
			recipe.AddRecipeGroup(nameof(ItemID.LeafWand));
			recipe.AddRecipeGroup(nameof(ItemID.LivingWoodWand));
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
