using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace g3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Form1 levelForm = new Form1();
            //Application.Run(levelForm);
            mainMenu menu = new mainMenu();
            Application.Run(menu) ;
        }

    }
}
