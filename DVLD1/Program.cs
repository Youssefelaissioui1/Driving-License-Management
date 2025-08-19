using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Form1 form = new Form1
            //{
            //    Width = 1200,
            //    Height = 650
            //};

            // Run the app with the customized Form1
            //Application.Run(new Form1());
            //Application.Run(new HomeForm());
            Application.Run(new LoginForm());
            //Application.Run(new UsersForm());
            //Application.Run(new LocalDrivingLicenseApplicationsForm());


        }
    }
}
