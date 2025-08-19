using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD1
{
    public partial class Form1 : Form
    {
        private Form _detailsForm = null;
        private CtrlShowDetailsPerson _ctrlShowDetailsPerson = null;

        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;// 👈 Affiche le form au centre de l'écran

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
            lblRecord.Text = $"# Records: {dataGridView1.Rows.Count}";

            comboBox2.Visible = false;

            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;


        }

        // Rafraîchir les données dans le DataGridView
        private void _RefreshContactsList()
        {
            DataTable allPeople = People.GetAllPeople();
            DataTable filtered = allPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                "SecondName", "ThirdName", "LastName", "DateOfBirth", "NationalityCountryID", "Gender", "Phone","Email");

            // Ajouter les colonnes dans la DataTable (et non dans le DataGridView)
            if (!filtered.Columns.Contains("CountryName"))
                filtered.Columns.Add("CountryName", typeof(string));

            if (!filtered.Columns.Contains("GenderText"))
                filtered.Columns.Add("GenderText", typeof(string));

            foreach (DataRow row in filtered.Rows)
            {
                if (row["NationalityCountryID"] != DBNull.Value &&
                    int.TryParse(row["NationalityCountryID"].ToString(), out int countryId))
                {
                    var country = Country.Find(countryId);
                    row["CountryName"] = country?.CountryName;
                }

                if (row["Gender"] != DBNull.Value &&
                       int.TryParse(row["Gender"].ToString(), out int gender))
                {
                    row["GenderText"] = gender == 0 ? "Male" : "Female";
                   
                }
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name == "Gender")
                    column.Visible = false;

                if (column.Name == "GenderText")
                    column.Visible = true;
            }
            dataGridView1.DataSource = filtered;
            dataGridView1.Columns["Gender"].Visible = false;
            dataGridView1.Columns["NationalityCountryID"].Visible = false;
          
        }


        // Ajouter une personne
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            CtrlAddPerson ctrl = new CtrlAddPerson();
            ShowFormWithControl(ctrl, "Ajouter une personne");
            _RefreshContactsList();
        }

        // Affichage formulaire avec un contrôle utilisateur
        private void ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 800,
                Height = 450,
                StartPosition = FormStartPosition.CenterScreen, // 👈 Affiche le form au centre de l'écran
              MinimizeBox=false,
              MaximizeBox=false
            };

            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }

        // Menu contextuel clic droit
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;

                var cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var cellLocation = dataGridView1.PointToScreen(cellRect.Location);
                contextMenuStrip1.Show(Cursor.Position);

                contextMenuStrip1.Show(cellLocation.X + cellRect.Width, cellLocation.Y);
            }
        }

        // Modifier la personne sélectionnée
        public void EditSelectedPerson()
        {
            
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une personne à éditer.");
                return;
            }

            int personId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PersonID"].Value);
            CtrlAddPerson ctrl = new CtrlAddPerson(personId);
            ShowFormWithControl(ctrl, "Modifier une personne");

            People updatedPerson = People.Find(personId);
            _ctrlShowDetailsPerson?.RefreshData(updatedPerson);
            _RefreshContactsList();
        }

        //public void editToolStripMenuItem3_Click(object sender, EventArgs e)
        //{
        //    EditSelectedPerson();
        //}

        //// Supprimer une personne
        //private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        //{
          
        //}

       

        // Changement du champ sélectionné dans ComboBox
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = comboBox1.SelectedIndex == 0;
        }

   


        public void ApplyFilter()
        {
            string filterText = textBox1.Text.Trim();
            string selectedField = comboBox1.SelectedItem?.ToString();
            DataTable dataTable = dataGridView1.DataSource as DataTable;

            if (dataTable == null || string.IsNullOrEmpty(selectedField))
                return;

            var fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
     { "PersonID", "PersonID" },
{ "National No", "NationalNo" },
{ "FirstName", "FirstName" },
{ "SecondName", "SecondName" },
{ "ThirdName", "ThirdName" },
{ "LastName", "LastName" },
{ "Phone", "Phone" },
{ "Nationality", "CountryName" },
{ "Gender", "GenderText" },
 { "Email", "Email" }


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

            if (columnName == "PersonID")
            {
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            ApplyFilter();

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox1.Clear();


          
            string selectedField = comboBox1.SelectedItem?.ToString();

            if (selectedField == "Gender")
            {
                textBox1.Visible = false;
                comboBox2.Visible = true;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Male");
                comboBox2.Items.Add("Female");
            }
            else
            {
                comboBox2.Visible = false;
                textBox1.Visible = true;
                textBox1.Clear();
            }


        }

        private void lblRecord_Click(object sender, EventArgs e)
        {
            lblRecord.Text = $"Records: {dataGridView1.Rows.Count}";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
            this.FindForm()?.Close(); // Close the whole form

        }

        //private void editToolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    // Créer un formulaire temporaire
        //    CtrlAddPerson ctrl = new CtrlAddPerson();
        //    ShowFormWithControl(ctrl, "Modifier une personne");
        //    _RefreshContactsList();
        //}

   

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            CtrlAddPerson ctrl = new CtrlAddPerson();
            ShowFormWithControl(ctrl, "Ajouter une personne");
            _RefreshContactsList();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = dataGridView1.DataSource as DataTable;
            string selectedField = comboBox1.SelectedItem?.ToString();
            string selectedValue = comboBox2.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedField) || string.IsNullOrEmpty(selectedValue))
                return;

            if (selectedField == "Gender")
            {
                // Convert text to number
                string genderValue = selectedValue == "Male" ? "0" : "1";

                // Appliquer le filtre sur la colonne Gender (qui contient des int)
                dataTable.DefaultView.RowFilter = $"Gender = {genderValue}";
            }
        }

        private void showDetailsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une personne.");
                return;
            }

            int personId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PersonID"].Value);
            People person = People.Find(personId);
            if (person == null)
            {
                MessageBox.Show("Personne non trouvée.");
                return;
            }

            if (_detailsForm == null || _detailsForm.IsDisposed)
            {
                _ctrlShowDetailsPerson = new CtrlShowDetailsPerson(person);
                _detailsForm = new Form
                {
                    Text = "Détails de la personne",
                    Width = 700,
                    Height = 330,
                    MinimizeBox = false,
                    MaximizeBox = false
                };
                _ctrlShowDetailsPerson.Dock = DockStyle.Fill;
                _detailsForm.Controls.Add(_ctrlShowDetailsPerson);
                _detailsForm.FormClosed += (s, args) =>
                {
                    _detailsForm = null;
                    _ctrlShowDetailsPerson = null;
                    _RefreshContactsList();
                };
                _detailsForm.Show();
            }
            else
            {
                _ctrlShowDetailsPerson.RefreshData(person);
                _detailsForm.BringToFront();
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditSelectedPerson();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une personne à supprimer.");
                return;
            }

            int personId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["PersonID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {

                People.DeletePerson(personId);

                if (!People.isPersonExist(personId))
                {
                    _RefreshContactsList();
                    MessageBox.Show($"Person with ID {personId} deleted successfully.");
                }

                else
                {
                    MessageBox.Show($"Person was not Deleted because it has data linked to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CtrlAddPerson ctrl = new CtrlAddPerson();
            ShowFormWithControl(ctrl, "Ajouter une personne");
            _RefreshContactsList();
        }
    }
}
