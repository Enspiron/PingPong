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
    public partial class option : Form
    {
        private bool bot;
        private bool game;
        private Timer timer1;
        private Ball ball;
        private Paddle paddle1;
        private Paddle paddle2;
        private Form1 form;
        Graphics gfx;



        public option(bool bot, Timer timer1, Ball ball, Paddle paddle1, Paddle paddle2, Form1 form)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.form = form;
            if (bot)
            {
                label1.Text = "Against Bot";
                radioButton1.Checked = true;
            }            
            else
            {
                label1.Text = "Local";
                radioButton2.Checked = true;
            }
            gfx = CreateGraphics();
            this.ball = ball;
            this.bot = bot;
            KeyPreview = true;
            this.timer1 = timer1;
        }

        private void option_Load(object sender, EventArgs e)
        {
          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ball.game = true;
            timer1.Enabled = true;
            this.Hide();
            
        }
        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label1.Text = "Against Bot";
            bot = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            bot = false;
            label1.Text = "Local";

        }

        private void option_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ball.game = true;
                timer1.Enabled = true;
                this.Hide();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ball.score1 = 0;
            ball.score2 = 0;
            ball.rally = 0;

            ball.x = ClientSize.Height / 2;
            ball.y = ClientSize.Width / 2;
            ball.hitbox.X = ClientSize.Height / 2;
            ball.hitbox.Y = ClientSize.Width / 2;

           


            if (radioButton1.Checked)
            {
                bot = true;
                label1.Text = "Against Bot";

            }
            else if (radioButton2.Checked)
            {
                bot = false;
                label1.Text = "Local";

            }

            form.Confirm(bot);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            form.Close();
        }
    }
}
