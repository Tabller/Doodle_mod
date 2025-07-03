using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.CameraModifiers;
using Terraria.ID;
using Terraria.ModLoader;

namespace test_mod.Content.NPCs.TestBoss
{
    [AutoloadBossHead] // This attribute looks for a texture called "ClassName_Head_Boss" and automatically registers it as the NPC boss head icon
    public class TestBoss : ModNPC
    {

        private int Phase = 1;

        const int ChaseState = 0;
        const int HoverProjectileState = 1;
        const int CircleProjectileState = 2;
        const int ProjectileWrathState = 3;

        float State
        {
            get => NPC.ai[0]; //When getting this variable's value, instead return the vaule of npc.ai[0] 
            set => NPC.ai[0] = value; //when setting this variable to something, instead set the value of npc.ai[0]
        }

        float Timer
        {
            get => NPC.ai[1];
            set => NPC.ai[1] = value;
        }


        // More advanced usage of a property, used to wrap around to floats to act as a Vector2
        public Vector2 FirstDestination
        {
            get => new Vector2(NPC.ai[1], NPC.ai[2]);
            set
            {
                NPC.ai[1] = value.X;
                NPC.ai[2] = value.Y;
            }
        }

        // Auto-implemented property, acts exactly like a variable by using a hidden backing field
        public Vector2 LastDestination { get; set; } = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 3;

            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                CustomTexturePath = "test_mod/Assets/Textures/Bestiary/TestBoss_Preview",
                PortraitScale = 0.7f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                PortraitPositionYOverride = 0f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 110;
            NPC.height = 86;
            NPC.damage = 45;
            NPC.defense = 35;
            NPC.lifeMax = 20000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f; // Take up open spawn slots, preventing random NPCs from spawning during the fight

            // Default buff immunities should be set in SetStaticDefaults through the NPCID.Sets.ImmuneTo{X} arrays.
            // To dynamically adjust immunities of an active NPC, NPC.buffImmune[] can be changed in AI: NPC.buffImmune[BuffID.OnFire] = true;
            // This approach, however, will not preserve buff immunities. To preserve buff immunities, use the NPC.BecomeImmuneTo and NPC.ClearImmuneToBuffs methods instead, as shown in the ApplySecondStageBuffImmunities method below.

            // Custom AI, 0 is "bound town NPC" AI which slows the NPC down and changes sprite orientation towards the target
            NPC.aiStyle = -1;

        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // Sets the description of this NPC that is listed in the bestiary
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(), // Plain black background
				new FlavorTextBestiaryInfoElement("When a Doodle World is in danger, it's time to take some tough action!")
                // new BestiaryPortraitBackgroundProviderPreferenceInfoElement()
            });
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
            int finalFrame = 2;

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


        public void FirstStage()
        {
             if (State == ChaseState)
            {
                Chase();
                if (Timer == 180)
                {
                    Timer = 0;
                    NPC.velocity = Vector2.Zero;
                    State = HoverProjectileState;
                }
            }
            else if (State == HoverProjectileState)
            {
                HoverProjectile();
                if (Timer == 180)
                {
                    Timer = 0;
                    State = CircleProjectileState;
                }
            }
            else if (State == CircleProjectileState)
            {
                CircleProjectile();
                if (Timer == 240)
                {
                    Timer = 0;
                    State = ChaseState;
                }
            }
            

            
        }

        public void SecondStage()
        {
            if (Phase == 1)
            {
                Phase++;
                Player player = Main.player[NPC.target];
                NPC.velocity = Vector2.Zero;
                NPC.Center = player.Center - new Vector2(0, 200);
                State = ProjectileWrathState;

            }
            if (State == ProjectileWrathState)
            {
                ProjectileWrath();
                if (Timer == 60 * 15)
                {
                    Timer = 0;
                    State = CircleProjectileState;
                }
            }
            if (State == CircleProjectileState)
            {
                CircleProjectile();
                if (Timer == 60 * 5)
                {
                    Timer = 0;
                    State = ChaseState;
                }
            }
            if (State == ChaseState)
            {
                Chase();
                if (Timer == 60 * 3)
                {
                    Timer = 0;
                    State = ProjectileWrathState;
                }
            }

        }
        public override void AI() 
        {
            Timer++;
            float maxHP = NPC.lifeMax;
            float currentHP = NPC.life;
            float percentHP = currentHP / maxHP * 100f;
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(false);
            }

            

            Player player = Main.player[NPC.target];

            if (!(player.dead))
            {
                if (percentHP > 50)
                {
                    FirstStage();
                }
                else
                {
                    SecondStage();
                }
            }
            else
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.04f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }
        }

        private void HoverProjectile()
        {
            Player player = Main.player[NPC.target];

            Vector2 toPlayer = player.Center - NPC.Center;

            float offsetX = 200f;

            Vector2 abovePlayer = player.Top + new Vector2(NPC.direction * offsetX, -NPC.height);

            Vector2 toAbovePlayer = abovePlayer - NPC.Center;
            Vector2 toAbovePlayerNormalized = toAbovePlayer.SafeNormalize(Vector2.UnitY);

            // The NPC tries to go towards the offsetX position, but most likely it will never get there exactly, or close to if the player is moving
            // This checks if the npc is "70% there", and then changes direction
            float changeDirOffset = offsetX * 0.7f;

            if (NPC.direction == -1 && NPC.Center.X - changeDirOffset < abovePlayer.X ||
                NPC.direction == 1 && NPC.Center.X + changeDirOffset > abovePlayer.X)
            {
                NPC.direction *= -1;
            }

            float speed = 8f;
            float inertia = 40f;

            // If the boss is somehow below the player, move faster to catch up
            if (NPC.Top.Y > player.Bottom.Y)
            {
                speed = 12f;
            }

            Vector2 moveTo = toAbovePlayerNormalized * speed;
            NPC.velocity = (NPC.velocity * (inertia - 1) + moveTo) / inertia;
            
            NPC.rotation = toPlayer.ToRotation() - MathHelper.PiOver2;

            if (Timer % 20 == 0 && Main.netMode != NetmodeID.MultiplayerClient) 
            {
                Vector2 ToPlayer = NPC.DirectionTo(player.Center) * 3;
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, ToPlayer.X, ToPlayer.Y, ProjectileID.WoodenArrowHostile, 50, 0f);
            }
        }

        private void Chase()
        {
            Player target = Main.player[NPC.target];
            Vector2 toPlayer = NPC.DirectionTo(target.Center) * 3;
            NPC.velocity = toPlayer;
        }

        private float angle = 0f;
        private float radius = 200f;
        private float speed = 8f;
        private void CircleProjectile()
        {

            Player player = Main.player[NPC.target];
            Vector2 toPlayer = player.Center;

            angle += speed;
            if (angle > MathHelper.Pi)
            {
                angle -= MathHelper.TwoPi;
            }

            Vector2 offset = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;
            NPC.Center = toPlayer + offset;

            if (Timer % 10 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Vector2 ToPlayer = NPC.DirectionTo(player.Center) * 3;
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, ToPlayer.X, ToPlayer.Y, ProjectileID.WoodenArrowHostile, 50, 0f);
            }
        }

        private void ProjectileWrath()
        {
            Player player = Main.player[NPC.target];
            float ceilingLimit = player.Center.Y - 700;
            float position = player.Center.X + Main.rand.Next(-500, 500);

            if (Timer % 10 == 0 && Main.netMode != NetmodeID.MultiplayerClient)

            {

                Vector2 spawnPosition = new Vector2(position, ceilingLimit);
                Vector2 ToPlayer = NPC.DirectionTo(player.Center) * 2;


                Projectile.NewProjectile(NPC.GetSource_FromThis(), spawnPosition.X, spawnPosition.Y, ToPlayer.X, ToPlayer.Y, ProjectileID.WoodenArrowHostile, 50, 0f);


            }

        }
    }

}