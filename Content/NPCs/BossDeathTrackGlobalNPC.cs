using DoodleMod.Common.Systems;
using DoodleMod.Content.NPCs.FrustratedTomato;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoodleMod.Content.NPCs
{
    internal class BossDeathTrackGlobalNPC : GlobalNPC
    {
        public override void OnKill(NPC npc)
        {
            if (npc.type == ModContent.NPCType<FrustratedTomatoBody>())
            {

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BossDeathTrackSystem.DownedFrustratedTomato = true;
                }
                else
                {
                    NetMessage.SendData(MessageID.WorldData);
                }
            }
        }
    }
}
