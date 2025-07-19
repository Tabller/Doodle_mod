using DoodleMod.Content.Tiles;
using System;
using Terraria.ModLoader;

namespace DoodleMod.Common.Systems
{
    public class DoodleBiomeTileCount : ModSystem
    {
        public int exampleBlockCount;

        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
            exampleBlockCount = tileCounts[ModContent.TileType<DoodleBlock>()];
        }
    }
}