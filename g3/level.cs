using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Resources;
using g3.Properties;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace g3
{
    public class level
    {

        static private int MAX_MOBS = 10000;
        static private int MAX_STATICICONS = 500;
        private mob[] mobs = new mob[MAX_MOBS]; //holds details of all mobile icons on screen
        public int noMobs = 0; //holds no of mobs used -needed?
        private int Player; //holds location in mob array of player
        private staticIcon[] staticIcons = new staticIcon[MAX_STATICICONS]; //holds details of static objects on screen
        private int noStaticIcons = 0; //holds no of static icons used
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private Stopwatch stopwatch = Stopwatch.StartNew();
        public Form1 parentForm;
        public int screenOffsetX, screenOffsetY; //location of screen compared to origin
        private PictureBox background; //background to draw on
        public delegate void playerDeadHandler(object level, deadPlayerEventArgs e);
        public playerDeadHandler playerDead;

        public level(Form1 parentForm)
        {
            this.parentForm = parentForm;
            //initialise timer to update level
            
            newMob(mob.mobName.player);
            timer.Interval = 33; 
            timer.Tick += new EventHandler(mainUpdate);
            timer.Enabled = true;
            screenOffsetX = 0;
            screenOffsetY = 0;
            bgInit();
        }
        public void Dispose()
        {
            //timer.Tick -= mainUpdate;
            timer.Dispose();
            background.Dispose();
        }
        public void mainUpdate(object Sender, EventArgs e) //main update loop to be run periodically
        {
            spawnMobs();
            spawnPowerups();
            playerControl(); //move player
            moveScreen(); //update screen offset
            mobTargetUpdate(); //change bearing of mobs
            moveMobs(); //move mobs
            mobAttacks(); //calculate mob attacks
            updateMobStatus(); //kill dead mobs
            aliveTimerUpdate();
            updateReloadBar();
            updateHealthBar();
            bgUpdate(); //draw background
            drawMobs();
            drawStaticIcons();
        }

        private void updateHealthBar()
        {
            parentForm.HealthMax = (int) mobs[Player].HealthMax;
            parentForm.Health = (int) mobs[Player].Health;
        }
        private void updateReloadBar()
        {
            if (mobs[Player].canAttack)
            {
                parentForm.reloadBarVisible = false;
                return;
            }
            else
            {
                parentForm.reloadBarVisible = true;
                parentForm.reloadBarValue = (int) (mobs[Player].reloadProgress * 100);
            }

        }
        private void bgInit()
        {
            Image imgsrc = Properties.Resources.b1;
            background = new PictureBox();
            background.MouseClick += new MouseEventHandler(playerAttack);
            Image imgdest = new Bitmap(parentForm.Width, parentForm.Height);
            using (Graphics gr = Graphics.FromImage(imgdest))
            {
                gr.DrawImageUnscaledAndClipped(imgsrc,
                    new Rectangle(0, 0, parentForm.Width, parentForm.Height));
            }
            background.Image = imgdest;
            background.Width = parentForm.Width;
            background.Height = parentForm.Height;
            background.SendToBack();
            parentForm.Controls.Add(background);

        }
        private void spawnMobs()
        {
            Random rand = new Random(DateTime.Now.Millisecond); //seed is millisecond to seperate it from the random location spawning -TODO make more random
            if (rand.Next(5) == 1) newMob(mob.mobName.spider);
            if (rand.Next(10) == 2) newMob(mob.mobName.zombie);
            if (rand.Next(35) == 3) newMob(mob.mobName.human);
        }
        private void spawnPowerups()
        {
            Random rand = new Random();
            if (rand.Next(7) == 1) //spawn health
            {
                //pass positions to class to keep on same Random instance and distibute evenly
                staticIcons[noStaticIcons] = new powerHealth((rand.Next(parentForm.Width) + screenOffsetX), (rand.Next(parentForm.Height) + screenOffsetY));
                noStaticIcons++;
            }
        }
        void playerAttack(object sender, MouseEventArgs e) //event triggered on mouse click
        {
            int spread = 10; //area aound click that effects damage - TODO change according to difficulty settings
            if (mobs[Player].canAttack) //if i can attack
            {
                mobs[Player].lastFired = DateTime.Now;
                for (int i = 0; i < noMobs; i++)
                {
                    if (i == Player) continue; //dont allow player to attack self
                    if (e.X + spread >= mobs[i].xPos - screenOffsetX && e.X - spread <= mobs[i].xPos - screenOffsetX + 18 && //if on target
                        e.Y + spread >= mobs[i].yPos - screenOffsetY && e.Y - spread <= mobs[i].yPos - screenOffsetY + 18)
                    {
                        mobs[i].Health -= mobs[Player].attackDamage;
                        Console.WriteLine("Player attack on " + i);
                        //break;
                    }
                }
            }
        }
        private void bgUpdate() //move and tile background
        {
            Image imgsrc = Properties.Resources.b1;
            //TODO improve performance - currently taking ~13% cpu
            Image imgdest = new Bitmap(parentForm.Width, parentForm.Height);

            int xoff = screenOffsetX % parentForm.Width; //calculate screen offsets to control tiling
            int yoff = screenOffsetY % parentForm.Height;
            if (xoff <= 0) xoff += parentForm.Width;
            if (yoff <= 0) yoff += parentForm.Height;
            //Bitmap bitmap = Bitmap.FromFile("b1.jpg");
            using (Graphics gr = Graphics.FromHwnd(Form1.ActiveForm.Handle)) //draw 4 tiling backrounds on imgdest
            {
                BitmapSource bm = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.b1.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                //BitmapSource bm = BitmapSource.Create(Properties.Resources.b1.Width, Properties.Resources.b1.Height, 96, 96, PixelFormats.Bgr32, null, Properties.Resources.b1, (Properties.Resources.b1.Width * PixelFormats.Bgr32.BitsPerPixel + 7) / 8);
                CachedBitmap cachedBitmap = new CachedBitmap(bm,BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                cachedBitmap.
                gr.DrawImageUnscaledAndClipped(imgsrc,
                    new Rectangle(-xoff, -yoff, parentForm.Width, parentForm.Height));
                gr.DrawImageUnscaledAndClipped(imgsrc,
                    new Rectangle(parentForm.Width - xoff, -yoff, parentForm.Width, parentForm.Height));
                gr.DrawImageUnscaledAndClipped(imgsrc,
                    new Rectangle(-xoff, parentForm.Height - yoff, parentForm.Width, parentForm.Height));
                gr.DrawImageUnscaledAndClipped(imgsrc,
                    new Rectangle(parentForm.Width - xoff, parentForm.Height - yoff, parentForm.Width, parentForm.Height));
            }
            background.SendToBack();
            background.Image = imgdest; //update bg on form
            //imgdest.Dispose();
        }
        public void aliveTimerUpdate()
        {
            parentForm.aliveText = string.Format("{0:00}:{1:00}:{2:00}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds/10);
            //parentForm.aliveText = stopwatch.Elapsed.ToString("c");
        }
        private void mobTargetUpdate() //update mob speed and direction to target players & other mobs
        {
            for (int i = 0; i < noMobs; i++) //for all mobs
            {
                if (mobs[i].mobType == mob.mobName.player ||!mobs[i].Alive) continue; //dont look at players or dead
                double minS = double.MaxValue;
                int minSIndex = -1;

                //double minS = double.MaxValue;
                for (int j = 0; j < noMobs; j++) //cycle through other mobs to find closest
                {
                    if (j == i || mobs[j].mobType == mob.mobName.spider || mobs[j].Alive != true) continue; //don't test against self or spiders or dead TODO- update reference to spiders with list of mobs not to target
                    double dS; //distance to other mob
                    dS = Math.Sqrt(Math.Pow(mobs[i].xPos - mobs[j].xPos, 2) + Math.Pow(mobs[i].yPos - mobs[j].yPos, 2));
                    if (dS < minS && dS < mobs[i].sightRange) //if closest target in range so far, record as closest
                    {
                        minS = dS;
                        minSIndex = j;
                    }
                }

                //set bearing towards closest 
                if (minSIndex != -1) //if target found in range
                {
                    double acos = (mobs[i].xPos - mobs[minSIndex].xPos) / minS;
                    double asin = (mobs[i].yPos - mobs[minSIndex].yPos) / minS;
                    if ((acos < -1) || (asin < -1))
                        Console.WriteLine("bearing calc wrong!");

                    //minSIndex = 0;
                    //minS = Math.Sqrt(Math.Pow(mobs[i].xPos - mobs[minSIndex].xPos, 2) + Math.Pow(mobs[i].yPos - mobs[minSIndex].yPos, 2));

                    double dx = mobs[i].xPos - mobs[minSIndex].xPos;
                    double dy = mobs[i].yPos - mobs[minSIndex].yPos;
                    //mobs[i].bearing = Math.Acos((mobs[i].xPos - mobs[minSIndex].xPos) / minS) + Math.Asin((mobs[i].yPos - mobs[minSIndex].yPos) / minS);
                    if (dy < 0) mobs[i].bearing = Math.Atan(dx / dy);
                    else mobs[i].bearing = (Math.Atan(dx / dy) + Math.PI);
                    if (mobs[i].mobType == mob.mobName.human) mobs[i].bearing += Math.PI; //humans move away from other mobs
                }
                else //if none in range move slightly randomly
                {
                    Random rand = new Random();
                    mobs[i].bearing += (rand.NextDouble() - 0.5) / 3;
                }
                if(double.IsNaN(mobs[i].bearing)) //fixes erronious bearings created when minS == 0
                {
                    mobs[i].bearing = 0;
                }
            }
        }
        private void mobAttacks()
        {
            for (int i = 0; i < noMobs; i++) //for all mobs
            {
                if (mobs[i].Alive != true || mobs[i].mobType == mob.mobName.player) continue; //ignore dead & players
                for (int j = 0; j < noMobs; j++) //cycle through other mobs to find collisions
                {
                    if (j == i || mobs[j].Alive != true) continue; //don't test against self or dead
                    double dS;
                    dS = Math.Sqrt(Math.Pow(mobs[i].xPos - mobs[j].xPos, 2) + Math.Pow(mobs[i].yPos - mobs[j].yPos, 2));
                    if (dS < mobs[i].attackRange) //if j is in i's attack range
                    {
                        if (mobs[i].canAttack) //if i can attack
                        {
                            mobs[i].lastFired = DateTime.Now;
                            mobs[j].Health -= mobs[i].attackDamage;
                            //Console.WriteLine("Attack: " + i + " on " + j);
                        }
                    }
                }
            }
        }
        private void updateMobStatus()
        {
            if (!mobs[Player].Alive)
            {
                playerDead(this, new deadPlayerEventArgs(stopwatch.Elapsed));
            }
            for (int i = 0; i < noMobs; i++) //for all mobs
            {
                if (!mobs[i].Alive) continue;

                if (mobs[i].xPos + 50 < screenOffsetX || mobs[i].xPos - 50 > screenOffsetX + parentForm.Width ||
                    mobs[i].yPos + 50 < screenOffsetY || mobs[i].yPos - 50 > screenOffsetY + parentForm.Height)
                {
                    mobs[i].die(); //kill mobs that are away from screen to increase performance
                    //Console.WriteLine("Mob out of area: " + i);
                }
            }
        }
        public void playerControl() //move player
        {
            if (mobs[Player] != null) //if player active
            {
                mobs[Player].xPos += (int) mobs[Player].speed * (Form1.keysDown[(int)Keys.D] - Form1.keysDown[(int)Keys.A]);
                //mobs[Player].btn.Left = screenOffsetX + (int)mobs[Player].xPos;

                mobs[Player].yPos += (int) mobs[Player].speed * (Form1.keysDown[(int)Keys.S] - Form1.keysDown[(int)Keys.W]);
                //mobs[Player].btn.Top = screenOffsetY + (int)mobs[Player].yPos;
            }
        }
        public void moveScreen()
        {
            screenOffsetX += (int)mobs[Player].speed * (Form1.keysDown[(int)Keys.D] - Form1.keysDown[(int)Keys.A]);
            screenOffsetY += (int)mobs[Player].speed * (Form1.keysDown[(int)Keys.S] - Form1.keysDown[(int)Keys.W]);
        }
        public void moveMobs() //move mobs on update
        {
            for (int i = 0; i < noMobs; i++)
            {
                if (mobs[i].mobType == mob.mobName.player || mobs[i].Alive != true) continue; //dont move players or dead
                mobs[i].xPos += (int) (mobs[i].speed * Math.Sin(mobs[i].bearing));
                //mobs[i].btn.Left = -screenOffsetX + (int) mobs[i].xPos; //HACK - changes icon position manually- should happen in mob class? or on tick update?

                mobs[i].yPos += (int) (mobs[i].speed * Math.Cos(mobs[i].bearing));
                //mobs[i].btn.Top = -screenOffsetY + (int) mobs[i].yPos; //HACK - changes icon position manually- should happen in mob class?
            }
            if(Form1.ActiveForm != null) Form1.ActiveForm.Invalidate(); //redraws form
        }
        public void drawMobs()
        {
            //Image mobImg = new Bitmap(parentForm.Width, parentForm.Height);
            ResourceManager rm = Resources.ResourceManager;
            using (Graphics gr = Graphics.FromImage(background.Image))
            {
                for (int i = 0; i < noMobs; i++) //draw mobs individually
                {
                    if(mobs[i].Alive) gr.DrawImageUnscaledAndClipped((Image) rm.GetObject(mobs[i].iconPath) , new Rectangle(mobs[i].xPos-screenOffsetX, mobs[i].yPos-screenOffsetY, 18, 18)); //TODO - change icon size automatically
                }
            }
        }

        private void drawStaticIcons()
        {
            using (Graphics gr = Graphics.FromImage(background.Image))
            {
                for (int i = 0; i < noStaticIcons; i++) //draw icons on individually
                {
                    if (staticIcons[i].Active) gr.DrawImageUnscaledAndClipped(staticIcons[i].icon, new Rectangle(staticIcons[i].XPos - screenOffsetX, staticIcons[i].YPos - screenOffsetY, 18, 18));
                    //if (staticIcons[i].Active) gr.DrawImageUnscaledAndClipped(Image.FromFile(staticIcons[i].iconPath), new Rectangle(staticIcons[i].XPos - screenOffsetX, staticIcons[i].YPos - screenOffsetY, 18, 18)); //TODO - change icon size automatically
                    //if (staticIcons[i].Active) gr.DrawIconUnstretched(ico, new Rectangle(staticIcons[i].XPos - screenOffsetX, staticIcons[i].YPos - screenOffsetY, 18, 18));
                }
            }
        }
        public void newMob(mob.mobName mobType) //add mob
        {
            switch(mobType)
            {
                case (mob.mobName.player):
                    mobs[Array.IndexOf(mobs,null)] = new player(this);
                    Player = noMobs;
                    noMobs++;
                    break; 
                case (mob.mobName.zombie):
                    mobs[Array.IndexOf(mobs, null)] = new zombie(this);
                    noMobs++;
                    break; 
                case (mob.mobName.spider): 
                    mobs[Array.IndexOf(mobs, null)] = new spider(this);
                    noMobs++;
                    break;
                case (mob.mobName.human):
                    mobs[Array.IndexOf(mobs, null)] = new human(this);
                    noMobs++;
                    break; 
                default: 
                    Console.WriteLine("New Mob Default"); 
                    break;
            }
        }


    }

    
}

public class deadPlayerEventArgs : EventArgs
{
    public TimeSpan aliveTime;
    public deadPlayerEventArgs(TimeSpan aliveTime)
    {
        this.aliveTime = aliveTime;
    }

}