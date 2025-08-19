using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD1
{
    public partial class ManageDetainedLicensesForm : Form
    {
        public People _person;
        public ManageDetainedLicensesForm()
        {
            InitializeComponent();
        }

        public void RefreshList()
        {

            DataTable allUsers = DetainLicense.GetAll();

            if (allUsers != null && allUsers.Rows.Count > 0)
            {


                DataTable filtered = allUsers.DefaultView.ToTable(false,
                    "DetainID", "LicenseID", "DetainDate", "IsReleased", "FineFees", "ReleaseDate", "NationalNo", "FullName", "ReleaseApplicationID");

                //foreach (DataRow row in filtered.Rows)
                //{
                //    if (row["ReleaseDate"] != DBNull.Value)
                //    {
                //        row["ReleaseDate"] = "X";
                //    }

                //    if (row["ReleaseApplicationID"] != DBNull.Value)
                //    {
                //        row["ReleaseApplicationID"] = "X";
                //    }

                //}
             

                dataGridView1.DataSource = filtered;
                dataGridView1.Columns["DetainID"].HeaderText = "D.ID";
                dataGridView1.Columns["LicenseID"].HeaderText = "L.ID";
                dataGridView1.Columns["DetainDate"].HeaderText = "D.Date";
                dataGridView1.Columns["FineFees"].HeaderText = "Fine Fess";
                dataGridView1.Columns["ReleaseDate"].HeaderText = "Release Date";
                dataGridView1.Columns["ReleaseApplicationID"].HeaderText = "Released App.ID";



            }
            else
            {
                dataGridView1.DataSource = null;
            }
            lblRecord.Text = $"# Records: {dataGridView1.Rows.Count}";


        }
        private void ManageDetainedLicensesForm_Load(object sender, EventArgs e)
        {
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            RefreshList();


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
        public Form ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 680,
                Height = 300
            };
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.Show();
            return form;
        }
        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select Detained License   for Show Person Details .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          

                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                People person = People.FindByNationalNo(Convert.ToString(dataGridView1.Rows[selectedRowIndex].Cells["NationalNo"].Value));
                _person = person;
            

            if (_person == null)
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CtrlShowDetailsPerson ctrlShowDetailsPerson = new CtrlShowDetailsPerson();
            ctrlShowDetailsPerson.SetPerson(_person);


            ShowFormWithControl(ctrlShowDetailsPerson, "Person Details");
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select  Detained License  for Show License History .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
       

                int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                People person = People.FindByNationalNo(Convert.ToString(dataGridView1.Rows[selectedRowIndex].Cells["NationalNo"].Value));
                _person = person;
            
          
            if (_person == null)
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LicenseHistoryForm licenseHistoryForm = new LicenseHistoryForm(_person);
            licenseHistoryForm.Show();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select  Detained License  for Show License History .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            Licence license=Licence.GetLicenseInfoByLicenseID(Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["LicenseID"].Value));


            if (license == null)
            {
                MessageBox.Show("license not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowLicenseForm showLicenseForm = new ShowLicenseForm(license);
            showLicenseForm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DetainLicenseForm detainLicenseForm = new DetainLicenseForm();
            detainLicenseForm.ShowDialog();
            RefreshList();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm releaseDetainedLicenseForm = new ReleaseDetainedLicenseForm();
            releaseDetainedLicenseForm.ShowDialog();
            RefreshList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblRecord_Click(object sender, EventArgs e)
        {

        }
        public void SetMenuEnabled(ToolStripMenuItem menuItem, bool isEnabled)
        {
            menuItem.Enabled = isEnabled;
        }
        public void Filter()
        {

            string filterText = textBox1.Text.Trim();
            string selectedField = comboBox1.SelectedItem?.ToString();
            DataTable dataTable = dataGridView1.DataSource as DataTable;

            if (dataTable == null || string.IsNullOrEmpty(selectedField))
                return;

            var fieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
      { "Detain ID", "DetainID" },
     { "National No", "NationalNO" },
{ "Full Name", "FullName" },
{ "Release Application ID", "ReleaseApplicationID" },
                {"Is Released","IsReleased" }



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

            if (columnName == "DetainID" || columnName == "ReleaseApplicationID")
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filter();



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedField = comboBox1.SelectedItem?.ToString();

            if (selectedField == "Is Released")
            {
                comboBox2.Visible = true;
                textBox1.Visible = false;

                comboBox2.SelectedIndex = 0; // valeur par défaut
            }
            else
            {
                comboBox2.Visible = false;
                textBox1.Visible = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void releaseDetainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            Licence license = Licence.GetLicenseInfoByLicenseID(Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["LicenseID"].Value));


            if (license == null)
            {
                MessageBox.Show("license not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ReleaseDetainedLicenseForm releaseDetainedLicenseForm = new ReleaseDetainedLicenseForm(license,true);
           releaseDetainedLicenseForm.ShowDialog();
            RefreshList();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                releaseDetainToolStripMenuItem.Enabled = false;
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            bool isReleased = selectedRow.Cells["IsReleased"].Value != null
                              && selectedRow.Cells["IsReleased"].Value != DBNull.Value
                              && Convert.ToBoolean(selectedRow.Cells["IsReleased"].Value);

            // on ne peut "Release" que si ce n’est pas encore relâché
            releaseDetainToolStripMenuItem.Enabled = !isReleased;
        }

    
}
}
