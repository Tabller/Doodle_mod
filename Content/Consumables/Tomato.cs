using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace test_mod.Content.Consumables
{
    internal class Tomato : ModItem
    {
        public static LocalizedText RestoreLifeText { get; private set; }
        public override void SetStaticDefaults()
        {
            RestoreLifeText = this.GetLocalization(nameof(RestoreLifeText));
            Item.ResearchUnlockCount = 5;

            // This is to show the correct frame in the inventory
            // The MaxValue argument is for the animation speed, we want it to be stuck on frame 1
            // Setting it to max value will cause it to take 414 days to reach the next frame
            // No one is going to have game open that long so this is fine
            // The second argument is the number of frames, which is 3
            // The first frame is the inventory texture, the second frame is the holding texture,
            // and the third frame is the placed texture
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(int.MaxValue, 3));


            ItemID.Sets.IsFood[Type] = true; // This allows it to be placed on a plate and held correctly
        }

        public override void SetDefaults()
        {

            // DefaultToFood sets all of the food related item defaults such as the buff type, buff duration, use sound, and animation time.
            Item.DefaultToFood(22, 22, BuffID.WellFed, 2500);
            Item.healLife = 5;
            Item.value = Item.buyPrice(0, 3);
            Item.rare = ItemRarityID.Blue;
        }

    }
}
