using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD1
{
    public partial class UsersForm : Form
    {
        private Form _detailsForm = null;
        private CtrlShowDetailsUser _ctrlShowDetailsUser = null;
        private CtrlShowDetailsPerson _ctrlShowDetailsPerson = null;
        private CtrlLoginInfo _CtrlLoginInfo;
        private CtrlAddUser _CtrlAddUser = null;
        private User _user;
        private People _person;

        public UsersForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // 👈 Affiche le form au centre de l'écran




     
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            textBox1.KeyPress += textBox1_KeyPress;

            comboBox1.Items.AddRange(new string[] {
                "None","UserID","PersonID",  "FullName","UserName", "IsActive"
            });
            comboBox1.SelectedIndex = 0;

            comboBox2.Visible = false;
            textBox1.Visible = false;

            _RefreshUsersList();

            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
        }

        public void _RefreshUsersList()
        {
            DataTable allUsers = User.GetAllUsers();

            if (allUsers != null && allUsers.Rows.Count > 0)
            {
                if (!allUsers.Columns.Contains("FullName"))
                    allUsers.Columns.Add("FullName", typeof(string));

                foreach (DataRow row in allUsers.Rows)
                {
                    row["FullName"] = $"{row["FirstName"]} {row["SecondName"]} {row["ThirdName"]} {row["LastName"]}";
                }

                DataTable filtered = allUsers.DefaultView.ToTable(false,
                    "UserID", "PersonID",  "FullName", "UserName", "IsActive");

                dataGridView1.DataSource = filtered;
            }
            else
            {
                dataGridView1.DataSource = null;
            }

            lblRecord.Text = $"# Records: {dataGridView1.Rows.Count}";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            CtrlAddUser ctrl = new CtrlAddUser();
            ShowFormWithControl(ctrl, "Ajouter un utilisateur");
            _RefreshUsersList();
        }

        public void ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(800, 610), // Taille élégante et classique
                FormBorderStyle = FormBorderStyle.FixedDialog, // Optionnel : empêche le redimensionnement
                MaximizeBox = false // Optionnel : enlève le bouton agrandir
            };

            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;

                var cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var cellLocation = dataGridView1.PointToScreen(cellRect.Location);
                contextMenuStrip1.Show(cellLocation.X + cellRect.Width, cellLocation.Y);
            }
        }

    

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur.");
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);
            User user = User.Find(userId);
            if (user == null)
            {
                MessageBox.Show("Utilisateur non trouvé.");
                return;
            }

            if (_detailsForm == null || _detailsForm.IsDisposed)
            {
                _person=People.Find(user.PersonID);
                _ctrlShowDetailsUser = new CtrlShowDetailsUser(_person,this);
                _detailsForm = new Form
                {
                    Text = "Détails de l'utilisateur",
                    Width = 800,
                    Height = 450,
                    StartPosition = FormStartPosition.CenterScreen // 👈 Affiche le form au centre de l'écran

                };
                _ctrlShowDetailsUser.Dock = DockStyle.Fill;
                _detailsForm.Controls.Add(_ctrlShowDetailsUser);
                _detailsForm.FormClosed += (s, args) =>
                {
                    _detailsForm = null;
                    _ctrlShowDetailsUser = null;
                };
                _detailsForm.Show();
            }
            else
            {
                //_ctrlShowDetailsUser.RefreshData(user);
                //_detailsForm.BringToFront();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox1.KeyPress -= textBox1_KeyPress;
            string filterText = textBox1.Text.Trim();
            string selectedField = comboBox1.SelectedItem?.ToString();
            DataTable dataTable = dataGridView1.DataSource as DataTable;

            if (dataTable == null || string.IsNullOrEmpty(selectedField))
                return;

            var fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
     { "UserID", "UserID" },
          { "PersonID", "PersonID" },
{ "FullName", "FullName" },
{ "UserName", "UserName" },
{ "IsActive", "IsActive" }

    };

            if (!fieldMap.TryGetValue(selectedField, out string columnName))
            {
                MessageBox.Show("Champ de filtre invalide.");
                return;
            }

            if (string.IsNullOrEmpty(filterText))
            {
                dataTable.DefaultView.RowFilter = string.Empty;
                return;
            }






            if (columnName == "PersonID" || columnName == "UserID")
            {
                textBox1.KeyPress += textBox1_KeyPress;
                if (int.TryParse(filterText, out int id))
                    dataTable.DefaultView.RowFilter = $"{columnName} = {id}";
                else
                    dataTable.DefaultView.RowFilter = "1 = 0";
            }
            else
            {

                string escaped = filterText.Replace("'", "''");
                dataTable.DefaultView.RowFilter = $"{columnName} LIKE '%{escaped}%'";
            }


        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string selectedField = comboBox1.SelectedItem?.ToString();

            if (selectedField == "UserID" || selectedField == "PersonID")
            {
                // Bloquer les lettres et caractères spéciaux, sauf les touches de contrôle
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedField = comboBox1.SelectedItem?.ToString();

            if (selectedField == "IsActive")
            {
                comboBox2.Visible = true;
                textBox1.Visible = false;



                comboBox2.SelectedIndex = 0; // Par défaut
            }
            else
            {
                comboBox2.Visible = false;
                textBox1.Visible = true;
            }
         
        }

   
        

     

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = dataGridView1.DataSource as DataTable;
            if (dataTable == null) return;

            string selectedStatus = comboBox2.SelectedItem?.ToString();
            if (selectedStatus == "Active")
                dataTable.DefaultView.RowFilter = "IsActive = true";
            else if (selectedStatus == "Inactive")
                dataTable.DefaultView.RowFilter = "IsActive = false";
            else
                dataTable.DefaultView.RowFilter = "";
        }


      
    
          public void editUser()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une personne à éditer.");
                return;
            }

            int personId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PersonID"].Value);
            User user = User.FindByPersonID(personId);
            CtrlAddUser ctrl = new CtrlAddUser(user.UserID);
            ShowFormWithControl(ctrl, "Edit User");

            People updatedPerson = People.Find(personId);
            _ctrlShowDetailsPerson?.RefreshData(updatedPerson);


            // Vérifie que la personne existe
            People person = People.Find(personId);

            if (user == null)
            {
                MessageBox.Show("user non trouvée.");
                return;
            }

            //// Crée le formulaire et passe la personne directement
            //CtrlAddUser ctrl = new CtrlAddUser(user.UserID);

            //ShowFormWithControl(ctrl, "Modifier une personne");

            _RefreshUsersList();
        

        }


        public void editToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            editUser();


         
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur à supprimer.");
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet utilisateur ?", "Confirmer", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (User.DeleteUser(userId))
                {
                    MessageBox.Show("User is Deleted Successfully");
                    _RefreshUsersList();

                }
                else
                {
                MessageBox.Show("User is not deleted due to data connected do it.","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une personne pour changer password.");
                return;
            }

            int personId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PersonID"].Value);

            // Vérifie que la personne existe
            People person = People.Find(personId);
            User user = User.FindByPersonID(personId);

            if (user == null)
            {
                MessageBox.Show("user non trouvée.");
                return;
            }

            // Crée le formulaire et passe la personne directement
            ctrlChangePassword ctrl = new ctrlChangePassword(user.UserID);

            ShowFormWithControl(ctrl, "Modifier une personne");

            _RefreshUsersList();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        public void ShowFormWithControl1(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                StartPosition = FormStartPosition.CenterScreen,
                Size = new Size(700, 480), // Taille élégante et classique
                FormBorderStyle = FormBorderStyle.FixedDialog, // Optionnel : empêche le redimensionnement
                MaximizeBox = false // Optionnel : enlève le bouton agrandir
            };

            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }
        private void showDetailsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une personne pour changer password.");
                return;
            }

            int personId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PersonID"].Value);

            // Vérifie que la personne existe
            People person = People.Find(personId);
            User user = User.FindByPersonID(personId);

            if (user == null)
            {
                MessageBox.Show("user non trouvée.");
                return;
            }

            // Crée le formulaire et passe la personne directement
            CtrlShowDetailsUser ctrl = new CtrlShowDetailsUser(person, this);
            ShowFormWithControl1(ctrl, "Details Of Person");

            _RefreshUsersList();
        }

        private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CtrlAddUser ctrl = new CtrlAddUser();
            ShowFormWithControl(ctrl, "Ajouter un utilisateur");
            _RefreshUsersList();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
         
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }

    
    }

