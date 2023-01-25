using System;
using System.Windows.Forms;

namespace discord
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
            Application.Run(new LoginForm());
            if(!string.IsNullOrWhiteSpace(DiscordHelper.token))
			{
                Application.Run(new Form1());
            }
        }
    }
}
