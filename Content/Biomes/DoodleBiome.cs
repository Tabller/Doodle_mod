using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using DoodleMod.Common.Systems;

namespace DoodleMod.Content.Biomes.Doodle
{
    internal class DoodleBiome : ModBiome
    {
        public override string BestiaryIcon => base.BestiaryIcon;
        public override string BackgroundPath => base.BackgroundPath;
        public override Color? BackgroundColor => base.BackgroundColor;
        public override string MapBackground => BackgroundPath;

        public override bool IsBiomeActive(Player player)
        {
            // We will check if our FirstBoss is defeated.
            bool b1 = BossDeathTrackSystem.DownedFrustratedTomato;

            // We will limit this biome to the inner horizontal third of the map.
            bool b2 = Math.Abs(player.position.ToTileCoordinates().X - Main.maxTilesX / 2) < Main.maxTilesX / 6;

            // We will limit the height at which this biome can be active to above ground (ie sky and surface). Most (if not all) surface biomes will use this condition.
            bool b3 = player.ZoneSkyHeight || player.ZoneOverworldHeight;
            return b1 && b2 && b3;
        }
    }
}
