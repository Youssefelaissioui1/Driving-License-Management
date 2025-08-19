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
    public partial class VisionTestAppointmentsForm : Form
    {
        LocalDrivingLicenseApplicationForm_Table_View localDrivingLicenseApplicationForm_Table_View;

        public static LocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        public static Applications _application;
        public static User _user;
        public static People _people;
        public static TestAppointment _TestAppointment;
        public static Tests _tests;
        public delegate void DataBackEventHandler(object sender);
        //declare an event and using the delegate 
        public event DataBackEventHandler DataBack;
        public  enum enMode { AddNew = 0, Update = 1,Retake = 2 };
        public static enMode _Mode;
        public static TestMode _Modetest;
        public event EventHandler DataChanged;
        public static int TotalTrial = 0;
        public VisionTestAppointmentsForm()
        {
            InitializeComponent();
        }
        public VisionTestAppointmentsForm(LocalDrivingLicenseApplication LocalDrivingLicenseApplication,Applications application)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            _application = application;
        }

        public VisionTestAppointmentsForm(LocalDrivingLicenseApplication LocalDrivingLicenseApplication, Applications application, TestMode modetest)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            _application = application;
            _Modetest = modetest;
        }



        protected void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        private void VisionTestAppointmentsForm_Load(object sender, EventArgs e)
        {
                _TestAppointment = TestAppointment.GetInfoTestID2(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);

            dataGridView1.CellMouseClick += dataGridView1_MouseClick;

            _people = People.Find(_application.ApplicantPersonID);

            if (_application == null || _LocalDrivingLicenseApplication == null || _people==null)
            {
                MessageBox.Show("application  null", "error");
                return;
            }
            User user = User.Find(_application.CreatedByUserID);
         


            ctrlTestAppointmentInfo1.LoadData(_LocalDrivingLicenseApplication,_application,_Modetest);

            _RefreshUsersListForTest(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
            }

     
        public void _RefreshUsersListForTest(int LocalDrivingLicenseApplicationID)
        {
            if (_Modetest == TestMode.Vision)
            {
                DataTable all = TestAppointment.GetAllTestOfVision(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                //lbltitle.Text = "Vision Test Appointments";
                //pictureBox1.Visible = true;
                if (all != null && all.Rows.Count > 0)
                {
                    DataTable filtered = all.DefaultView.ToTable(false,
                        "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

                    dataGridView1.DataSource = filtered;
                    TotalTrial = dataGridView1.Rows.Count;

                }
            }
            else if (_Modetest == TestMode.Writing)

            {
                DataTable all = TestAppointment.GetAllTestOfWriting(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                //lbltitle.Text = "Written Test Appointments";
                //pbWriting.Visible = true;

                if (all != null && all.Rows.Count > 0)
                {


                    DataTable filtered = all.DefaultView.ToTable(false,
                        "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

                    dataGridView1.DataSource = filtered;
                    TotalTrial = dataGridView1.Rows.Count;

                }
            }
            else if (_Modetest == TestMode.Street)
            {
                DataTable all = TestAppointment.GetAllTestOfStreet(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                //lbltitle.Text = "Street Test Appointments";
                //pbStreet.Visible = true;

                if (all != null && all.Rows.Count > 0)
                {
                    DataTable filtered = all.DefaultView.ToTable(false,
                        "TestAppointmentID", "AppointmentDate", "PaidFees", "IsLocked");

                    dataGridView1.DataSource = filtered;
                    TotalTrial = dataGridView1.Rows.Count;

                }

            }
            else
            {
                dataGridView1.DataSource = null;
            }

         
            lblRecord.Text = $"# Records: {dataGridView1.Rows.Count}";
        }
        public Form ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 700,
                Height = 350
            };
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.Show(); // Show au lieu de ShowDialog pour permettre l'événement FormClosed
            return form;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this);
            this.FindForm()?.Close();
        }

        private void linkPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_people != null)
            {
                CtrlShowDetailsPerson ctrlShowDetailsPerson = new CtrlShowDetailsPerson(_people);
                Form frm = ShowFormWithControl(ctrlShowDetailsPerson, "View Details Person");
            }
            else
                MessageBox.Show("person is not found");
        }

        public void addtestvisionorupdate(enMode mode, TestAppointment testAppointment, TestMode testMode )
        {

            _TestAppointment = testAppointment;
                if (_TestAppointment!=null){
                    ScheduleTestForm frm = new ScheduleTestForm(_LocalDrivingLicenseApplication, _application, _people, _TestAppointment,(ScheduleTestForm.enMode)mode, testMode,TotalTrial);
                // 🔥 Abonnement à l'événement DataBack
                frm.DataBack += (sender) =>
                {
                    _RefreshUsersListForTest(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                };
                frm.ShowDialog();
            }
            else
{
                    ScheduleTestForm frm = new ScheduleTestForm(_LocalDrivingLicenseApplication, _application, _people, (ScheduleTestForm.enMode)mode);
                // 🔥 Abonnement à l'événement DataBack
                frm.DataBack += (sender) =>
                {
                    _RefreshUsersListForTest(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                };
                frm.Show();
            }              
             
            }
        //}
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Application is null", "Error");
                return;
            }
            //_TestAppointment = TestAppointment.GetInfoTestID2(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);

            if (_TestAppointment == null)
            {
                if(TotalTrial==0)
                _Mode = enMode.AddNew;
                _TestAppointment = new TestAppointment();
                addtestvisionorupdate(_Mode, _TestAppointment, _Modetest);
                return;
            }

            if (_tests == null)
            {
                _tests = Tests.GetTestbyID(_TestAppointment.TestAppointmentID);
            }
            if (TestAppointment.isExist(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID))
            {
                MessageBox.Show("Person already has an active appointment for this test. You cannot add a new appointment.",
                                "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool passed = false;
            switch (_Modetest)
            {
                case TestMode.Vision:
                    passed = _tests != null && _tests.IsTestExistIDAndIsPassofVision(_TestAppointment.TestAppointmentID);
                    TotalTrial = TestAppointment.GetAllTestOfVision_Count(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                    break;
                case TestMode.Writing:
                    passed = _tests != null && _tests.IsTestExistIDAndIsPassofWriting(_TestAppointment.TestAppointmentID);
                    TotalTrial = TestAppointment.GetAllTestOfWriting_Count(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);

                    break;
                case TestMode.Street:
                    passed = _tests != null && _tests.IsTestExistIDAndIsPassofStreet(_TestAppointment.TestAppointmentID);
                    TotalTrial = TestAppointment.GetAllTestOfStreet_Count(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                    break;
            }

            if (passed)
            {
                MessageBox.Show("Person already passed this test before, you can only retake failed tests",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if(!passed)
            {
                if(TotalTrial!=0)
                _Mode = enMode.Retake;
                else
                    _Mode = enMode.AddNew;
                //_TestAppointment = new TestAppointment();
                addtestvisionorupdate(_Mode, _TestAppointment, _Modetest);
                return;
            }

         

            // Cas Retake
            if (_tests != null && _tests.TestResult == false && _TestAppointment.isLocked)
            {
                if (TotalTrial != 0)
                    _Mode = enMode.Retake;
                else
                    _Mode = enMode.AddNew;
                addtestvisionorupdate(_Mode, _TestAppointment, _Modetest);
            }
        }


        private void dataGridView1_MouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;

                var cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var cellLocation = dataGridView1.PointToScreen(cellRect.Location);

                contextMenuStrip1.Show(cellLocation.X + cellRect.Width, cellLocation.Y);
            }
        }

        private void editTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            int _TestAppointmentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TestAppointmentID"].Value);
            _TestAppointment=TestAppointment.GetInfoTestIDtest(_TestAppointmentID);
            _Mode = enMode.Update;
            addtestvisionorupdate(_Mode,_TestAppointment,_Modetest);
            OnDataChanged();


        }

        public void addTest(TestAppointment testAppointment)
        {
            _TestAppointment = testAppointment;

            if (_TestAppointment.isLocked)
            {
                MessageBox.Show("test is taked", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TakeTestForm takeTestForm = new TakeTestForm(_TestAppointment, _LocalDrivingLicenseApplication,_Modetest);
            // Abonnement à DataBack pour rafraîchir la liste
            takeTestForm.DataBack += (sender) =>
            {
                _RefreshUsersListForTest(_LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
            };
            takeTestForm.ShowDialog();
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int _TestAppointmentID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TestAppointmentID"].Value);
            _TestAppointment = TestAppointment.GetInfoTestIDtest(_TestAppointmentID);
            addTest(_TestAppointment);
            OnDataChanged();

        }

        private void ctrlTestAppointmentInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
