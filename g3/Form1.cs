using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace g3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //level Level = new level();

            //for (int i = 0; i < 256; i++) keysDown[i] = 0;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            Level = new level(this);
            Level.playerDead += new level.playerDeadHandler(Level_playerDead);

        }

        void Level_playerDead(object level, deadPlayerEventArgs e) //executes on player death
        {
            this.Close();
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("Form1 closing");
            Level.Dispose();
            Level = null;            
        }
        public level Level;// = new level(); //spawn level

        public static int[] keysDown = new int[256]; //record which keys are being pressed

        public int Health
        {
            set 
            {
                try
                {
                    healthBar.Value = value;
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Health bar out of range");
                }
            }
        }
        public int HealthMax
        {
            set { healthBar.Maximum = value; }
        }
        public int reloadBarValue 
        { 
            get {return reloadBar.Value;} 
            set {
                reloadBar.Visible = true;
                try
                {
                    reloadBar.Value = value;
                    reloadBar.Value -= 1; //hack to force controls to redraw faster
                    reloadBar.Value = value;
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Reload bar out of range");
                }
                //reloadBar.Update();
                reloadBar.BringToFront();
            } 
        }
        public bool reloadBarVisible {
            get { return reloadBar.Visible; }
            set { 
                reloadBar.Visible = value;
            }
        }

        void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keysDown[(int) e.KeyCode] = 0;
        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            keysDown[(int)e.KeyCode] = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // mob test = new mob();
            Level.newMob(mob.mobName.player);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Level.newMob(mob.mobName.zombie);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Parent.L = new level();
            //spider test = new spider();
            Level.newMob(mob.mobName.spider);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Level.moveMobs();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Level.newMob(mob.mobName.human);
        }

    }
}
