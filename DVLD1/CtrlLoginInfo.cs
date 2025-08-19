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
    public partial class CtrlLoginInfo : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private User _user;
        private int _UserID;
        private People currentPerson; // Toujours utiliser currentPerson pour référence cohérente

        private CtrlShowDetailsPerson _ctrlShowDetailsPerson;
        private CtrlLoginInfo _CtrlLoginInfo;
        private UsersForm _UsersForm;
        public CtrlLoginInfo()
        {
            InitializeComponent();
            _user = new User();
            _Mode = enMode.AddNew;
            _UserID = -1;
        }
      
        public CtrlLoginInfo(int UserID) : this()  // Appel du constructeur par défaut pour DRY
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
            }
        }
        public void RefreshData(User updatedPerson)
        {
            _user = updatedPerson;

           
            lblUserID.Text =updatedPerson.UserID.ToString();
            tbUserName.Text = updatedPerson.UserName;
            tbPassword.Text = updatedPerson.PassWord;
            tbConfirmPassword.Text = updatedPerson.PassWord.ToString();
            if(updatedPerson.IsActive) 
            ckbIsActive.Checked = true;
            else
            ckbIsActive.Checked = false;



            // Lève l'événement DataChanged avec la nouvelle personne
        }

        public void SetUser(User user)
        {_user = user;

            if (user == null)
            {
                MessageBox.Show("La personne fournie est nulle.");
                return;
            }

            RefreshData(user);
        }

        public void SetCurrentPerson(People person)
        {
            currentPerson = person;
        }


        private void CtrlLoginInfo_Load_1(object sender, EventArgs e)
        {
            SetUser(_user);

        }


    

     

        private User GetUserInfo()
        {
            if (_user == null)
                _user = new User();
            
            _user.UserName = tbUserName.Text.Trim();
            _user.PassWord = tbPassword.Text.Trim();
            _user.IsActive = ckbIsActive.Checked;

            return _user;
        }

     


        public void LoadUser( ref User user)
        {
            _user = user;
            if (user == null)
            {
                MessageBox.Show("L'utilisateur est nul.");
                return;
            }

            _user = user;
            lblUserID.Text = user.UserID.ToString();
            tbUserName.Text = user.UserName;
            tbPassword.Text = user.PassWord;
            tbConfirmPassword.Text = user.PassWord;
            ckbIsActive.Checked = user.IsActive;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close(); // Close the whole form
        }

        public void AddUser(People person)
        {
            if (string.IsNullOrWhiteSpace(tbUserName.Text) && string.IsNullOrWhiteSpace(tbPassword.Text) && string.IsNullOrWhiteSpace(tbConfirmPassword.Text))
            {
                MessageBox.Show("Please enter information of User in Login info", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tbPassword.Text != tbConfirmPassword.Text || string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(tbUserName.Text))
               { 
                MessageBox.Show("UserName is not validate", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _user.PassWord = tbPassword.Text;
            _user.UserName = tbUserName.Text;

            if (ckbIsActive.Checked)
                _user.IsActive = true;
            else
                _user.IsActive = false;

            _user = User.Find(int.Parse(lblUserID.Text));
            if(_user != null)
                _Mode = enMode.Update;
            else
                _Mode = enMode.AddNew;
            if (_Mode == enMode.AddNew)
            {
                _user = GetUserInfo();
                _user.PersonID = person.PersonID;
                _user.Save();
                MessageBox.Show("Utilisateur ajouté avec succès.");
                //_UsersForm._RefreshUsersList();
                lblUserID.Text = _user.UserID.ToString();


            }
            else
            {
                _user = GetUserInfo(); // MAJ info existante
                _Mode = enMode.Update;

                _user.Save();
                MessageBox.Show("Utilisateur mis à jour avec succès.");
            }
        }


        private void tbUserName_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUserName.Text))
            {
                tbUserName.Focus();
                errorProvider1.SetError(tbUserName, "UserName should have a value!");
                e.Cancel = true; // empêche de quitter le champ

            }
            else
            {
                errorProvider1.SetError(tbUserName, "");
            }
        }

        private void tbPassword_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                tbPassword.Focus();
                errorProvider1.SetError(tbPassword, "Password should have a value!");
                e.Cancel = true; // empêche de quitter le champ

            }
            else
            {
                errorProvider1.SetError(tbPassword, "");
            }
        }

        private void tbConfirmPassword_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbConfirmPassword.Text))
            {
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "Confirm Password should have a value!");
                e.Cancel = true; // empêche de quitter le champ

            }
            else if(tbConfirmPassword!=tbPassword)
            {
                tbConfirmPassword.Focus();
                errorProvider1.SetError(tbConfirmPassword, "Confirm Password and password are not the same!");
                e.Cancel = true; // empêche de quitter le champ
            }
            else
            {
                errorProvider1.SetError(tbConfirmPassword, "");
            }
        }

        private void lblUserID_Click(object sender, EventArgs e)
        {

        }
    }
}
