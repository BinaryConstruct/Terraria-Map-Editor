﻿using Microsoft.Xna.Framework;
using TEditXNA.Terraria;

namespace TEditXna.Render
{
    public static class PixelMap
    {
        #region Alpla Blend
        public static Color AlphaBlend(this Color background, Color color)
        {
            return AlphaBlend(background.A, background.R, background.B, background.G,
                              color.A, color.R, color.B, color.G);
        }
        public static Color AlphaBlend(this Color background, System.Windows.Media.Color color)
        {
            return AlphaBlend(background.A, background.R, background.B, background.G,
                              color.A, color.R, color.B, color.G);
        }
        public static Color AlphaBlend(this System.Windows.Media.Color background, Color color)
        {
            return AlphaBlend(background.A, background.R, background.B, background.G,
                              color.A, color.R, color.B, color.G);
        }
        public static Color AlphaBlend(this System.Windows.Media.Color background, System.Windows.Media.Color color)
        {
            return AlphaBlend(background.A, background.R, background.B, background.G,
                              color.A, color.R, color.B, color.G);
        }
        public static Color AlphaBlend(byte a1, byte r1, byte b1, byte g1, byte a2, byte r2, byte b2, byte g2)
        {
            var a = (byte)((a2 / 255F) * a2 + (1F - a2 / 255F) * a1);
            var r = (byte)((a2 / 255F) * r2 + (1F - a2 / 255F) * r1);
            var g = (byte)((a2 / 255F) * g2 + (1F - a2 / 255F) * g1);
            var b = (byte)((a2 / 255F) * b2 + (1F - a2 / 255F) * b1);
            return Color.FromNonPremultiplied(r, g, b, a);
        }
        #endregion

        public static Color GetTileColor(Tile tile, Color background, bool showWall = true, bool showTile = true, bool showLiquid = true, bool showWire = true)
        {
            var c = new Color(0, 0, 0, 0);

            if (tile.Wall > 0 && showWall)
                if (World.WallProperties.Count > tile.Wall)
                    c = c.AlphaBlend(World.WallProperties[tile.Wall].Color);
                else
                    c = c.AlphaBlend(Color.Magenta); // Add out-of-range colors
            else
                c = background;

            if (tile.IsActive && showTile)
            {
                if (World.TileProperties.Count > tile.Type)
                    c = c.AlphaBlend(World.TileProperties[tile.Type].Color);
                else
                    c = c.AlphaBlend(Color.Magenta); // Add out-of-range colors
            }

            if (tile.Liquid > 0 && showLiquid)
                c = c.AlphaBlend(tile.IsLava ? World.GlobalColors["Lava"] : World.GlobalColors["Water"]);

            if (showWire){
                if (tile.HasWire)
                {
                    c = c.AlphaBlend(World.GlobalColors["Wire"]);
                }
                if (tile.HasWire2)
                {
                    c = c.AlphaBlend(World.GlobalColors["Wire1"]);
                }
                if (tile.HasWire3)
                {
                    c = c.AlphaBlend(World.GlobalColors["Wire2"]);
                }
            }

            return c;
        }
    }
}