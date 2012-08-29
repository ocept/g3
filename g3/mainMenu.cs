using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace g3
{
    public partial class mainMenu : Form
    {
        public mainMenu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playButton_Click(object sender, EventArgs e) //play game
        {
            Form1 levelForm = new Form1();
            levelForm.Visible = true;
            levelForm.Activate();
            levelForm.FormClosing += new FormClosingEventHandler(levelForm_FormClosing);
            this.Visible = false;
            levelForm.Level.playerDead += new level.playerDeadHandler(Level_playerDead);
        }

        void Level_playerDead(object level, deadPlayerEventArgs e) //update menu text based on last play
        {
            string minutes = e.aliveTime.Minutes > 0 ? (e.aliveTime.Minutes > 1 ? (e.aliveTime.Minutes.ToString() + " Minutes and ") : (e.aliveTime.Minutes.ToString() + " Minute and ")) : "";
            timerTextLabel.Text = "You survived for " + minutes + e.aliveTime.Seconds + " Seconds";
            timerTextLabel.Visible = true;
        }

        void levelForm_FormClosing(object sender, FormClosingEventArgs e) //show menu again
        {
            this.Visible = true;
            this.Activate();
        }

    }
}
