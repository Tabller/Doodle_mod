using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoodleMod.Content
{
    internal class TreeDrops : GlobalTile
    {
        public override void Drop(int i, int j, int type)
        {
            if (type == TileID.Trees)
            {
                if (Main.rand.NextFloat() < 0.000000000012f)
                {
                    Item.NewItem(
                        WorldGen.GetItemSource_FromTileBreak(i, j),
                        i * 16,
                        j * 16,
                        16,
                        16,
                        ModContent.ItemType<Items.Weapons.Tree>());
                }
            }
        }
    }
}
