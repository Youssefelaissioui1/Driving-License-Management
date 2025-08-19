using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD1
{
    public partial class NewLocalLicenseForm : Form
    {
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        public int _PersonID;
        private People _Person;



        public NewLocalLicenseForm( )
        {
            InitializeComponent();
            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;
                _Person = new People();
            }
            else
            {
                _Mode = enMode.Update;
                _Person = People.Find(_PersonID);  // <-- IL MANQUE CETTE LIGNE
            }
        }


        private CtrlAddUser _ctrlAddUser;
        public NewLocalLicenseForm(CtrlAddUser form)
        {
            InitializeComponent();
            _ctrlAddUser = form;

        }
     

        private void ctrlAddUser1_Load(object sender, EventArgs e)
        {

        }

     
        private void NewLocalLicenseForm_Load_1(object sender, EventArgs e)
        {
            ctrlAddUser1.HideTabPage(); // Appel normal
            


        }

        private void button1_Click(object sender, EventArgs e)
        {

          


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           CtrlApplicationInfoNewLocalDriving ctrlApplicationInfoNewLocalDriving =new CtrlApplicationInfoNewLocalDriving();
            _Person=ctrlAddUser1.getPerson();
            Applications application = ctrlApplicationInfoNewLocalDriving.AddApplication(_Person);
           



        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();


        }

        private void btnNext_Click(object sender, EventArgs e)
        {

        }
    }
}
