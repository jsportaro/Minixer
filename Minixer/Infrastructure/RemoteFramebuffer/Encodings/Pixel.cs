using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Infrastructure.RemoteFramebuffer.Encodings
{
    public class Pixel
    {
        public byte Red { get; private set; }
        public byte Green { get; private set; }
        public byte Blue { get; private set; }

        public int GdiPixel { get; private set; }

        public Pixel(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public Pixel(int color)
        {
            Red = (byte)(color << 16);
            Green = (byte)(color << 8);
            Blue = (byte)(color & 0xFF);
        }

        public int ToGdiPlusOrder(byte red, byte green, byte blue)
        {
            // Put colour values into proper order for GDI+ (i.e., BGRA, where Alpha is always 0xFF)
            return blue & 0xFF | green << 8 | red << 16 | 0xFF << 24;
        }

        public byte[] ToArray()
        {
            return new[] { Red, Green, Blue, (byte)0xFF };
        }
    }
}
