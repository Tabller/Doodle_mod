using DoodleMod.Content.Biomes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoodleMod.Content.Tiles
{
    public class DoodleBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            DustType = DustID.Ash;
            VanillaFallbackOnModDeletion = TileID.DiamondGemspark;

            AddMapEntry(new Color(200, 200, 200));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void ChangeWaterfallStyle(ref int style)
        {
            style = ModContent.GetInstance<DoodleWaterfallStyle>().Slot;
        }
    }
}