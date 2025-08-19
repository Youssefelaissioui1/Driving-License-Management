using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD1
{
    public partial class ReleaseDetainedLicenseForm : Form
    {
        public int _LicenseID;
        Licence _license;
        public int _DetainID;
        public Driver _driver;
        public People _people;
        public bool _FromDataGrivView = false; 
        public ReleaseDetainedLicenseForm()
        {
            InitializeComponent();
        }
        public ReleaseDetainedLicenseForm(Licence licence,bool FromDataGrivView=false)
        {
            InitializeComponent();
            _license = licence;
            _FromDataGrivView=FromDataGrivView;
        }
         
        public void LoadData(int id)
        {
            if (!DetainLicense.IsLicenseDetained(id))
            {
                MessageBox.Show("Selected License is not   Detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRelease.Enabled = false;
                linklblShowLicensesHistory.Enabled = true;
                return;
            }


            ApplicationTypes applicationTypes = new ApplicationTypes();
            DetainLicense detainLicense = DetainLicense.GetDetainedLicenseInfo(id);
            _DetainID = detainLicense.DetainID;
            lblDetainID.Text = detainLicense.DetainID.ToString();
            lblLicenseID.Text = id.ToString();
            lblDetainDate.Text = detainLicense.DetainDate.ToString("dd/MM/yyyy");
            lblFineFees.Text = detainLicense.FineFess.ToString();
            lblCreatedBy.Text = detainLicense.CreatedByUserID.ToString();
            lblApplicationFees.Text = ApplicationTypes.FindApplicationTypeByID(5).ApplicationFees.ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblApplicationFees.Text) + Convert.ToDecimal(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;
            linklblShowLicensesHistory.Enabled = true;

        }
        private void ReleaseDetainedLicenseForm_Load(object sender, EventArgs e)
        {
            if (_license == null)
            {
                ctrlShearchDrivingLicense1.LicenseSelected += id =>
                {
                    _LicenseID = id;
                    Licence licence = Licence.GetLicenseInfoByLicenseID(_LicenseID);
                    _license = licence;
                    LoadData(id);
                };
            }
            else
                  {  if (_FromDataGrivView)
                ctrlShearchDrivingLicense1.GbFiltrerEnabled(_license.LicenseID);
                ctrlShearchDrivingLicense1.LoadDataCtrlShearch(_license);
                LoadData(_license.LicenseID);
            }

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (_license == null)
            {
                MessageBox.Show("Please select a license first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Driver driver = Driver.GetInfoDriverByPersonID(_license.DriverID);
            People people = People.Find(driver.PersonID);
            _driver = driver;
            _people = people;
            Applications applications1 = new Applications();
            applications1 = Applications.GetApplicationsbyID(_license.ApplicationID);

            Applications applications = new Applications();

            applications = new Applications
            {

                ApplicantPersonID = people.PersonID,
                ApplicationDate = DateTime.Now,
                ApplicationTypeID = 5,
                ApplicationStatus = 3,
                LastStatusDate = DateTime.Now,
                PaidFees = Convert.ToInt32(lblApplicationFees.Text),
                CreatedByUserID = GlobalUser.CurrentUser.UserID,
            };
            if(applications._AddNewApplications())
          
        {   
                if (DetainLicense.ReleaseDetainedLicense(_DetainID, DateTime.Now, GlobalUser.CurrentUser.UserID, applications.ApplicationID))
                {
                    MessageBox.Show("License released successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblApplicationid.Text = applications.ApplicationID.ToString();
                    btnRelease.Enabled = false;
                    linkLblShowLicensesInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Failed to release the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linklblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (_people == null)
            {
                Driver driver = Driver.GetInfoDriverByPersonID(_license.DriverID);
                People people = People.Find(driver.PersonID);
                _driver = driver;
                _people = people;
            }

            LicenseHistoryForm LicenseHistoryForm = new LicenseHistoryForm(_people);

            LicenseHistoryForm.ShowDialog();
        }

        private void linkLblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseForm showLicenseForm = new ShowLicenseForm(_license);
            showLicenseForm.LoadData();
            showLicenseForm.ShowDialog();
        }
    }
}
