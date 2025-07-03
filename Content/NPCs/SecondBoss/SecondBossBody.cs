using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace test_mod.Content.NPCs.SecondBoss
{
    public class SecondBoss : ModNPC
    {
        private bool SecondPhase = false;

        public int PropellerMaxHealthTotal
        {
            get => (int)NPC.ai[3];
            set => NPC.ai[3] = value;
        }

        public bool SpawnedPropellers
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }


        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;
        }
    
    public override void SetDefaults()
        {
            NPC.width = 256;
            NPC.height = 256;
            NPC.damage = 1;
            NPC.defense = 0;
            NPC.lifeMax = 10;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;
            NPC.aiStyle = -1;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses; // use the boss immunity cooldown counter, to prevent ignoring boss attacks by taking damage from other sources
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            // This NPC animates with a simple "go from start frame to final frame, and loop back to start frame" rule
            int startFrame = 0;
            int finalFrame = 0;

            int frameSpeed = 5;
            NPC.frameCounter += 0.5f;
            NPC.frameCounter += NPC.velocity.Length() / 10f; // Make the counter go faster with more movement speed
            if (NPC.frameCounter > frameSpeed)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;

                if (NPC.frame.Y > finalFrame * frameHeight)
                {
                    NPC.frame.Y = startFrame * frameHeight;
                }
            }
        }

        private void SpawnPropellers()
        {
            if (SpawnedPropellers || Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            SpawnedPropellers = true;

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }

            int count = 4;
            var entitySource = NPC.GetSource_FromAI();

            PropellerMaxHealthTotal = 0;
            for (int i = 0; i < count; i++)
            {
                NPC propellerNPC = NPC.NewNPCDirect(entitySource, (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Propeller>(), NPC.whoAmI);
                if (propellerNPC.whoAmI == Main.maxNPCs)
                    continue;


                Propeller propeller = (Propeller)propellerNPC.ModNPC;
                propeller.ParentIndex = NPC.whoAmI; // Let the minion know who the "parent" is
                propeller.PositionOffset = i / (float)count; // Give it a separate position offset

                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, number: propellerNPC.whoAmI);
                }

            }
        }

        private void Fly()
        {
            Player player = Main.player[NPC.target];

            // this is hard
        }
        public override void AI()
        {
            SpawnPropellers();

            NPC.dontTakeDamage = !SecondPhase;

            Fly(); 
        }
    }
}
