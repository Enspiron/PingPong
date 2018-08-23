using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace PingPong
{
    public class Ball
    {
        public static SystemSound Beep { get; }
        Random random = new Random();


        //locaion of ball
        public int x;
        public int y;

        //speed that ball goes at
        public int xspeed;
        public int yspeed;
        public int size;

        //Times the paddle has scored
        public int score1 = 0;
        public int score2 = 0;

        //how many times the ball has hit the paddles
        public int rally;

        //for border
        public int left = 10;
        public int right = 875;

        public bool game = true;
        Brush color;

        public Rectangle hitbox;

        public Ball(int x, int y, int xspeed, int yspeed, int size, Brush color)
        {
            this.x = x;
            this.y = y;
            this.xspeed = xspeed;
            this.yspeed = yspeed;
            this.color = color;
            this.size = size;

            hitbox = new Rectangle(x, y, size, size);
        }

        public void Move(int csWidth, int csHeight, Paddle paddle1, Paddle paddle2)
        {
            x += xspeed;
            y += yspeed;
            hitbox = new Rectangle(x, y, size, size);
            if (x > csWidth - 25)
            {
                xspeed = -xspeed;
            }
            if (y > csHeight - 25)
            {
                yspeed = -yspeed;
            }
            if (x < 5)
            {
                xspeed = -xspeed;
            }
            if (y < 5)
            {
                yspeed = -yspeed;
            }
            if ((x > paddle1.hitbox.X) && (x < paddle1.hitbox.X + paddle1.hitbox.Width) && (y > paddle1.hitbox.Y) && (y < paddle1.hitbox.Y + paddle1.hitbox.Height))
            {
                rally++;
                Console.Beep();
                xspeed = -xspeed;
                //yspeed = -yspeed;
            }
            if ((x > paddle2.hitbox.X - paddle2.hitbox.Width) && (x > paddle2.hitbox.X - paddle2.hitbox.Width) && (y > paddle2.hitbox.Y) && (y < paddle2.hitbox.Y + paddle2.hitbox.Height))
            {
                rally++;
                Console.Beep();
                xspeed = -xspeed;
                //yspeed = -yspeed;
            }

        }


        public void Draw(Graphics gfx)
        {
            gfx.FillEllipse(color, x, y, size, size);
        }
    }
}
