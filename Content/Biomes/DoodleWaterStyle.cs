﻿using DoodleMod.Content.Biomes.Doodle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoodleMod.Content.Biomes
{
    public class DoodleWaterStyle : ModWaterStyle
    {
        private Asset<Texture2D> rainTexture;
        public override void Load()
        {
            rainTexture = Mod.Assets.Request<Texture2D>("Content/Biomes/DoodleRain");
        }

        public override int ChooseWaterfallStyle()
        {
            return ModContent.GetInstance<DoodleWaterStyle>().Slot;
        }

        public override int GetSplashDust()
        {
            return DustID.Adamantite;
        }

        public override int GetDropletGore()
        {
            return ModContent.GoreType<DoodleDroplet>();
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }

        public override Color BiomeHairColor()
        {
            return Color.White;
        }

        public override byte GetRainVariant()
        {
            return (byte)Main.rand.Next(3);
        }

        public override Asset<Texture2D> GetRainTexture() => rainTexture;
    }
}