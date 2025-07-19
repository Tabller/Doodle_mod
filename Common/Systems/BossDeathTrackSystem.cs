
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoodleMod.Common.Systems
{
    internal class BossDeathTrackSystem : ModSystem
    {
        public static bool DownedFrustratedTomato = false;

        public override void OnWorldLoad()
        {
            DownedFrustratedTomato = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag.Add("DownedFrustratedTomato", DownedFrustratedTomato);
        }

        public override void LoadWorldData(TagCompound tag)
        {
            DownedFrustratedTomato = tag.GetBool("DownedFrustratedTomato");
        }
    }
}
