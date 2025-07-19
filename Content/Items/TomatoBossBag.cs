using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using System;
using DoodleMod.Content.Consumables;


namespace DoodleMod.Content.Items
{
    internal class TomatoBossBag : ModItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Cyan;
            Item.expert = true;
        }


        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            int quantity = Main.rand.Next(99) + 1;
            player.QuickSpawnItem(player.GetSource_FromThis(), ModContent.ItemType<Tomato>(), quantity);
        }

    }
}
