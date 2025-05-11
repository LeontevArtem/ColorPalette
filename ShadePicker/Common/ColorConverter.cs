using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadePicker.Common
{
    public static class ColorConverter
    {
        public static double RgbToHue(int r, int g, int b)
        {
            // Normalize RGB values to the range [0, 1]
            double rNorm = r / 255.0;
            double gNorm = g / 255.0;
            double bNorm = b / 255.0;

            // Find the min and max values of R, G, and B
            double max = Math.Max(rNorm, Math.Max(gNorm, bNorm));
            double min = Math.Min(rNorm, Math.Min(gNorm, bNorm));
            double delta = max - min;

            double hue = 0.0;

            if (delta == 0)
            {
                hue = 0; // No color difference
            }
            else if (max == rNorm)
            {
                hue = 60 * (((gNorm - bNorm) / delta) % 6);
            }
            else if (max == gNorm)
            {
                hue = 60 * (((bNorm - rNorm) / delta) + 2);
            }
            else if (max == bNorm)
            {
                hue = 60 * (((rNorm - gNorm) / delta) + 4);
            }

            // Ensure hue is positive
            if (hue < 0)
            {
                hue += 360;
            }

            return hue;
        }
        public static (byte R, byte G, byte B) HueToRgb(float hue, float saturation, float lightness)
        {
            hue = hue % 360; // Ensure hue is within 0-360
            if (hue < 0) hue += 360;

            float chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            float x = chroma * (1 - Math.Abs((hue / 60) % 2 - 1));
            float m = lightness - chroma / 2;

            float r = 0, g = 0, b = 0;

            if (hue >= 0 && hue < 60)
            {
                r = chroma; g = x; b = 0;
            }
            else if (hue >= 60 && hue < 120)
            {
                r = x; g = chroma; b = 0;
            }
            else if (hue >= 120 && hue < 180)
            {
                r = 0; g = chroma; b = x;
            }
            else if (hue >= 180 && hue < 240)
            {
                r = 0; g = x; b = chroma;
            }
            else if (hue >= 240 && hue < 300)
            {
                r = x; g = 0; b = chroma;
            }
            else if (hue >= 300 && hue < 360)
            {
                r = chroma; g = 0; b = x;
            }

            // Convert to 0-255 range
            byte R = (byte)((r + m) * 255);
            byte G = (byte)((g + m) * 255);
            byte B = (byte)((b + m) * 255);

            return (R, G, B);
        }
    }
}
