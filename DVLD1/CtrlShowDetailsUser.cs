using System;
//using System.Drawing;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD1
{
    public partial class CtrlShowDetailsUser : UserControl
    {
        private User _user;
        private People _person;
        private UsersForm _UsersForm;

        public CtrlShowDetailsUser(People person,UsersForm usersForm)
        {
            InitializeComponent();
            _person = person;
            DisplayUserDetails();
            _UsersForm = usersForm;
        }
        public CtrlShowDetailsUser(int UserID)
        {
            InitializeComponent();
            _user=User.Find(UserID);
            _person = People.Find(_user.PersonID);
            DisplayUserDetails();
        }
        public CtrlShowDetailsUser()
        {
            InitializeComponent();
        }

        private void DisplayUserDetails()
        {
            if (_person != null)
            {
                _user = User.FindByPersonID(_person.PersonID);
                if (_user == null) return;

                _person = People.Find(_user.PersonID);
                if (_person == null) return;

                lblPersonID2.Text = _person.PersonID.ToString();
                lblName2.Text = $"{_person.FirstName} {_person.SecondName} {_person.ThirdName} {_person.LastName}".Trim();

                lblGender2.Text = _person.Gender == 0 ? "Male" : "Female";
                lblNationalNo2.Text = _person.NationalNo?.ToString() ?? "";
                lblAddress2.Text = _person.Address ?? "";

                Country country = Country.Find(_person.NationalityCountryID);
                lblCountry2.Text = country?.CountryName ?? "Unknown";

                lblDateOfBirth2.Text = _person.DateOfBirth.ToString("yyyy-MM-dd");
                lblPhone2.Text = _person.Phone ?? "";
                lblEmail2.Text = _person.Email ?? "";

                if (!string.IsNullOrEmpty(_person.ImagePath) && File.Exists(_person.ImagePath))
                {
                    try
                    {
                        pbImageInfoDetails.Image = Image.FromFile(_person.ImagePath);
                    }
                    catch
                    {
                        // fallback image in case of error
                        pbImageInfoDetails.Image = _person.Gender == 1
                            ? Properties.Resources.person_woman__4_
                            : Properties.Resources.person_man__2_;
                    }
                }
                else
                {
                    pbImageInfoDetails.Image = _person.Gender == 1
                        ? Properties.Resources.person_woman__4_
                        : Properties.Resources.person_man__2_;
                }

                lblUserID.Text = _user.UserID.ToString();
                lblUserName.Text = _user.UserName ?? "";
                lblIsActive.Text = _user.IsActive ? "Yes" : "No";
            }
        }

        private void CtrlShowDetailsUser_Load(object sender, EventArgs e)
        {
            DisplayUserDetails();
        }
        private void FormNewPersonDataBack(object sender, int personID)
        {
            _person = People.Find(personID);
            DisplayUserDetails();
        }
        public void ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(720, 460), // Taille élégante et classique
                FormBorderStyle = FormBorderStyle.FixedDialog, // Optionnel : empêche le redimensionnement
                MaximizeBox = false // Optionnel : enlève le bouton agrandir
            };

            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }
        private void LnkEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //CtrlAddPerson ctrl=new  CtrlAddPerson(_user.PersonID);
            //_UsersForm.ShowFormWithControl(ctrl, "Modifier une personne");

            if (_person != null)
            {
                CtrlAddPerson ctrl = new CtrlAddPerson(_person.PersonID);
                ctrl.DataBack += this.FormNewPersonDataBack;


                ShowFormWithControl(ctrl, "Modifier une personne");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}
