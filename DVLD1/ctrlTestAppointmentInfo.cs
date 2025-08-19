using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static System.Net.Mime.MediaTypeNames;
using static DVLD1.LocalDrivingLicenseApplicationForm_Table_View;

namespace DVLD1
{
    public partial class ctrlTestAppointmentInfo : UserControl
    {
        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        private Applications _application;

     
        public static TestAppointment _TestAppointment;
        public static Tests _tests;
        public static TestMode _Modetest;

        public ctrlTestAppointmentInfo()
        {
            InitializeComponent();

     
        }

        public ctrlTestAppointmentInfo(LocalDrivingLicenseApplication localDrivingLicenseApplication, Applications application)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = localDrivingLicenseApplication;
            _application = application;

        }

      

    


    public void LoadData(LocalDrivingLicenseApplication localDrivingLicenseApplication,Applications applications,TestMode testMode)
        {
            _LocalDrivingLicenseApplication=localDrivingLicenseApplication;
            _application = applications;
            _Modetest = testMode;
            // First, validate the inputs before using them
            if (_application == null || _LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Application or Local Driving License Application is null", "Error");
                return;
            }
            LocalDrivingLicenseApplication localDrivingLicenseApplication1= LocalDrivingLicenseApplication.Find(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
            _TestAppointment = TestAppointment.GetInfoTestID2(
                _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID
            );

            User user = User.Find(_application.CreatedByUserID);

            lblDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblClasse.Text = localDrivingLicenseApplication1.ClassName ?? string.Empty;
            lblTests.Text = localDrivingLicenseApplication1.PassedTestCount.ToString() + "/3";
            lblID.Text = _application.ApplicationID.ToString();
            lblStatus.Text = localDrivingLicenseApplication1.Status?.ToString() ?? string.Empty;
            lblfees.Text = _application.PaidFees.ToString();
            lblApplicant.Text = localDrivingLicenseApplication1.FullName ?? string.Empty;
            lblDate.Text = _application.ApplicationDate.ToString();
            lblStatusDate.Text = _application.LastStatusDate.ToString();
            lblCreatedBy.Text = user?.UserName ?? "Unknown";
            lblType.Text = "New Local Driving License Service";

            switch (_Modetest)
            {
                case TestMode.Vision:
                    lbltitle.Text = "Vision Test Appointments";
                    pictureBox1.Visible = true;
                    break;

                case TestMode.Writing:
                    lbltitle.Text = "Written Test Appointments";
                    pbWriting.Visible = true;
                    break;

                case TestMode.Street:
                    lbltitle.Text = "Street Test Appointments";
                    pbStreet.Visible = true;
                    break;
                case TestMode.Issue:
                    this.Controls.Remove(lbltitle);
                    this.Controls.Remove(pbStreet);
                    this.Controls.Remove(pbWriting);
                    this.Controls.Remove(pictureBox1);
                    break;
            }
        }

        private void ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 700,
                Height = 350,
                StartPosition = FormStartPosition.CenterScreen // 👈 Affiche le form au centre de l'écran

            };
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }
        private void linkPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            People people= People.Find(_application.ApplicantPersonID);
        
                CtrlShowDetailsPerson ctrlShowDetailsPerson = new CtrlShowDetailsPerson(people);
            ShowFormWithControl(ctrlShowDetailsPerson,"Show Person Details");
        }
    }
}
