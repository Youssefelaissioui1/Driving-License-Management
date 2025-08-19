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
using static DVLD1.LocalDrivingLicenseApplicationForm_Table_View;

namespace DVLD1
{
    public partial class TakeTestForm : Form
    {
        static  TestAppointment _TestAppointment;
        static LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        static Tests _tests;
    
        public delegate void DataBackEventHandler(object sender);
        //declare an event and using the delegate 
        public event DataBackEventHandler DataBack;
        public event EventHandler DataChanged;
        VisionTestAppointmentsForm VisionTestAppointmentsForm;
        public static TestMode _Modetest;
        public TakeTestForm()
        {
            InitializeComponent();
        }

        public TakeTestForm(TestAppointment testAppointment,LocalDrivingLicenseApplication localDrivingLicenseApplication,TestMode testMode)
        {
            InitializeComponent();
            _TestAppointment = testAppointment;
            _LocalDrivingLicenseApplication = localDrivingLicenseApplication;
            _Modetest=testMode;
        }

        private void gbResult_Enter(object sender, EventArgs e)
        {
          

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void TakeTestForm_Load(object sender, EventArgs e)
        {
         
       
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
        }

        private void TakeTestForm_Load_1(object sender, EventArgs e)
        {
            if (TestMode.Vision == _Modetest)
            {
                pbxVison.Visible = true;
                groupBox2.Text = "Vision Test";
            }
            else if (TestMode.Writing == _Modetest)
            {
                pbxWriting.Visible = true;
                groupBox2.Text = "Writing Test";
            }
            else if (TestMode.Street == _Modetest)
            {
                pbxStreet.Visible = true;
                groupBox2.Text = "Street Test";
            }
            lblAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            labelClass.Text = _LocalDrivingLicenseApplication.ClassName.ToString();
            labelName.Text = _LocalDrivingLicenseApplication.FullName.ToString();
            labelTrial.Text = (VisionTestAppointmentsForm.TotalTrial - 1).ToString();
            labelDate.Text = _TestAppointment.appointmentDate.ToString();
            labelFees.Text = _TestAppointment.paidFees.ToString();
            rbPass.Checked = true;
            rbFail.Checked = false;
            labelTestID.Text = "Not Taken Yet";

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //verifier
            if (_TestAppointment.isLocked)
            {
                MessageBox.Show("test is taked", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //
            _tests = new Tests();
            DialogResult result = MessageBox.Show("are you sure you want to save? after that you cannot change the Pass/Fail results after you save?", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
            {
                return;
            }
            _tests.TestAppointmentID = _TestAppointment.TestAppointmentID;
            if (rbPass.Checked)
            {
                _tests.TestResult = true;

            }
            else if (rbFail.Checked)
            {
                _tests.TestResult = false;
            }
            if (_tests.Notes != "")
                _tests.Notes = textBox2.Text;

            _tests.CreatedByUserID = GlobalUser.CurrentUser.UserID;

            if (_tests._AddNewTests())
            {
                MessageBox.Show("Data saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                _TestAppointment.isLocked = true;
                _TestAppointment.UpdateTestAppointments(_TestAppointment.TestAppointmentID, _TestAppointment.isLocked, _TestAppointment.appointmentDate);
                DataBack?.Invoke(this);
                DataBack?.Invoke(this);
                return;
            }
            else
            {
                MessageBox.Show("Data not saved successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            //DataBack?.Invoke(this);

        }
    }
}
