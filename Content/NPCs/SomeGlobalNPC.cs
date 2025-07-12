using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using DoodleMod.Content.Items.Placeable;

namespace DoodleMod.Content.NPCs
{
    internal class SomeGlobalNPC : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Dryad)
            {
                shop.Add(new Item(ModContent.ItemType<TomatoSeeds>()) {
                    shopCustomPrice = 15
                });
            }
        }




    }
}
