using System;
using System.ComponentModel;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD1
{
    public partial class CtrlAddUser : UserControl
    {
        public CtrlShowDetailsPerson ShowDetailsPersonControl => _ctrlShowDetailsPerson;

        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private User _user;
        private People _person;

        private int _UserID;
        public People currentPerson; // Toujours utiliser currentPerson pour référence cohérente

        private CtrlShowDetailsPerson _ctrlShowDetailsPerson;
        private CtrlLoginInfo _CtrlLoginInfo;
        private CtrlAddUser _ctrlAddUser;

        public CtrlAddUser()
        {
            InitializeComponent();

            _ctrlShowDetailsPerson = ctrlShowDetailsPerson1;
            _CtrlLoginInfo = ctrlLoginInfo1;
            _user = new User();
            _Mode = enMode.AddNew;
            _UserID = -1;
        }

        public CtrlAddUser(int UserID) : this()
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
                    People person = People.Find(_user.PersonID);
                    if (person != null)
                    {
                        ctrlShowDetailsPerson3.SetPerson(person);
                        ctrlLoginInfo3.SetUser(_user);
                        comboBox3.Text = "Person ID";
                        tbSearch.Text = person.PersonID.ToString();
                       
                        gbFilter.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Person is null dans CtrlAddUser (Update).");
                    }
                }
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedField = comboBox3.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedField))
            {
                MessageBox.Show("Please select a search field.");
                return;
            }

            People foundPerson = null;

            if (selectedField == "PersonID")
            {
                string searchText = tbSearch.Text.Trim();
                if (int.TryParse(searchText, out int id))
                {
                    foundPerson = People.Find(id);
                }
                else
                {
                    MessageBox.Show("Invalid PersonID format.");
                    return;
                }
            }
            else if (selectedField == "National No")
            {
                string searchText = tbSearch.Text.Trim();
                if (!string.IsNullOrEmpty(searchText))
                {
                    foundPerson = People.FindByNationalNo(searchText);
                }
                else
                {
                    MessageBox.Show("Please enter a National No.");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Search by PersonID or National No only.");
                return;
            }

            if (foundPerson == null)
            {
                MessageBox.Show("Person not found.");
                return;
            }
            currentPerson = foundPerson;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void btnNextPage_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }



    
        private Form ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 850,  // Taille élégante et raisonnable
                Height = 600,
                StartPosition = FormStartPosition.CenterScreen
            };

            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.Show();

            return form; // ✅ Retourne la référence
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        public void FormNewPersonDataBack(object sender, int PersonID)
        {
            //_person=People.Find(PersonID);
            //SetPerson(_person); // Utilise ta méthode pour charger la personne et l'utilisateur lié
            tbSearch.Text = PersonID.ToString();
            comboBox3.SelectedIndex = 0;
            btnSearch.PerformClick(); // simulate le clic
        }


        
     

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            People person = null;
            if (comboBox3.SelectedIndex == 0)
            {
                string searchText = tbSearch.Text.Trim();
                int id;
                if (!int.TryParse(searchText, out id))
                {
                    MessageBox.Show("ID invalide : " + searchText + "\nVeuillez entrer un numéro valide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                person = People.Find(id);
              
            }
            else if (comboBox3.SelectedIndex == 1)
            {

                person = People.FindByNationalNo(tbSearch.Text.ToString());
               
            }
            if (person == null)
            {
                MessageBox.Show("Person not found!", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlShowDetailsPerson3.RefreshData(person);
            currentPerson=person;
            ctrlShowDetailsPerson3.SetPerson(currentPerson);


        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                MessageBox.Show("Please choose a person.");
                return;
            }

            if (User.isPersonExist(currentPerson.PersonID)) {
                MessageBox.Show("Selected Person already has a user,choose another one.","select another person",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
                }
            tabControl1.SelectedIndex = 1;
        }

        private void ctrlLoginInfo2_Load(object sender, EventArgs e)
        {
        
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ctrlLoginInfo3.AddUser(currentPerson);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.FindForm()?.Close(); 
        }

        private void CtrlAddUser_Load(object sender, EventArgs e)
        {
            btnNext2.Visible = false;
            comboBox3.SelectedIndex = 0;

        }
        public void HideTabPage()
        {
            lblPersonDetails.Visible = false;
            lbltittleNewlocal.Visible = true;
            tabControl1.TabPages.Remove(tb2Logininfo);

            TabPage tabPage2 = new TabPage("Informations Application");
            tabPage2.Name = "tb2Logininfo";

            CtrlApplicationInfoNewLocalDriving ctrlAppInfo = new CtrlApplicationInfoNewLocalDriving();
                tabPage2.Controls.Add(ctrlAppInfo);
                ctrlAppInfo.Dock = DockStyle.Fill;
          


            // Ajouter la nouvelle page au TabControl
            tabControl1.TabPages.Add(tabPage2);

            //btnNext.Visible= false;
            btnSave.Visible= false;
            button1.Visible= false;
            btnNext.Visible= false;
            btnNext2.Visible = true;



        }


        private void LnkEditPerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {

            CtrlAddPerson ctrl = new CtrlAddPerson();

            // 🔗 Souscrire à l’événement DataBack
            ctrl.DataBack += FormNewPersonDataBack;

            Form frm = ShowFormWithControl(ctrl, "Ajouter une nouvelle personne");
        }

        public People getPerson()
        {
            return currentPerson;
        }

        private void btnNext2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbSearch.Text))
            {
                MessageBox.Show("Please choose a person.");
                return;
            }

            tabControl1.SelectedIndex = 1;
        }
    }

}