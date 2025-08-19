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

namespace DVLD1
{
    public partial class ShowLicenseForm : Form
    {
        LocalDrivingLicenseApplication _localDrivingLicenseApplication;
        Applications _applications;
        Licence _licence;
        People _people;
        InternationalLicenseApplication _internationalLicenseApplication;
        public int _type;
        public ShowLicenseForm()
        {
            InitializeComponent();
        }
        public ShowLicenseForm(LocalDrivingLicenseApplication localDrivingLicenseApplication,Applications applications,Licence licence,
            People people)
        {
            InitializeComponent();
            _localDrivingLicenseApplication = localDrivingLicenseApplication;
            _applications = applications;
            _licence = licence;
            _people = people;

        }
        public ShowLicenseForm(InternationalLicenseApplication InternationalLicenseApplication, int Type=0)
        {
            InitializeComponent();

            _internationalLicenseApplication = InternationalLicenseApplication;
            _type = Type;

        }
        public ShowLicenseForm(Licence licence)
        {
            InitializeComponent();
         
            _licence = licence;

        }


        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void gbDrivingLicenseInfo_Enter(object sender, EventArgs e)
        {

        }

        private void pbImageInfoDetails_Click(object sender, EventArgs e)
        {

        }

        private void l_Click(object sender, EventArgs e)
        {

        }

        public void LoadData()
        {
            if (_localDrivingLicenseApplication != null && _applications != null && _people!=null)
            {
                label1.Text = "Driving License Info";
                ctrlDrivingLicenseInfo1.LoadData(_localDrivingLicenseApplication, _applications, _licence, _people);

            }
        
            else
            {
                if(_licence!=null)
                ctrlDrivingLicenseInfo1.LoadDataByLicense(_licence);
            }
        }
        private void ShowLicenseForm_Load(object sender, EventArgs e)
        {
            //CtrlDrivingLicenseInfo ctrlDrivingLicenseInfo = new CtrlDrivingLicenseInfo(_localDrivingLicenseApplication, _applications, _licence, _people);
            //ctrlDrivingLicenseInfo.Dock = DockStyle.Fill;
            //ctrlDrivingLicenseInfo.BringToFront();
            //this.Controls.Add(ctrlDrivingLicenseInfo);
            //ctrlDrivingLicenseInfo.Show();
            LoadData();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }
    }
}
