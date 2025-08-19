using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Windows.Forms;
using DVLDBusinessLayer;
using System.Drawing;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace DVLD1
{
 
    public partial class CtrlAddPerson : UserControl
    {
        //delate send from add person to adduser data
        //declare elegate
        public delegate void DataBackEventHandler(object sender, int PersonID);
        //declare an event and using the delegate 
        public event DataBackEventHandler DataBack;



        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;

        private int _PersonID;
        private People _Person;

        public int PersonID { get; internal set; }

        public CtrlAddPerson(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;

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


        public CtrlAddPerson()
        {
            InitializeComponent();
            _Person = new People();
            _Mode = enMode.AddNew;
            _PersonID = -1;


        }

        private void CtrlAddPerson_Load(object sender, EventArgs e)
        {
            LoadCountries();

            if (_Mode == enMode.AddNew)
            {
                label1.Text = "Add New Person";
            }
            else
            {
                label1.Text = "Edit Person";
                LoadPersonData();
            }


            dtpDateOfBirth.MaxDate = DateTime.Today.AddYears(-18);
            dtpDateOfBirth.Value = DateTime.Today.AddYears(-18);   // Valeur sélectionnée par défaut
            gbGendor.TabIndex = 1;
            lnkRemoveImage.Visible = false;

            tbFirstName.Validating += erprFirstName_RightToLeftChanged;
            tbLastName.Validating += erprLastname_RightToLeftChanged;
            tbNationalNo.Validating += erprNationalNo_RightToLeftChanged;

            tbAddress.Validating += erprAddress_RightToLeftChanged;
            tbPhone.Validating += erprPhone_RightToLeftChanged;
            dtpDateOfBirth.Validating += dtpDateOfBirth_Validating;
            gbGendor.Validating += gbGender_Validating;
            if (_Mode == enMode.AddNew)
                cbxCountry.SelectedIndex = 118;



        }

        private void LoadCountries()
        {
            DataTable dtCountries = Country.GetAllCountries();
            cbxCountry.Items.Clear();

            foreach (DataRow row in dtCountries.Rows)
            {
                cbxCountry.Items.Add(row["CountryName"].ToString());
            }
        }

        private void LoadPersonData()
        {
            if (_Person == null)
            {
                MessageBox.Show($"Person with ID {_PersonID} not found.");
                return;
            }

            tbFirstName.Text = _Person.FirstName;
            tbSecondName.Text = _Person.SecondName;
            tbThirdName.Text = _Person.ThirdName;
            tbLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            tbPhone.Text = _Person.Phone;
            tbEmail.Text = _Person.Email;
            tbAddress.Text = _Person.Address;
            tbNationalNo.Text = _Person.NationalNo;

            if (_Person.Gender == 0)
                rbMale.Checked = true;
            else if (_Person.Gender == 1)
                rbFemale.Checked = true;
            else
            {
                rbMale.Checked = false;
                rbFemale.Checked = false;
            }

            string basePath = @"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources";
            string fullImagePath = Path.Combine(basePath, _Person.ImagePath);

            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(fullImagePath))
            {
                using (var fs = new FileStream(fullImagePath, FileMode.Open, FileAccess.Read))
                {
                    Image img = Image.FromStream(fs);
                    pbImage.Image = new Bitmap(img); // Clone the image to avoid locking the file
                }
            }
            else
            {
                if(rbMale.Checked)
                pbImage.Image = Image.FromFile(@"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources\person_man (2).png");
                if (rbFemale.Checked)
                    pbImage.Image = Image.FromFile(@"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources\person_woman (4).png");
            }




            // Sélectionner le pays dans la ComboBox
            if (_Person.NationalityCountryID > 0)
            {
                var country = Country.Find(_Person.NationalityCountryID);
                if (country != null)
                {
                    int index = cbxCountry.FindStringExact(country.CountryName);
                    if (index >= 0)
                        cbxCountry.SelectedIndex = index;
                }
            }
            else
            {
                cbxCountry.SelectedIndex = -1;
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string targetDirectory = @"C:\DVLD-People Image\";

                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }

                    if (_Person != null &&
                        !string.IsNullOrWhiteSpace(_Person.ImagePath) &&
                        File.Exists(_Person.ImagePath))
                    {
                        // Normaliser chemins
                        string normalizedImagePath = Path.GetFullPath(_Person.ImagePath).TrimEnd(Path.DirectorySeparatorChar);
                        string normalizedTargetDir = Path.GetFullPath(targetDirectory).TrimEnd(Path.DirectorySeparatorChar);

                        if (normalizedImagePath.StartsWith(normalizedTargetDir, StringComparison.OrdinalIgnoreCase))
                        {
                            // Libérer l'image affichée pour pouvoir supprimer le fichier
                            if (pbImage.Image != null)
                            {
                                pbImage.Image.Dispose();
                                pbImage.Image = null;
                            }

                            try
                            {
                                File.Delete(_Person.ImagePath);
                                System.Diagnostics.Debug.WriteLine("Image supprimée : " + _Person.ImagePath);
                                MessageBox.Show("Chemin image à supprimer : " + _Person.ImagePath);
                                bool fileExists = File.Exists(_Person.ImagePath);
                                MessageBox.Show("Le fichier existe : " + fileExists);
                            }
                            catch (Exception deleteEx)
                            {
                                MessageBox.Show("Impossible de supprimer l'ancienne image : " + deleteEx.Message);
                            }
                        }
                    }

                    // Copier la nouvelle image
                    string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(openFileDialog1.FileName);
                    string destinationPath = Path.Combine(targetDirectory, newFileName);
                    File.Copy(openFileDialog1.FileName, destinationPath, true);

                    // Charger l'image sans verrouiller le fichier
                    using (var fs = new FileStream(destinationPath, FileMode.Open, FileAccess.Read))
                    {
                        pbImage.Image = Image.FromStream(fs);
                    }

                    lnkRemoveImage.Visible = true;

                    _Person.ImagePath = destinationPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur lors du chargement de l'image : " + ex.Message);
                }
            }
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Si une nouvelle personne a été ajoutée avec succès
            if (_Mode == enMode.Update && _PersonID > 0)
            {
                // 🔥 Déclenche l’événement pour envoyer l’ID à CtrlAddUser
                DataBack?.Invoke(this, _PersonID);
            }

            // Ferme le formulaire parent
            this.FindForm()?.Close();


        }

  

  private void rbFemale_CheckedChanged(object sender, EventArgs e)
{
    if (rbFemale.Checked)
        pbImage.Image = Properties.Resources.person_woman__4_;
}

private void rbMale_CheckedChanged(object sender, EventArgs e)
{
    if (rbMale.Checked)
        pbImage.Image = Properties.Resources.person_man__2_;
}


        private void erprFirstName_RightToLeftChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                erprFirstName.SetError(tbFirstName, "Le Prenom ne peut pas être vide.");
                e.Cancel = true; // empêche de quitter le champ
            }
            else
            {
                erprFirstName.SetError(tbFirstName, ""); // supprime l’erreur
            }
        }

        private void tbLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void erprLastname_RightToLeftChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                erprLastname.SetError(tbLastName, "Le Nom ne peut pas être vide.");
                e.Cancel = true; // empêche de quitter le champ
            }
            else
            {
                erprLastname.SetError(tbLastName, ""); // supprime l’erreur
            }
        }

        private void erprNationalNo_RightToLeftChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbNationalNo.Text))
            {
                erprNationalNo.SetError(tbNationalNo, "Naional No ne peut pas être vide.");
                e.Cancel = true; // empêche de quitter le champ
            }
            else
            {
                erprNationalNo.SetError(tbNationalNo, ""); // supprime l’erreur
            }
            var no = People.FindByNationalNo(tbNationalNo.Text.Trim());
            if (no != null)
            {
                MessageBox.Show("National No is exist\n Please enter another .");
                tbNationalNo.Focus();
                return;

            }
            else
            {
                _Person.NationalNo = tbNationalNo.Text.Trim();
            }
        }

        private void erprAddress_RightToLeftChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbAddress.Text))
            {
                erprAddress.SetError(tbAddress, "Address No ne peut pas être vide.");
                e.Cancel = true; // empêche de quitter le champ
            }
            else
            {
                erprAddress.SetError(tbAddress, ""); // supprime l’erreur
            }
        }

   

        private void erprPhone_RightToLeftChanged(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPhone.Text))
            {
                erprPhone.SetError(tbPhone, "Phone No ne peut pas être vide.");
                e.Cancel = true; // empêche de quitter le champ
            }
            else
            {
                erprPhone.SetError(tbPhone, ""); // supprime l’erreur
            }
        }

        private void gbGender_Validating(object sender, CancelEventArgs e)
        {
            if (!rbFemale.Checked && !rbMale.Checked)
            {
                erprGender.SetError(gbGendor, "Veuillez sélectionner un genre.");
                e.Cancel = true;  // empêche de quitter le contrôle
            }
            else
            {
                erprGender.SetError(gbGendor, ""); // supprime l’erreur
            }
        }

        private void dtpDateOfBirth_Validating(object sender, CancelEventArgs e)
        {
            DateTime selectedDate = dtpDateOfBirth.Value;
            DateTime today = DateTime.Today;
            int age = today.Year - selectedDate.Year;

            if (selectedDate > today.AddYears(-age)) age--; // ajuste si l'anniversaire n'est pas encore passé

            if (age < 18)
            {
                erprDateOfBirth.SetError(dtpDateOfBirth, "La personne doit avoir au moins 18 ans.");
                e.Cancel = true;
            }
            else
            {
                erprDateOfBirth.SetError(dtpDateOfBirth, "");
            }
        }

        private void lnkRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string imagePath = _Person.ImagePath;

            // Libérer l'image affichée dans le PictureBox
            if (pbImage.Image != null)
            {
                pbImage.Image.Dispose();
                pbImage.Image = null;
            }

            // Appeler la méthode pour supprimer l’image
            DeleteImage(imagePath);

            // Remettre l’image par défaut
            if (rbFemale.Checked)
            {
                pbImage.Image = Image.FromFile(@"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources\person_woman (4).png");
            }
            else
            {
                pbImage.Image = Image.FromFile(@"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources\person_man (2).png");
            }

            // Nettoyer les infos image
            _Person.ImagePath = "";
            pbImage.ImageLocation = null;
            lnkRemoveImage.Visible = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbxCountry.SelectedItem == null)
            {
                MessageBox.Show("Please select a country.");
                return;
            }

            var country = Country.Find(cbxCountry.SelectedItem.ToString());
            if (country == null)
            {
                MessageBox.Show("Country not found.");
                return;
            }
         
            int gender = -1;
            if (rbMale.Checked) gender = 0;
            else if (rbFemale.Checked) gender = 1;

            // Remplir les propriétés de l'objet People à partir des contrôles
            _Person.FirstName = tbFirstName.Text.Trim();
            _Person.SecondName = tbSecondName.Text.Trim();
            _Person.ThirdName = tbThirdName.Text.Trim();
            if (tbLastName.Text.Length <= 0)
            {
                MessageBox.Show("Please enter your last name.");
                return;
            }
            else
            {
                _Person.LastName = tbLastName.Text.Trim();
            }

            _Person.DateOfBirth = dtpDateOfBirth.Value;
            _Person.Phone = tbPhone.Text.Trim();
            if (tbEmail.Text.Length != 0)
            {
                if (IsValidEmail(tbEmail.Text))
                {
                    _Person.Email = tbEmail.Text;
                    erprEmail.SetError(tbEmail, ""); // Nettoyer si OK
                }
                else
                {
                    erprEmail.SetError(tbEmail, "L'email n'est pas valide.");
                }
            }
            else
            {
                erprEmail.SetError(tbEmail, "L'email ne peut pas être vide.");
            }
            _Person.Gender = gender;
            _Person.Address = tbAddress.Text.Trim();
            _Person.NationalityCountryID = country.ID;

            string basePath = @"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources";
            string fullImagePath = Path.Combine(basePath, _Person.ImagePath);

            if (!string.IsNullOrEmpty(_Person.ImagePath) && File.Exists(fullImagePath))
            {
                using (var fs = new FileStream(fullImagePath, FileMode.Open, FileAccess.Read))
                {
                    Image img = Image.FromStream(fs);
                    pbImage.Image = new Bitmap(img); // Clone the image to avoid locking the file
                }
            }
            else
            {
                if (rbMale.Checked)
                    pbImage.Image = Image.FromFile(@"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources\person_man (2).png");
               else
                    pbImage.Image = Image.FromFile(@"C:\Bureau\programming advice\Cours 19 Project\DVLD\DVLD1\Resources\person_woman (4).png");
            }

            // Définir le mode (AddNew ou Update)
            _Person.Mode = (_Mode == enMode.AddNew) ? People.enMode.AddNew : People.enMode.Update;

            try
            {
                bool result = _Person.Save();
                label1.Text = "Edit Person ID = " + _Person.PersonID;

                if (result)
                {
                    MessageBox.Show("Data Saved Successfully.");

                    if (_Mode == enMode.AddNew)
                    {
                        _PersonID = _Person.PersonID; // récupère ID nouvellement créé si possible
                        _Mode = enMode.Update;
                        label1.Text = "Edit Contact";
                    }

                    MessageBox.Show($"Person ID: {_PersonID}, Name: {_Person.FirstName}");
                
                }
                else
                {
                    MessageBox.Show("Error: Data Is not Saved Successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception during save: " + ex.Message);
            }
            label3.Text = _PersonID.ToString();

        }

     
    

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        // Expression régulière pour valider une adresse e-mail simple
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

   

        public void DeleteImage(string imagePath)
        {
            try
            {
                MessageBox.Show("Chemin reçu : " + imagePath);

                if (!string.IsNullOrEmpty(imagePath))
                {
                    string fullPath = Path.GetFullPath(imagePath);

                    MessageBox.Show("Chemin complet : " + fullPath);
                    bool exists = File.Exists(fullPath);
                    MessageBox.Show("Existe ? " + exists);

                    if (exists)
                    {
                        File.Delete(fullPath);
                        MessageBox.Show("Image supprimée avec succès !");
                        Debug.WriteLine("Image supprimée : " + fullPath);
                    }
                    else
                    {
                        MessageBox.Show("Fichier non trouvé : " + fullPath);
                        Debug.WriteLine("Image introuvable ou chemin vide : " + fullPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression de l'image : " + ex.Message);
            }
        }

        private void cbxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtpDateOfBirth_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
