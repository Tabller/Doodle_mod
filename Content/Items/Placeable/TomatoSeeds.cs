using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoodleMod.Content.Items.Placeable
{
    internal class TomatoSeeds : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.TomatoPlant>());
            Item.width = 12;
            Item.height = 14;
            Item.value = 80;
        }

    }
}
