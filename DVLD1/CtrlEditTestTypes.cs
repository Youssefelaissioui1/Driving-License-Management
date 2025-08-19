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
    public partial class CtrlEditTestTypes : UserControl
    {
        private TestTypes _testTypes;
        public CtrlEditTestTypes()
        {
            InitializeComponent();
        }


        public CtrlEditTestTypes(int id)
        {
            InitializeComponent();
            _testTypes = TestTypes.FindTestTypesByID(id);
            tbFees.Text = _testTypes.TestTypeFees.ToString();
            tbTitles.Text = _testTypes.TestTypeTitle.ToString();
            lblID.Text = _testTypes.TestTypeID.ToString();
            tbDescription.Text = _testTypes.TestTypeDescription.ToString();

        }

        private void CtrlEditTestTypes_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _testTypes.TestTypeFees = double.Parse(tbFees.Text);
            _testTypes.TestTypeTitle = tbTitles.Text;
            _testTypes.TestTypeDescription = tbDescription.Text;


            if (_testTypes.UpdateTestTypes())
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
