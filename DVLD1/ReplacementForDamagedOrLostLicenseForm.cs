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

namespace DVLD1
{
    public partial class ReplacementForDamagedOrLostLicenseForm : Form
    {
        public int _LicenseID;
        Licence _license;
        public Driver _driver;
        public People _people;
        public int OldLicenseID;
        ApplicationTypes applicationTypes = new ApplicationTypes();

        public ReplacementForDamagedOrLostLicenseForm()
        {
            InitializeComponent();
        }

        private void ReplacementForDamagedOrLostLicenseForm_Load(object sender, EventArgs e)
        {
            lblCreatedBy.Text = GlobalUser.CurrentUser.UserName;
            lblFees.Text = ApplicationTypes.FindApplicationTypeByID(3).ApplicationFees.ToString();
            lblApplicationDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            ctrlShearchDrivingLicense1.LicenseSelected += id =>
            {
                _LicenseID = id;
                OldLicenseID = id;
                Licence licence = Licence.GetLicenseInfoByLicenseID(_LicenseID);
                if (licence == null)
                {
                    MessageBox.Show("License not found. Please select a valid license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssue.Enabled = false;
                    linklblShowLicensesHistory.Enabled = false;
                    return;
                }
                _license = licence;
                lblOldLicenseID.Text = licence.LicenseID.ToString();
                if (!licence.IsActive)
                {
                    MessageBox.Show("Selected License is not  Active ,choose an active License" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnIssue.Enabled = false;
                    linklblShowLicensesHistory.Enabled = true;
                    return;
                }
                btnIssue.Enabled = true;
                linklblShowLicensesHistory.Enabled = true;

                //LoadData(licence);
            };
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            ApplicationTypes applicationTypes = new ApplicationTypes();
            if (rbDamaged.Checked)
                applicationTypes = ApplicationTypes.FindApplicationTypeByID(4);
            else if (rbLost.Checked)
                applicationTypes = ApplicationTypes.FindApplicationTypeByID(3);
            else
            {
                MessageBox.Show("Please select a reason for replacement (Damaged or Lost).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            DialogResult result = MessageBox.Show("Are you sure you want to Replacement  license?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (applications._AddNewApplications())
                    if(rbDamaged.Checked)
                        _license.IssueReason = 4; // Replacement for Damaged
                    else if (rbLost.Checked)
                        _license.IssueReason = 3; // Replacement for Lost
                    else
                    {
                        MessageBox.Show("Please select a reason for replacement (Damaged or Lost).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                _license = new Licence
                    {
                        ApplicationID = applications.ApplicationID,
                        DriverID = _license.DriverID,
                        LicenseClass = _license.LicenseClass,
                        IssueDate = DateTime.Now,
                        ExpirationDate = DateTime.Now.AddYears(LicenseClasses.Find(_license.LicenseClass).DefaultValidityLength),
                        PaidFees = Convert.ToInt32(lblFees.Text),
                        IsActive = true,
                        IssueReason = 3,
                        CreatedByUserID = GlobalUser.CurrentUser.UserID

                    };
                if (rbDamaged.Checked)
                    _license.IssueReason = 4; 
                else if (rbLost.Checked)
                    _license.IssueReason = 3;
                if (_license.AddNewLicence())
                {
                    if (Licence.UpdateIsActiveLIcenseToFalse(OldLicenseID))
                    {
                        MessageBox.Show("Applications Added with successfully with ID=" + applications.ApplicationID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MessageBox.Show(" license Replaced successfully with ID=" + _license.LicenseID, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblReplacedLicense.Text = _license.LicenseID.ToString();
                        lblLRApplicationID.Text = applications.ApplicationID.ToString();
                        linkLblShowLicensesInfo.Enabled = true;
                        btnIssue.Enabled = false;
                        gbxReplacementFor.Enabled = false;
                        //_internationalLicenseApplication = internationalLicenseApplication;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to issue  license.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

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

        private void gbxReplacementFor_Enter(object sender, EventArgs e)
        {
         
          
        }

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamaged.Checked)
                applicationTypes = ApplicationTypes.FindApplicationTypeByID(4);
            lblFees.Text = applicationTypes.ApplicationFees.ToString();
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
               if (rbLost.Checked)
                applicationTypes = ApplicationTypes.FindApplicationTypeByID(3);
            lblFees.Text = applicationTypes.ApplicationFees.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
