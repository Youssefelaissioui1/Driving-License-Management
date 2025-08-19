using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

using DVLDBusinessLayer;
using System.IO;

namespace DVLD1
{
    public partial class CtrlShowDetailsPerson : UserControl
    {
        private Form1 _mainForm;
        private People _person;
        public event EventHandler DataChanged;
        private CtrlShowDetailsPerson ctrlShowDetailsPerson;
        private CtrlShowDetailsPerson _CtrlShowDetailsPerson;



        public string FullName => lblName2.Text;
        public string NationalNo => lblNationalNo2.Text;
        public string Gendre => lblGender2.Text;
        public string DateOfBirth => lblDateOfBirth.Text;
        public string Phone => lblPhone.Text;
        public string Email => lblEmail.Text;


        protected void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public CtrlShowDetailsPerson(Form1 mainForm, People person)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _person = person;

            Country country1 = new Country();
            lblName2.Text = $"{person.FirstName} {person.ThirdName} {person.SecondName} {person.LastName}";
            lblPhone2.Text = person.Phone;
            lblGender2.Text = (person.Gender == 0) ? "Male" : "Female";
            country1 = Country.Find(person.NationalityCountryID);
            lblCountry2.Text = country1.CountryName.ToString();
            lblDateOfBirth2.Text = person.DateOfBirth.ToShortDateString();
            lblAddress2.Text = person.Address;
            lblEmail2.Text = person.Email;
            lblNationalNo2.Text = person.NationalNo;
            lblPersonID2.Text = person.PersonID.ToString();
            if (File.Exists(person.ImagePath))
            {
                pbImageInfoDetails.Image = Image.FromFile(person.ImagePath);
            }
        }


        public CtrlShowDetailsPerson()
        {
            InitializeComponent();
            LnkEditPerson.Visible = false; // Masquer le lien en mode ajout

        }

        public CtrlShowDetailsPerson(People person)
        {
            _person = person;
            InitializeComponent();

            Country country1 = new Country();
            lblName2.Text = $"{person.FirstName}{person.ThirdName}{person.SecondName} {person.LastName}";
            lblPhone2.Text = person.Phone;
            if (person.Gender == 0)
                lblGender2.Text = "Male";
            else lblGender2.Text = "Female";
             country1 = Country.Find(person.NationalityCountryID);
            lblCountry2.Text = country1.CountryName.ToString();
            lblDateOfBirth2.Text = person.DateOfBirth.ToShortDateString();
            lblAddress2.Text = person.Address;
            lblEmail2.Text = person.Email;
            lblNationalNo2.Text = person.NationalNo;
            lblPersonID2.Text = person.PersonID.ToString();
            if (File.Exists(person.ImagePath))
            {
                pbImageInfoDetails.Image = Image.FromFile(person.ImagePath);
            }
            else
            {
                if (_person.Gender == 1)
                    pbImageInfoDetails.Image = Properties.Resources.person_woman__4_;
                else
                    pbImageInfoDetails.Image = Properties.Resources.person_man__2_;
            }




        }

     

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close(); // Close the whole form

        }

        public Form ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 800,
                Height = 450
            };
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.Show(); // Show au lieu de ShowDialog pour permettre l'événement FormClosed
            return form;
        }

        public void LnkEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            CtrlAddPerson ctrl = new CtrlAddPerson(_person.PersonID);

            Form frm = ShowFormWithControl(ctrl, "Modifier une personne");

            frm.FormClosed += (s, args) =>
            {
                People updatedPerson = People.Find(_person.PersonID);
                if (updatedPerson != null)
                {
                    _person = updatedPerson;
                    SetPerson(_person);
                    OnDataChanged(); // Notifie les autres contrôles
                }
            };
        }

        






        public void RefreshData(People updatedPerson)
        {
            
            _person = updatedPerson;
            Country country1 = Country.Find(_person.NationalityCountryID);
          
            lblName2.Text = $"{_person.FirstName} {_person.ThirdName} {_person.SecondName} {_person.LastName}";
            lblPhone2.Text = _person.Phone;
            lblGender2.Text = (_person.Gender == 0) ? "Male" : "Female";
            lblCountry2.Text = country1.CountryName.ToString();
            lblDateOfBirth2.Text = _person.DateOfBirth.ToShortDateString();
            lblAddress2.Text = _person.Address;
            lblEmail2.Text = _person.Email;
            lblNationalNo2.Text = _person.NationalNo;
            lblPersonID2.Text = _person.PersonID.ToString();

            if (File.Exists(_person.ImagePath))
            {
                pbImageInfoDetails.Image = Image.FromFile(_person.ImagePath);
            }
            else
            {
                if (_person.Gender == 1)
                    pbImageInfoDetails.Image = Properties.Resources.person_woman__4_;
                else
                    pbImageInfoDetails.Image = Properties.Resources.person_man__2_;
            }

            // Lève l'événement DataChanged avec la nouvelle personne
        }

     
        public void SetPerson(People person)
        {
            if (person == null)
            {
                MessageBox.Show("La personne fournie est nulle.");
                return;
            }
            RefreshData(person);
            LnkEditPerson.Visible = true; // Afficher le lien

        }
        public void UpdateAll(People person, User user)
        {
            SetPerson(person);
            RefreshData(person);
        }

        private void CtrlShowDetailsPerson_Load(object sender, EventArgs e)
        {

        }

        private void pbImageInfoDetails_Click(object sender, EventArgs e)
        {

        }

        private void gbPersonInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}
