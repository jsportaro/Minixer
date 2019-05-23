using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minixer.Infrastructure.RemoteFramebuffer
{
    public class Rectangle
    {
        private Point point;
        private object size;

        public int X { get; set; }
        public int Y { get; set; }
        public int  Width { get; set; }
        public int Height { get; set; }
        public int Top => Y;

        public Rectangle()
        {

        }

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

    }
}
