using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPong
{
    public class Paddle
    {
        public int x;
        public int y;
        public int sizex;
        public int sizey;
        public bool drawHitBox;

        public Rectangle hitbox;
        Graphics gfx;

        public Paddle(int x, int y, int sizex, int sizey)
        {
            this.x = x;
            this.y = y;
            this.sizex = sizex;
            this.sizey = sizey;
            hitbox = new Rectangle(x, y + 50, sizex, sizey);

        }

        public void draw(Graphics gfx)
        {
            gfx.FillRectangle(Brushes.DarkGreen, x, y, sizex, sizey);
        }


    }
}
