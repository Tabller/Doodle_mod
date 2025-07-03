using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace test_mod.Content.Projectiles
{
    public class Acorn : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.arrow = true;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = ProjAIStyleID.Arrow; // or 1
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            AIType = ProjectileID.WoodenArrowFriendly;
        }
    }
}
