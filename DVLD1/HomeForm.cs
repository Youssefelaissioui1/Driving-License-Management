using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace DVLD1
{
    public partial class HomeForm : Form
    {
        public HomeForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // 👈 Affiche le form au centre de l'écran

            notifyIcon1.Icon = SystemIcons.Application;
            //notifyIcon1.BalloonTipIcon = Properties.Resources.add;

            notifyIcon1.BalloonTipTitle = "Open App";
            notifyIcon1.BalloonTipText = "Welcome in Driver Lisence App\n You Have A New Tasks :-) ";
            notifyIcon1.ShowBalloonTip(10000);
        }



        private void HomeForm_Load(object sender, EventArgs e)
        {
            label1.Text = $"Bienvenue {GlobalUser.CurrentUser.UserName}";

        }

    

        private void lblPeople_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UsersForm usersForm = new UsersForm();
            usersForm.ShowDialog();
                }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }

   

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            GlobalUser.Logout();
          
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form.Name != "LoginForm") // ou form != this
                {
                    form.Close();
                }
            }
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

        }

        private void btnAccountSettings_Click(object sender, EventArgs e)
        {
            var buttonLocation = btnAccountSettings.PointToScreen(System.Drawing.Point.Empty);

            contextMenuStrip1.Show(buttonLocation.X, buttonLocation.Y + btnAccountSettings.Height);
        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationTypesForm applicationTypesForm = new ApplicationTypesForm();
            applicationTypesForm.Show();
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            var buttonLocation = btnApplications.PointToScreen(System.Drawing.Point.Empty);

            contextMenuStrip2.Show(buttonLocation.X, buttonLocation.Y + btnApplications.Height);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void manageApplicationsTypesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TestTypesForm TestTypesForm = new TestTypesForm();
            TestTypesForm.Show();
         
        }
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //LocalDrivingLicenseApplicationsForm localDrivingLicenseApplicationsForm = new LocalDrivingLicenseApplicationsForm();
            //localDrivingLicenseApplicationsForm.Show();
        }

        private void toolStripMenuItem4_MouseDown(object sender, MouseEventArgs e)
        {
       
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
        
        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationForm_Table_View localDrivingLicenseApplicationForm_Table_View = new LocalDrivingLicenseApplicationForm_Table_View();
            localDrivingLicenseApplicationForm_Table_View.ShowDialog();


        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewLocalLicenseForm form = new NewLocalLicenseForm();
            form.Size = new Size(760, 600); // Définir la taille avant
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        public void ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(700, 460), // Taille élégante et classique
                FormBorderStyle = FormBorderStyle.FixedDialog, // Optionnel : empêche le redimensionnement
                MaximizeBox = false // Optionnel : enlève le bouton agrandir
            };

            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }
        public void ShowFormWithControl1(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(800, 660), // Taille élégante et classique
                FormBorderStyle = FormBorderStyle.FixedDialog, // Optionnel : empêche le redimensionnement
                MaximizeBox = false // Optionnel : enlève le bouton agrandir
            };

            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }
        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CtrlShowDetailsUser ctrl =new CtrlShowDetailsUser(GlobalUser.CurrentUser.UserID);
            ShowFormWithControl(ctrl, "Show User Information");
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ctrlChangePassword ctrl = new ctrlChangePassword(GlobalUser.CurrentUser.UserID);
            ShowFormWithControl1(ctrl, "Change Password");
        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            DriversForm driversForm = new DriversForm();
            driversForm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewInternationalLicenseForm form = new AddNewInternationalLicenseForm();
            form.ShowDialog();

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            InternationalLicenseListForm internationalLicenseListForm = new InternationalLicenseListForm();
            internationalLicenseListForm.ShowDialog();
        }

        private void newDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLocalDrivingLicenseForm renewLocalDrivingLicenseForm = new RenewLocalDrivingLicenseForm(true);
            renewLocalDrivingLicenseForm.ShowDialog();
        }

        private void replacementForLostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplacementForDamagedOrLostLicenseForm replacementForDamagedOrLostLicenseForm = new ReplacementForDamagedOrLostLicenseForm();
            replacementForDamagedOrLostLicenseForm.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            DetainLicenseForm detainLicenseForm = new DetainLicenseForm();
            detainLicenseForm.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm releaseDetainedLicenseForm = new ReleaseDetainedLicenseForm();
            releaseDetainedLicenseForm.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ManageDetainedLicensesForm manageDetainedLicensesForm = new ManageDetainedLicensesForm();
            manageDetainedLicensesForm.ShowDialog();
        }

        private void releaseDetainedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm releaseDetainedLicenseForm1 = new ReleaseDetainedLicenseForm();
            releaseDetainedLicenseForm1.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationForm_Table_View localDrivingLicenseApplicationForm_Table_View = new LocalDrivingLicenseApplicationForm_Table_View();
            localDrivingLicenseApplicationForm_Table_View.ShowDialog();
        }
    }
}
