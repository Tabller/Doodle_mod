using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace test_mod.Content.NPCs.SecondBoss
{
    public class Propeller : ModNPC
    {

        //smartass stuff down that is in example mod
        public int ParentIndex
        {
            get => (int)NPC.ai[0] - 1;
            set => NPC.ai[0] = value + 1;
        }

        public bool HasParent => ParentIndex > -1;

        public float PositionOffset
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }
        public static int BodyType()
        {
            return ModContent.NPCType<Propeller>();
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 1;

            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Poisoned] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.Confused] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 64;
            NPC.height = 64;
            NPC.damage = 1;
            NPC.defense = 0;
            NPC.HitSound = SoundID.DD2_SkeletonHurt;
            NPC.DeathSound = SoundID.DD2_SkeletonDeath;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.8f;
            NPC.netAlways = true;

            NPC.lifeMax = 100; 
            NPC.dontTakeDamage = false;
            NPC.chaseable = true;
            NPC.aiStyle = -1;
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = ImmunityCooldownID.Bosses; // use the boss immunity cooldown counter, to prevent ignoring boss attacks by taking damage from other sources
            return true;
        }

        public override void AI()
        {
            if (!HasParent || !Main.npc[ParentIndex].active)
            {
                NPC.active = false;
                return;
            }

            NPC parentNPC = Main.npc[ParentIndex];

            float offsetX = parentNPC.width / 2f;
            float offsetY = parentNPC.height / 2f;

            Vector2 offset;
            switch ((PositionOffset * 4))
            {
                case 0:
                    offset = new Vector2(-offsetX, -offsetY);
                    break;
                case 1: 
                    offset = new Vector2(offsetX, -offsetY);
                    break;
                case 2:
                    offset = new Vector2(offsetX, offsetY);
                    break;
                case 3:
                    offset = new Vector2(-offsetX, offsetY);
                    break;
                default:
                    offset = Vector2.Zero; break;
            }

            Vector2 destination = parentNPC.Center + offset;
            Vector2 toDestination = destination - NPC.Center;

            NPC.velocity = toDestination;
        }
    }
}
