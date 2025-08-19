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
    public partial class ctrlShearchDrivingLicense : UserControl
    {
        public static int _LicenceID;
        public Licence _Licence;
        public int _LicenseID;
        ctrlShearchDrivingLicense _ctrlShearchDrivingLicense;
        static bool _Renew;

        public ctrlShearchDrivingLicense()
        {
            InitializeComponent();
        }

        public ctrlShearchDrivingLicense(Licence licence)
        {
            InitializeComponent();
            _Licence = licence;
        }
   
        private void ctrlShearchDrivingLicense_Load(object sender, EventArgs e)
        {

        }
        public event Action<int> LicenseSelected;

        public  void LoadDataCtrlShearch(Licence licence)
        {
            ctrlDrivingLicenseInfo1.LoadDataByLicense(licence);

        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (!int.TryParse(textBox1.Text, out int licenseId) || licenseId <= 0)
            {
                MessageBox.Show("Please enter a valid License ID.");
                return;
            }
            Licence licence = Licence.GetLicenseInfoByLicenseID(licenseId);
            if (licence == null)
            {
                MessageBox.Show("License is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LicenceID = licence.LicenseID;
            ctrlDrivingLicenseInfo1.LoadDataByLicense(licence);



            LicenseSelected?.Invoke(_LicenceID);

        }

        public void GbFiltrerEnabled(int ID)
        {
            gbfiltrer.Enabled =false ;
            textBox1.Text = ID.ToString();
        }
        public int GetLicenseID()
        {
            return _LicenceID;

        }
        public bool IsRenew()
        {
            return _Renew;
        }
    }
}
