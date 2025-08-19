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
    public partial class ShowInternationalLicenseInfoForm : Form
    {
        public InternationalLicenseApplication _internationalLicenseApplication;
        public Licence _licence;
        public People _people;
        public Driver _driver;
        public ShowInternationalLicenseInfoForm()
        {
            InitializeComponent();
        }

        public ShowInternationalLicenseInfoForm(InternationalLicenseApplication internationalLicenseApplication,Licence licence,
                            People people,Driver driver)
        {
            InitializeComponent();
            _internationalLicenseApplication = internationalLicenseApplication;
            _licence = licence;
            _people = people;
            _driver = driver;
        }
        private void ShowInternationalLicenseInfoForm_Load(object sender, EventArgs e)
        {
            LicenseClasses licenseClasses = new LicenseClasses();
            licenseClasses = LicenseClasses.Find(_licence.LicenseClass);
            lblName2.Text = licenseClasses.ClassName.ToString();
            lblintLicenseID.Text = _internationalLicenseApplication.InternationalLicenseApplicationID.ToString();
            lblLicenseID.Text = _licence.LicenseID.ToString();
            lblNationalNo2.Text=_people.NationalNo.ToString();
            lblGender2.Text = (_people.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _internationalLicenseApplication.IssueDate.ToString("dd/MM/yyyy");
            lblApplicationid.Text = _internationalLicenseApplication.ApplicationID.ToString();
            lblIsActive.Text = (_internationalLicenseApplication.IsActive) ? "Yes" : "No";
            lblDateOfBirth2.Text = _people.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriver.Text = _licence.DriverID.ToString();
            lblExpirationDate.Text = _internationalLicenseApplication.ExpirationDate.ToString("dd/MM/yyyy");
            if (!string.IsNullOrEmpty(_people.ImagePath) && System.IO.File.Exists(_people.ImagePath))
            {
                pbImageInfoDetails.Image = System.Drawing.Image.FromFile(_people.ImagePath);
            }
            else
            {
                if (_people.Gender == 0)
                    pbImageInfoDetails.Image = Properties.Resources.person_man__2_;
                else
                    pbImageInfoDetails.Image = Properties.Resources.person_woman__4_;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
