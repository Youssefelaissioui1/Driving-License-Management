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
    public partial class ScheduleTestForm : Form
    {
        VisionTestAppointmentsForm VisionTestAppointmentsForm;
        static LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        static Applications _applications;
        static People _person;
        public static TestAppointment _testAppointment;
        static TestTypes _testTypes;
        private ApplicationTypes _applicationTypes;
        public enum enMode { AddNew = 0, Update = 1, Retake = 2 };
        public static enMode _Mode;
        public static TestMode _Modetest;
        public static int  _trialtotal ;
        public delegate void DataBackEventHandler(object sender);
        //declare an event and using the delegate 
        public event DataBackEventHandler DataBack;
        public ScheduleTestForm()
        {
            InitializeComponent();
        }
        public ScheduleTestForm(LocalDrivingLicenseApplication lcApp,Applications app,People p1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication= lcApp;
            _applications= app;
            _person= p1;
        }

        public ScheduleTestForm(LocalDrivingLicenseApplication lcApp, Applications app, People p1,  enMode mode)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = lcApp;
            _applications = app;
            _person = p1;
            _Mode = mode;

        }

        public ScheduleTestForm(LocalDrivingLicenseApplication lcApp, Applications app, People p1,TestAppointment testAppointment,enMode mode,TestMode testMode,int totaltrial)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = lcApp;
            _applications = app;
            _person = p1;
            _Mode = mode;
            _testAppointment = testAppointment;
            _Modetest = testMode;
            _trialtotal = totaltrial;
            
        }

        private void ScheduleTestForm_Load(object sender, EventArgs e)
        {
          
            if (_testAppointment != null)
            {
                lbldtime.MinDate = DateTime.Today;
                lblDLAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblclass.Text = _LocalDrivingLicenseApplication.ClassName.ToString();
                if (_Modetest == TestMode.Vision)
                {
                    _testTypes = TestTypes.FindTestTypesByID(1);

                }
                else if (_Modetest == TestMode.Writing)
                {
                    _testTypes = TestTypes.FindTestTypesByID(2);


                }
                else if (_Modetest == TestMode.Street)
                {
                    _testTypes = TestTypes.FindTestTypesByID(3);
                }
                lblFees.Text = _testTypes.TestTypeFees.ToString();
                lbltrial.Text =( VisionTestAppointmentsForm.TotalTrial).ToString();
                lblName.Text = $"{_person.FirstName} {_person.ThirdName} {_person.SecondName} {_person.LastName}";
                lbldtime.Text = DateTime.Now.ToString();



                lblTotalFess.Text = _testTypes.TestTypeFees.ToString();
                lblRAPPFEES.Text = "0";
                //if (_trialtotal == 0)
                //    _Mode = enMode.AddNew;
                //else if (_trialtotal > 0)
                //    _Mode = enMode.Retake;
                if (_testAppointment.isLocked && _Mode.Equals(enMode.Update))
                {

                    lbldtime.Enabled = false;
                    btnSave.Enabled = false;
                    lbltitle2.Visible = true;
                    lbltitleRetaketest.Visible = true;
                    lbltitle.Visible = false;
                    lbldtime.Text = _testAppointment.appointmentDate.ToString();
                  

                }

                if ( _Mode==enMode.Retake)
                {
                    _applicationTypes = ApplicationTypes.FindApplicationTypeByID(7);
                    lbltitleRetaketest.Visible = true;
                    lbltitle2.Visible = false;
                    lblTotalFess.Text=( _applicationTypes.ApplicationFees+_testTypes.TestTypeFees).ToString();
                    //_testAppointment=TestAppointment.GetInfoTestID2(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                    //lblRtestAppID.Text=_testAppointment.TestAppointmentID.ToString();
                    //if(_Mode.Equals(enMode.Retake))
                    //{
                        gbRetakeTestInfo.Enabled = true;
                        lblRAPPFEES.Text = _applicationTypes.ApplicationFees.ToString();

                    //}

                }
            }
            if (_Modetest == TestMode.Vision)
            {
                pictureBox1.Visible = true;
                groupBox1.Text = "Vision Test";

            }
            else if (_Modetest == TestMode.Writing )
            {
                pbWriting.Visible = true;
                groupBox1.Text = "Writing Test";

            }
            else if (_Modetest == TestMode.Street)
            {
                pbStreet.Visible = true;
                groupBox1.Text = "Street Test";

            }
            if (_Mode.Equals(enMode.Update) && !_testAppointment.isLocked)
            {
                   _applicationTypes = ApplicationTypes.FindApplicationTypeByID(7);
                gbRetakeTestInfo.Enabled = true;
                lblRtestAppID.Text = _testAppointment.TestAppointmentID.ToString();
                    lbltitleRetaketest.Visible = true;
                    lblRAPPFEES.Text = _applicationTypes.ApplicationFees.ToString();
                
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
      

            this.FindForm()?.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
            if (_Mode == enMode.AddNew || _Mode == enMode.Retake)
            {
                if (TestAppointment.isExist(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID))
                {
                    MessageBox.Show("Person already has an active appointment for this test. You cannot add a new appointment.",
                                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                _testAppointment = new TestAppointment
                {
                    testTypeID = _testTypes.TestTypeID,
                    localDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID,
                    appointmentDate = lbldtime.Value,
                    paidFees = Convert.ToDecimal(_testTypes.TestTypeFees),
                    createdByUserID = GlobalUser.CurrentUser.UserID,
                    isLocked = false
                };

                if (_testAppointment.AddTestAppointmentAndGetID())
                {
                    MessageBox.Show("Test Appointment Add successfully", "Add Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this);
                    //VisionTestAppointmentsForm visionTestAppointmentsForm = new VisionTestAppointmentsForm();
                    //visionTestAppointmentsForm._RefreshUsersListForTest(_testAppointment.TestAppointmentID);
                }
                else
                {
                    MessageBox.Show("Test Appointment is not Add", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            if (_Mode == enMode.Update)
            {
                _testAppointment = TestAppointment.GetInfoTestID2(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                if (_testAppointment == null)
                {
                    MessageBox.Show("Test Appointment not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _testAppointment.appointmentDate = lbldtime.Value;

                if (_testAppointment.UpdateTestAppointments(_testAppointment.TestAppointmentID,_testAppointment.isLocked, _testAppointment.appointmentDate))
                {
                    _testAppointment = TestAppointment.GetInfoTestID2(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                    MessageBox.Show("Test Appointment updated successfully", "Update Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DataBack?.Invoke(this);
                    //VisionTestAppointmentsForm visionTestAppointmentsForm = new VisionTestAppointmentsForm();
                    //visionTestAppointmentsForm._RefreshUsersListForTest(_testAppointment.TestAppointmentID);
                }
                else
                {
                    MessageBox.Show("Test Appointment is not updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }


        private void lblRAPPFEES_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalFess_Click(object sender, EventArgs e)
        {

        }

        private void pbWriting_Click(object sender, EventArgs e)
        {

        }

        private void lbltitle2_Click(object sender, EventArgs e)
        {

        }
    }
}
