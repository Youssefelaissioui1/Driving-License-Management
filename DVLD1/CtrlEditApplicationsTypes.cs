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
    public partial class CtrlEditApplicationsTypes : UserControl
    {
        private ApplicationTypes _applicationTypes;

        public CtrlEditApplicationsTypes()
        {
            InitializeComponent();

        }

        public CtrlEditApplicationsTypes(int ApplicationTypesID)
        {
            InitializeComponent();

            _applicationTypes = ApplicationTypes.FindApplicationTypeByID(ApplicationTypesID);
            tbFees.Text=_applicationTypes.ApplicationFees.ToString();
            tbTitles.Text=_applicationTypes.ApplicationTypeTitle.ToString();
            lblID.Text=_applicationTypes.ApplicationTypeID.ToString();
        }

        private void CtrlEditApplicationsTypes_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _applicationTypes.ApplicationFees=double.Parse(tbFees.Text);
            _applicationTypes.ApplicationTypeTitle=tbTitles.Text ;

            if (_applicationTypes.UpdateApplicationTypes())
                MessageBox.Show("Update completed successfully ");
            else
                MessageBox.Show("Update failed.");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
