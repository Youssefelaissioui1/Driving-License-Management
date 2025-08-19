using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static DVLD1.LocalDrivingLicenseApplicationForm_Table_View;

namespace DVLD1
{
    public partial class IssueDrivingLicenseForm : Form
    {
     License _license;
        LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        Applications _application;
        TestAppointment _TestAppointment;
        public static TestMode _Modetest;
        public delegate void DataBackEventHandler(object sender);
        //declare an event and using the delegate 
        public event DataBackEventHandler DataBack;
        public event EventHandler DataChanged;

        public IssueDrivingLicenseForm()
        {
            InitializeComponent();
        }
        public IssueDrivingLicenseForm(LocalDrivingLicenseApplication localDrivingLicenseApplication,Applications applications,TestMode testMode)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = localDrivingLicenseApplication;
            _application = applications;
            _Modetest = testMode;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            LicenseClasses licenseClasses = new LicenseClasses();
            licenseClasses = LicenseClasses.Find(_LocalDrivingLicenseApplication.LicenseClassID);

            Driver driver = new Driver();
            driver.PersonID = _application.ApplicantPersonID;
            driver.CreatedByUserID = GlobalUser.CurrentUser.UserID;
            driver.CreatedDate = DateTime.Now;
            //Driver lastDriver = driver.GetInfoLastDriver();

            Licence license=new Licence();
            license.ApplicationID=_application.ApplicationID;
            license.LicenseClass=_LocalDrivingLicenseApplication.LicenseClassID;
            license.IssueDate=DateTime.Now;
            license.ExpirationDate = DateTime.Now.AddYears(licenseClasses.DefaultValidityLength);
            license.Notes = string.IsNullOrWhiteSpace(tbxNotes.Text) ? null : tbxNotes.Text;
            license.PaidFees=_application.PaidFees;
            license.IsActive = true;
            license.IssueReason = 1;
            license.CreatedByUserID=GlobalUser.CurrentUser.UserID;
            if(!Driver.isDriverExist(_application.ApplicantPersonID))
                {   
                    if (driver.AddNewDriver())
                    {
                        license.DriverID = driver.DriverID;
                        MessageBox.Show($"Driver Successfully with Driver ID=" + driver.DriverID);

                    }
                    else
                    {
                        MessageBox.Show("Driver Not Added");
                        return;
                    }
                }
                else{
                driver = Driver.GetInfoDriver(_application.ApplicantPersonID);
                license.DriverID = driver.DriverID;
                    MessageBox.Show($"Driver Successfully with Driver ID=" + driver.DriverID);
                }
            

            if (license.AddNewLicence())
            {
                MessageBox.Show($"License Issue Successfully with License ID=" + license.LicenseID);

                if (_application.Update())
                 { 
                        MessageBox.Show($"License Issue Successfully with License ID=" + license.LicenseID);
                        DataBack?.Invoke(this);
                    
                }


                
            }
            else
            {
                MessageBox.Show($"License Issue Not Added ");

            }
            DataBack?.Invoke(this);

        }

        private void IssueDrivingLicenseForm_Load(object sender, EventArgs e)
        {


            if (_application == null || _LocalDrivingLicenseApplication == null )
            {
                MessageBox.Show("application or  Local Driving License Application is null", "error");
                return;
            }

            
            ctrlTestAppointmentInfo1.LoadData(_LocalDrivingLicenseApplication, _application, _Modetest);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }
    }
    }

