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

namespace DVLD1
{
    public partial class RenewLocalDrivingLicenseForm : Form
    {
        public int _LicenseID;
        //ctrlShearchDrivingLicense _ctrlShearchDrivingLicense;
        static bool _Renew;
        Licence _license;
        //Applications _applications;
        public int LicenseID;
        public Driver _driver;
        public People _people;
        public int OldLicenseID;
        public RenewLocalDrivingLicenseForm()
        {
            InitializeComponent();

     
        }
        public RenewLocalDrivingLicenseForm(bool Renew=false)
        {
            InitializeComponent();
            _Renew = Renew;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //public void LoadData2()
        //{
        //    gbApplicationNewLicenseInfo
        //}
        public void LoadData(Licence licence)
        {
         

            LicenseClasses licenseClasses = new LicenseClasses();
            licenseClasses = LicenseClasses.Find(licence.LicenseClass);
            lblCreatedBy.Text = GlobalUser.CurrentUser.UserName;
            lblFees.Text = ApplicationTypes.FindApplicationTypeByID(2).ApplicationFees.ToString();
            lblLicenseFees.Text = licenseClasses.ClassFees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblExpirationDate2.Text = (DateTime.Now.AddYears(licenseClasses.DefaultValidityLength)).ToString();
            lblTotalFees.Text = (Convert.ToDecimal(lblFees.Text) + Convert.ToDecimal(lblLicenseFees.Text)).ToString();
           lblLocalLicenseID.Text = licence.LicenseID.ToString();
            OldLicenseID=licence.LicenseID;


        }
        private void RenewLocalDrivingLicenseForm_Load(object sender, EventArgs e)
        {
       

            ctrlShearchDrivingLicense2.LicenseSelected += id =>
            {
                _LicenseID = id;
                Licence licence = Licence.GetLicenseInfoByLicenseID(_LicenseID);
                _license=licence;
                if (licence.ExpirationDate >= DateTime.Now)
                {
                    MessageBox.Show("Selected License is not  yet Expiared ,it will expire on:" + licence.ExpirationDate.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnRenew.Enabled = false;
                    linklblShowLicensesHistory.Enabled = true;
                    return;
                }
                linklblShowLicensesHistory.Enabled = true;
                LoadData(licence);
            };
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

        private void btnRenew_Click(object sender, EventArgs e)
        {
            //_license = Licence.GetLicenseInfoByLicenseID(LicenseID);

            if (_license == null)
            {
                MessageBox.Show("License not found for the selected ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Applications applications = new Applications();
            Driver driver = Driver.GetInfoDriverByPersonID(_license.DriverID);
            applications = Applications.GetApplicationsbyID(_license.ApplicationID);
            People people = People.Find(driver.PersonID);
            _driver = driver;
            _people = people;

            ApplicationTypes applicationTypes = ApplicationTypes.FindApplicationTypeByID(2);
            //Applications applications = Applications.GetApplicationsbyID(_license.ApplicationID);
            applications = new Applications
                {

                    ApplicantPersonID = people.PersonID,
                    ApplicationDate = DateTime.Now,
                    ApplicationTypeID = applications.ApplicationTypeID,
                    ApplicationStatus = 3,
                    LastStatusDate = DateTime.Now,
                    PaidFees = Convert.ToInt32(applicationTypes.ApplicationFees.ToString()),
                    CreatedByUserID = GlobalUser.CurrentUser.UserID,
                };

                DialogResult result = MessageBox.Show("Are you sure you want to Renew  license?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (applications._AddNewApplications())
                 
                    _license = new Licence
                        {
                            ApplicationID = applications.ApplicationID,
                            DriverID = _license.DriverID,
                            LicenseClass = _license.LicenseClass,
                             IssueDate = DateTime.Now,
                            ExpirationDate = DateTime.Now.AddYears(LicenseClasses.Find(_license.LicenseClass).DefaultValidityLength),
                             Notes = tbxNotes.Text,
                             PaidFees=Convert.ToInt32(lblTotalFees.Text),
                            IsActive = true,
                             IssueReason = 2,
                        CreatedByUserID = GlobalUser.CurrentUser.UserID

                        };
                        if (_license.AddNewLicence())
                        {
                    if (Licence.UpdateIsActiveLIcenseToFalse(OldLicenseID))
                    {
                        MessageBox.Show("Applications Added with successfully with ID=" + applications.ApplicationID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MessageBox.Show(" license Renewed successfully with ID=" + _license.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblRenewLicenseID.Text = _license.LicenseID.ToString();
                        lblinternationalApplicationID.Text = applications.ApplicationID.ToString();
                        linkLblShowLicensesInfo.Enabled = true;
                        btnRenew.Enabled = false;
                        //_internationalLicenseApplication = internationalLicenseApplication;
                    }
                        }
                        else
                        {
                            MessageBox.Show("Failed to issue  license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                

            }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
           ShowLicenseForm showLicenseForm = new ShowLicenseForm(_license);
            showLicenseForm.LoadData();
            showLicenseForm.ShowDialog();
        }
    }
}
