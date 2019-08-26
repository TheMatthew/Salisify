using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace salsa_beats
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (System.IO.FileNotFoundException e)
            {
                
                MessageBox.Show("The program unnexpectedly crashed, please tell Matthew" + "\nExcpetion : " + e.ToString());
            }
        }
    }
}
