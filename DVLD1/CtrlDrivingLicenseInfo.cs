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
    public partial class CtrlDrivingLicenseInfo : UserControl
    {
        LocalDrivingLicenseApplication _localDrivingLicenseApplication;
        Applications _applications;
        Licence _licence;
        People _people;
        public CtrlDrivingLicenseInfo()
        {
            InitializeComponent();
        }

        public CtrlDrivingLicenseInfo(LocalDrivingLicenseApplication localDrivingLicenseApplication, Applications applications, Licence licence,
         People people)
        {
            InitializeComponent();
            _localDrivingLicenseApplication = localDrivingLicenseApplication;
            _applications = applications;
            _licence = licence;
            _people = people;

        }

        public void LoadData(LocalDrivingLicenseApplication localDrivingLicenseApplication, Applications applications, Licence licence,
         People people)
        {
            _localDrivingLicenseApplication = localDrivingLicenseApplication;
            _applications = applications;
            _licence = licence;
            _people = people;

            if (_localDrivingLicenseApplication == null ||
                _licence == null ||
                _people == null)
            {
                return; 
            }
            LicenseClasses licenseClasses = new LicenseClasses();
            licenseClasses = LicenseClasses.Find(_localDrivingLicenseApplication.LicenseClassID);
            DetainLicense detainLicense = new DetainLicense();
            if (DetainLicense.IsLicenseDetained(_licence.LicenseID))
            {
                lblisDetained.Text = "YES";
            }
            else
            {
                lblisDetained.Text = "NO";
            }
            lblClass1.Text = licenseClasses.ClassName.ToString();
            lblName2.Text = _people.FullName();
            lblLicenseID.Text = _licence.LicenseID.ToString();
            lblNationalNo2.Text = _people.NationalNo.ToString();
            lblGender2.Text = (_people.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _licence.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = (_licence.IssueReason == 1) ? "First Time" : (_licence.IssueReason == 2) ? "Renew" :
                (_licence.IssueReason == 3) ? "Replacement For a lost" : (_licence.IssueReason == 4) ? "Replacement For a damaged" : "?????"; lblNotes.Text = string.IsNullOrWhiteSpace(_licence.Notes) ? "No notes" : _licence.Notes;
            lblIsActive.Text = (_licence.IsActive) ? "Yes" : "No";
            lblDateOfBirth2.Text = _people.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriver.Text = _licence.DriverID.ToString();
            lblExpirationDate.Text = _licence.ExpirationDate.ToString("dd/MM/yyyy");
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


        public void LoadDataByLicense( Licence licence, string Title="Driver License Info")
        {
      
            _licence = licence;

            if (_licence == null )
            {
                return;
            }
            LicenseClasses licenseClasses = new LicenseClasses();
            licenseClasses = LicenseClasses.Find(_licence.LicenseClass);
            Driver driver = Driver.GetInfoDriverByPersonID(_licence.DriverID);
            People _people = People.Find(driver.PersonID);
            //if (!_licence.IsActive || _licence.ExpirationDate<DateTime.Now)
            //{
            //    MessageBox.Show("This license is not active or has expired.", "License Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
                lblClass1.Text = licenseClasses.ClassName.ToString();
            lblName2.Text = _people.FullName();
            lblLicenseID.Text = _licence.LicenseID.ToString();
            lblNationalNo2.Text = _people.NationalNo.ToString();
            lblGender2.Text = (_people.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _licence.IssueDate.ToString("dd/MM/yyyy");
            lblIssueReason.Text = (_licence.IssueReason == 1) ? "First Time":(_licence.IssueReason == 2)?"Renew":
                (_licence.IssueReason == 3) ? "Replacement For a lost": (_licence.IssueReason == 4) ? "Replacement For a damaged":"?????";
            lblNotes.Text = string.IsNullOrWhiteSpace(_licence.Notes) ? "No notes" : _licence.Notes;            
                lblIsActive.Text = (_licence.IsActive) ? "Yes" : "No";
            
            lblDateOfBirth2.Text = _people.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriver.Text = _licence.DriverID.ToString();
            lblExpirationDate.Text = _licence.ExpirationDate.ToString("dd/MM/yyyy");
            DetainLicense detainLicense = new DetainLicense();
            if (DetainLicense.IsLicenseDetained(_licence.LicenseID))
            {
                lblisDetained.Text = "YES";
            }
            else
            {
                lblisDetained.Text = "NO";
            }
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


      
        private void CtrlDrivingLicenseInfo_Load(object sender, EventArgs e)
        {
            if (_localDrivingLicenseApplication == null ||
                _applications == null )
            {
                LoadDataByLicense(_licence);

            }
            else
            LoadData(_localDrivingLicenseApplication,_applications,_licence,_people);

        }

        private void gbDrivingLicenseInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
