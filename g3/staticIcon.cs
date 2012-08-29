using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace g3
{
    public abstract class staticIcon
    {
        public enum staticTypes {powerHealth};
        protected staticTypes staticType;
        protected int xPos, yPos;
        protected bool active; 
        public string iconPath;
        public Image icon;

        public staticIcon()
        {
            active = true;
        }
        public bool Active { get { return active; } }
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
    }
    public class powerHealth : staticIcon
    {
        public static int healthValue = 20; //amount to heal player
        public powerHealth(int XPos, int YPos)
        {
            //icon = Image.FromFile("health.gif");
            icon = Properties.Resources.health;

            iconPath = "health.gif";
            staticType = staticTypes.powerHealth;
            xPos = XPos;
            yPos = YPos;
        }
    }
}
