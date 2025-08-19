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
    public partial class AddNewInternationalLicenseForm : Form
    {
        Licence _license;
        Applications _applications;
        public int LicenseID;
        public  Driver _driver;
        public People _people;
        public InternationalLicenseApplication _internationalLicenseApplication;
        public AddNewInternationalLicenseForm()
        {
            InitializeComponent();
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _license = Licence.GetLicenseInfoByLicenseID(LicenseID);

            if (_license == null)
            {
                MessageBox.Show("License not found for the selected ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Driver driver = Driver.GetInfoDriverByPersonID(_license.DriverID);
            People people=People.Find(driver.PersonID);
            _driver = driver;
            _people = people;

            ApplicationTypes applicationTypes =ApplicationTypes.FindApplicationTypeByID(6);
            if (InternationalLicenseApplication.IsInternationalLicenseApplicationExist(_license.DriverID))
            {
                MessageBox.Show("An international license application already exists for this local license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                linklblShowLicensesHistory.Enabled = true;
                linkLblShowLicensesInfo.Enabled = true;
                btnIssue.Enabled = false;
                return;
            }
            else
            {
Applications applications =Applications.GetApplicationsbyID(_license.ApplicationID);
                _applications = new Applications
                {
                    
                    ApplicantPersonID = people.PersonID,
                    ApplicationDate =DateTime.Now,
                    ApplicationTypeID = applications.ApplicationTypeID,
                    ApplicationStatus = 3,
                    LastStatusDate = DateTime.Now,
                    PaidFees = Convert.ToInt32(applicationTypes.ApplicationFees.ToString()),
                    CreatedByUserID = GlobalUser.CurrentUser.UserID,
                };
            
                DialogResult result = MessageBox.Show("Are you sure you want to issue an international license?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                    if (result == DialogResult.Yes)
                    {
                        if (_applications._AddNewApplications())
                        {
                        InternationalLicenseApplication internationalLicenseApplication = new InternationalLicenseApplication
                        {
                            ApplicationID = _applications.ApplicationID,
                            DriverID = _license.DriverID,
                            IssuedUsingLocalLicenseID = _license.LicenseID,
                            IssueDate = DateTime.Now,
                            ExpirationDate = DateTime.Now.AddYears(1),
                            IsActive = true,
                            CreatedByUserID = GlobalUser.CurrentUser.UserID

                        };
                        if (internationalLicenseApplication.AddNew())
                            {
                            MessageBox.Show("Applications Added with successfully with ID=" + _applications.ApplicationID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            MessageBox.Show("International license issued successfully with ID=" + internationalLicenseApplication.InternationalLicenseApplicationID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                lblInternationalLicenseID.Text = internationalLicenseApplication.InternationalLicenseApplicationID.ToString();
                                lblinternationalApplicationID.Text = _applications.ApplicationID.ToString();
                                linkLblShowLicensesInfo.Enabled = true;
                                btnIssue.Enabled = false;
                            _internationalLicenseApplication = internationalLicenseApplication;

                        }
                            else
                            {
                                MessageBox.Show("Failed to issue international license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                
            }
        }

   
        private void AddNewInternationalLicenseForm_Load(object sender, EventArgs e)
        {
           
            lblCreatedBy.Text = GlobalUser.CurrentUser.UserName;
            lblFees.Text = ApplicationTypes.FindApplicationTypeByID(6).ApplicationFees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblIssueDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblExpirationDate2.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
            ctrlShearchDrivingLicense1.LicenseSelected += id =>
            {
                linklblShowLicensesHistory.Enabled = true;
                lblLocalLicenseID.Text = id.ToString();
                LicenseID = id;
            };


        }

        private void linkLblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_internationalLicenseApplication == null)
{
                MessageBox.Show(" International License Application is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            ShowInternationalLicenseInfoForm showInternationalLicenseInfoForm = new ShowInternationalLicenseInfoForm(_internationalLicenseApplication, _license, _people,_driver);
            showInternationalLicenseInfoForm.ShowDialog();
        }

        private void linklblShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(_license==null)
            {
                _license = Licence.GetLicenseInfoByLicenseID(LicenseID);
            }
            if (_people==null)
            {
                Driver driver = Driver.GetInfoDriverByPersonID(_license.DriverID);
                People people = People.Find(driver.PersonID);
                _driver = driver;
                _people = people;
            }
            
            LicenseHistoryForm LicenseHistoryForm = new LicenseHistoryForm( _people);

            LicenseHistoryForm.ShowDialog();
        }
    }
}
