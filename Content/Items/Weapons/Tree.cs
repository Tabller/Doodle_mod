using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using test_mod.Content.Projectiles;

namespace test_mod.Content.Items.Weapons
{
    public class Tree : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 76;
            Item.height = 142;
            
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.autoReuse = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 240;
            Item.knockBack = 25;
            Item.crit = 20;

            Item.value = Item.buyPrice(gold: 20);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.DD2_OgreGroundPound;

            Item.shoot = ModContent.ProjectileType<Projectiles.Acorn>();
            Item.shootSpeed = 8f; 
            
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);
            NetMessage.SendData(MessageID.PlayerControls, number: player.whoAmI);

            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        
    }
}
