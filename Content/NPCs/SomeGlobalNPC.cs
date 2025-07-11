using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using test_mod.Content.Items.Placeable;

namespace test_mod.Content.NPCs
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
