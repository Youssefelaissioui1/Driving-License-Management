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
    public partial class ctrlChangePassword : UserControl
    {
        public CtrlShowDetailsPerson ShowDetailsPersonControl => _ctrlShowDetailsPerson;
        public ctrlChangePassword _ctrlChangePassword;
        private DVLD1.CtrlLoginInfo _ctrlLoginInfo1;

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private User _user;
        private People _person;
        private int _UserID;
        private People currentPerson; // Toujours utiliser currentPerson pour référence cohérente

        private CtrlShowDetailsPerson _ctrlShowDetailsPerson;
        private CtrlLoginInfo _CtrlLoginInfo;
        public ctrlChangePassword()
        {
            InitializeComponent();
            _ctrlShowDetailsPerson = ctrlShowDetailsPerson1;
            _CtrlLoginInfo = _ctrlLoginInfo1;

            _user = new User();
            _Mode = enMode.AddNew;
            _UserID = -1;
            this.Load += ctrlChangePassword_Load;

        }

        public ctrlChangePassword(int UserID) : this()
        {
            _UserID = UserID;

            if (_UserID == -1)
            {
                _Mode = enMode.AddNew;
                _user = new User();
            }
            else
            {
                _Mode = enMode.Update;
                _user = User.Find(_UserID);
                if (_user == null)
                {
                    MessageBox.Show($"Utilisateur avec ID {_UserID} non trouvé.");
                    _user = new User();
                    _Mode = enMode.AddNew;
                }
                else
                {
                    // On récupère la personne liée à l'utilisateur
                    People person = People.Find(_user.PersonID);
                    _person=person;
                    if (person != null)
                    {
                        ctrlShowDetailsPerson2.SetPerson(person);
                    }
                    else
                    {
                        MessageBox.Show("Person is null dans CtrlAddUser (Update).");
                    }
                }
            }
        }

private void ctrlShowDetailsPerson1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter()
        {
            _user = User.FindByPersonID(_person.PersonID);
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text=_user.UserName;
            if (_user.IsActive == true)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "NO";
        }

        private void ctrlChangePassword_Load(object sender, EventArgs e)
        {
            groupBox1_Enter();
        }


        private void tbCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbCurrentPassword.Text) || _user.PassWord != tbCurrentPassword.Text)
            {
                e.Cancel = true;
                tbCurrentPassword.Focus();
                errorProvider1.SetError(tbCurrentPassword, "the Password is not correct");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbCurrentPassword, "");
            }
        }

        private void tbNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbNewPassword.Text))
            {
                e.Cancel = true;
                tbNewPassword.Focus();
                errorProvider1.SetError(tbNewPassword, "Confirm Password should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbNewPassword, "");
            }
        }

        private void tbConfirmPasword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(tbConfirmPasword.Text))
            {
                e.Cancel = true;
                tbConfirmPasword.Focus();
                errorProvider1.SetError(tbConfirmPasword, "Confirm Password should have a value!");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tbConfirmPasword, "");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbConfirmPasword.Text != tbNewPassword.Text)
            {

                MessageBox.Show("New password and Confirm Password is Not the same");
                return;
            }
            if (_user.PassWord != tbCurrentPassword.Text)
            {
                MessageBox.Show("the Password is not correct");
                return;
            }
            _user.PassWord = tbConfirmPasword.Text;
            _Mode = enMode.Update;
                if(_user.Save())
                MessageBox.Show("Password updated successfully.");
            LoginSettings.SaveCredentials(_user.UserName, _user.PassWord);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }
    }
}
