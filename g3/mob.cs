using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace g3
{
    public abstract class mob
    {
        public enum mobName { zombie, spider, human, player };
        public mobName mobType;
        public int xPos, yPos;
        public double bearing, speed, attackDamage, attackRange, sightRange;
        protected double attackTime;
        public string iconPath;
        private bool alive;
        public DateTime lastFired;
        protected double healthMax, health;
        public mob(level Level)
        {
            //addIconControl(imgURL);
            Random rand = new Random();
            xPos = Level.screenOffsetX + rand.Next(Level.parentForm.Width);
            yPos = Level.screenOffsetY + rand.Next(Level.parentForm.Height);
            const int ctrSize = 200;
            if (Level.parentForm.Width / 2 - ctrSize < xPos - Level.screenOffsetX && xPos - Level.screenOffsetX < Level.parentForm.Width / 2 + ctrSize &&
                Level.parentForm.Height / 2 - ctrSize < yPos - Level.screenOffsetY && yPos - Level.screenOffsetY < Level.parentForm.Height / 2 + ctrSize)
                xPos = xPos > Level.parentForm.Width + Level.screenOffsetX ? xPos += ctrSize : xPos -= ctrSize;//dont spawn right is middle of screen next to player
            bearing = rand.NextDouble() * 2 * Math.PI; //assign random bearing
            alive = true;
            lastFired = DateTime.MinValue;
            attackTime = 10000000; 
            sightRange = 350;
        }
        public bool canAttack 
        { 
            get 
            {
                if (lastFired.AddTicks((long) attackTime).CompareTo(DateTime.Now) < 0) return true;
                else return false;
            } 
        }
        public double HealthMax { get { return healthMax; }}
        public double Health 
        {
            get { return health; }
            set
            {
                health = value;
                if (health <= 0)
                {
                    die();
                    //if (mobType = mobName.player) level.playerDeadHandler
                }
                else if (health > healthMax)
                    health = healthMax;
            }
        }
        public double reloadProgress 
        {
            get { return (DateTime.Now - lastFired).Ticks/ attackTime; }
        }
        public bool Alive
        {
            get { return alive; }
        }
        public int XPos 
        {
            get { return xPos; }
            set
            {
                xPos = value;
            }
        }
        public int YPos
        {
            get { return yPos; }
            set
            {
                yPos = value;
            }
        }
        public void die()
        {
            alive = false;
        }
    }

    public class zombie : mob
    {
        public zombie(level Level) :base(Level)
        {
            mobType = mobName.zombie;
            iconPath = "r02";
            speed = 3;
            health = 35;
            attackDamage = 10;
            attackRange = 10;
            healthMax = health;
        }
    }
    public class spider : mob
    {
        public spider(level Level) : base(Level)
        {
            mobType = mobName.spider;
            iconPath = "r01";
            health = 30;
            speed = 8;
            attackDamage = 5;
            attackRange = 10;
            healthMax = health;
        }
    }
    public class human : mob
    {
        public human(level Level) : base(Level)
        {
            iconPath = "humanIcon";
            mobType = mobName.human;
            speed = 2;
            health = 50;
            attackDamage = 30;
            attackRange = 30;
            attackTime = 12000000;
            healthMax = health;
        }
    }
    public class player : human
    {
        public player(level Level) : base(Level)
        {
            attackTime = 3000000;
            mobType = mobName.player;
            speed = 7;
            xPos = Level.screenOffsetX + Level.parentForm.Width / 2;
            yPos = Level.screenOffsetY + Level.parentForm.Height / 2 - 9;
            //addIconControl("humanIcon.gif", Level);
            health = 100;
            healthMax = health;
        }
    }
}
