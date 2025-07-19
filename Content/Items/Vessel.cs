using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using DoodleMod.Content.Consumables;
using DoodleMod.Content.NPCs.FrustratedTomato;

namespace DoodleMod.Content.Items
{
    internal class Vessel : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Item.type] = 13;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.maxStack = 20;
            Item.rare = ItemRarityID.Cyan;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item105;
            Item.consumable = true;
        }

        public override bool? UseItem(Player player)
        {
            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<FrustratedTomatoBody>());
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Tomato>());
            recipe.AddIngredient(ItemID.LargeAmber);
            recipe.AddIngredient(ItemID.LargeAmethyst);
            recipe.AddIngredient(ItemID.LargeDiamond);
            recipe.AddIngredient(ItemID.LargeEmerald);
            recipe.AddIngredient(ItemID.LargeRuby);
            recipe.AddIngredient(ItemID.LargeSapphire);
            recipe.AddIngredient(ItemID.LargeTopaz);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
