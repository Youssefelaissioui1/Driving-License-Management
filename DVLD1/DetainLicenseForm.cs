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
    public partial class DetainLicenseForm : Form
    {
        public int LicenseID ;
        public Driver _driver;
        public People _people;
        public Licence _license;
        public DetainLicenseForm()
        {
            InitializeComponent();
        }

        private void DetainLicenseForm_Load(object sender, EventArgs e)
        {
            lblDetainDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lblCreatedBy.Text = GlobalUser.CurrentUser.UserName;

            ctrlShearchDrivingLicense1.LicenseSelected += id =>
            {
                lblDetainID.Text = "[????]";
                tbxFineFees.Text = null;
                linklblShowLicensesHistory.Enabled = true;
                lblLicenseID.Text = id.ToString();
                LicenseID = id;
                _license = Licence.GetLicenseInfoByLicenseID(LicenseID);

                if (DetainLicense.IsLicenseDetained(LicenseID))
                {
                    MessageBox.Show("This license is already detained.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnDetain.Enabled = false;
                    return;
                }
                btnDetain.Enabled = true;
                lblLicenseID.Text = LicenseID.ToString();

            };
          
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

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tbxFineFees.Text) || !float.TryParse(tbxFineFees.Text, out float fineFees) || fineFees < 0)
            {
                MessageBox.Show("Please enter a valid fine fees amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DetainLicense detainLicense = new DetainLicense();
             detainLicense = new DetainLicense
            {
                LicenseID = _license.LicenseID,
                DetainDate = DateTime.Now,
                FineFess = Convert.ToSingle(tbxFineFees.Text),
                CreatedByUserID = GlobalUser.CurrentUser.UserID,
                IsReleased = false,
                ReleaseDate = DateTime.Now
            };
            DialogResult result = MessageBox.Show("Are you sure you want to detain this license?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (detainLicense.AddNewDetainedLicense())
                {
                    MessageBox.Show("License has been successfully detained.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDetain.Enabled = false;
                    linkLblShowLicensesInfo.Enabled = true;
                    lblDetainID.Text = detainLicense.DetainID.ToString();
                }
                else
                {
                    MessageBox.Show("Failed to detain the license. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

           
        }

        private void linkLblShowLicensesInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseForm showLicenseForm = new ShowLicenseForm(_license);
            showLicenseForm.LoadData();
            showLicenseForm.ShowDialog();
        }
    }
}
