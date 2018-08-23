using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingPong
{
    public partial class Form1 : Form
    {

              
        static void drawText(Graphics gfx, string drawString, int fontSize, int x, int y)
        {
            System.Drawing.Font drawFont = new System.Drawing.Font("Comic Sans MS", fontSize);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
            gfx.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
        }

        static Pen greenPen = new Pen(Color.Green);
        static Pen bluePen = new Pen(Color.Blue, 5);
        static Random random = new Random();
        Graphics gfx;
        Paddle paddle1 = new Paddle(50, 100, 15, 100);
        Paddle paddle2 = new Paddle(300, 100, 15, 100);
        Ball ball = new Ball(250, 250, 10, 5, 20, Brushes.Gold);
        bool bot = true;

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            gfx = CreateGraphics();
            KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //initializes code (resets everything)
            paddle1.x = 50;
            paddle1.y = 150;
            ball.x = ClientSize.Width / 2;
            ball.y = ClientSize.Height / 2;

            paddle2.x = ClientSize.Width - 50;
            paddle2.hitbox.X = paddle2.x;

            paddle2.y = 150;
            //label1.BackColor = Color.Transparent;
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (bot)
            {
                //code for single player to control with arrow keys
                if (e.KeyCode == Keys.S && paddle1.y < 345)
                {
                    paddle1.y += 2;
                    paddle1.hitbox.Y += 2;
                }
                if (e.KeyCode == Keys.W && paddle1.y > 5)
                {
                    paddle1.y -= 2;
                    paddle1.hitbox.Y -= 2;
                }

                if (paddle2.y < 345 && paddle2.y > 5)
                {
                    paddle2.y = ball.y;
                    paddle2.hitbox.Y = ball.hitbox.Y;
                }
            }
           
            if(!bot)
            {
                //code for player one
                if (e.KeyCode == Keys.S && paddle1.y < 345)
                {
                    paddle1.y += 2;
                    paddle1.hitbox.Y += 2;
                }
                if (e.KeyCode == Keys.W && paddle1.y > 5)
                {
                    paddle1.y -= 2;
                    paddle1.hitbox.Y -= 2;
                }
                //Code for player two
                if (e.KeyCode == Keys.Down && paddle1.y < 345)
                {
                    paddle2.y += 2;
                    paddle2.hitbox.Y += 2;
                }
                if (e.KeyCode == Keys.Up && paddle1.y > 5)
                {
                    paddle2.y -= 2;
                    paddle2.hitbox.Y -= 2;
                }
            }

           

            if (e.KeyCode == Keys.Escape)
            {
                option form = new option(bot, timer1, ball, paddle1, paddle2, this);
                ball.game = false;
                timer1.Enabled = false;
                //form.ShowDialog();
                form.Show();
                //this.Show();
            }
        }

        public void Confirm(bool bot)
        {
            this.bot = bot;
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(Color.LightBlue);

            

            paddle1.draw(gfx);
            paddle2.draw(gfx);
            ball.Draw(gfx);
            if (ball.game)
            {
                ball.Move(ClientSize.Width, ClientSize.Height, paddle1, paddle2);
            }
            if (paddle1.drawHitBox == true && paddle2.drawHitBox == true)
            {

                //paddle1 hitbox
                gfx.DrawRectangle(bluePen, paddle1.hitbox.X, paddle1.hitbox.Y, paddle1.hitbox.Width, paddle1.hitbox.Height);

                //paddle2 hitbox right
                gfx.DrawRectangle(bluePen, paddle2.hitbox.X, paddle2.hitbox.Y, paddle2.hitbox.Width, paddle2.hitbox.Height);
            }
            if (bot)
            {
                if (ball.y < ClientSize.Height - 100 && ball.y > 5)
                {
                    paddle2.y = ball.hitbox.Y;
                    paddle2.hitbox.Y = paddle2.y;
                }
            }


            greenPen.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
            // Draw a line.

            gfx.DrawLine(greenPen, ball.left, 0, paddle1.x - 40, ClientSize.Height);
            gfx.DrawLine(greenPen, ball.right, 0, ball.right, ClientSize.Height);


            // Change the SmoothingMode to none.
            gfx.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.None;

            drawText(gfx, "Rally: " + ball.rally, 48, 296, 9);
            drawText(gfx, "Paddle 1: " + ball.score1, 20, 23, 37);
            drawText(gfx, "Paddle 2: " + ball.score2, 20, 707, 37);

            if(ball.x <= ball.left)
            {
                //if ball is on left wall, point to right player
                ball.rally = 0;
                Console.Beep(300, 500);
                ball.game = false;                
                ball.score2++;
                paddle1.y = ClientSize.Height / 2;
                paddle2.y = ClientSize.Height / 2;
                ball.x = ClientSize.Width / 2;
                ball.y = ClientSize.Height / 2;
                if (ball.score2 == 3)
                {
                    MessageBox.Show("Player 2 Won!");

                }
                else
                {
                    MessageBox.Show("Player 2 Won 1 Point");
                }
                paddle1.hitbox.Y = paddle1.y;
                paddle2.hitbox.Y = paddle2.y;
                ball.game = true;
            }
            if (ball.x >= ball.right)
            {
                //if ball is on right wall, point is to left player
                ball.rally = 0;
                Console.Beep(300, 500);
                ball.game = false;                
                ball.score1++;
                paddle1.y = ClientSize.Height / 2;
                paddle2.y = ClientSize.Height / 2;
                ball.x = ClientSize.Width / 2;
                ball.y = ClientSize.Height / 2;
                if (ball.score1 == 3)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Player 1 Won!");
                    ball.x = ClientSize.Width / 2;
                    ball.y = ClientSize.Height / 2;
                    paddle1.y = ClientSize.Height / 2;
                    paddle2.y = ClientSize.Height / 2;

                    ball.score1 = 0;
                    ball.score2 = 0;
                    ball.rally = 0;
                    timer1.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Player 1 Won 1 Point");
                }

                paddle1.hitbox.Y = paddle1.y;
                paddle2.hitbox.Y = paddle2.y;
                ball.game = true;
            }
           
            if (ball.score2 == 3)
            {
                timer1.Enabled = false;
                MessageBox.Show("Player 2 Won!");
                ball.x = ClientSize.Width / 2;
                ball.y = ClientSize.Height / 2;
                paddle1.y = ClientSize.Height / 2;
                paddle2.y = ClientSize.Height / 2;

                ball.score1 = 0;
                ball.score2 = 0;
                ball.rally = 0;
                timer1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ball.x = ClientSize.Width / 2;
            ball.y = ClientSize.Height / 2;
            paddle1.y = ClientSize.Height / 2;
            paddle2.y = ClientSize.Height / 2;

            ball.score1 = 0;
            ball.score2 = 0;
            ball.rally = 0;

          
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
