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
    public partial class InternationalLicenseListForm : Form
    {
        public InternationalLicenseListForm()
        {
            InitializeComponent();
        }
        public void _RefreshUsersList()
        {
            DataTable allUsers = InternationalLicenseApplication.GetAll();

            if (allUsers != null && allUsers.Rows.Count > 0)
            {


                DataTable filtered = allUsers.DefaultView.ToTable(false,
                    "InternationalLicenseID", "ApplicationID", "DriverID","LicenseID", "IssueDate", "ExpirationDate", "IsActive");


                dataGridView1.DataSource = filtered;
                //dataGridView1.Columns["PassedTestCount"].HeaderText = "Passed Test";
                //dataGridView1.Columns["ClassName"].HeaderText = "Driving Class";
                //dataGridView1.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "L.D.L.ApplID ";

            }
            else
            {
                dataGridView1.DataSource = null;
            }

            lblRecords.Text = $"# Records: {dataGridView1.Rows.Count}";
        }
        private void InternationalLicenseListForm_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;

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

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select Intertnational driving license for Show Person Details .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            Driver driver = Driver.GetInfoDriverByPersonID(Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["DriverID"].Value));
            if (driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            People person = People.Find(driver.PersonID);
            if (person == null)
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CtrlShowDetailsPerson ctrlShowDetailsPerson = new CtrlShowDetailsPerson();
            ctrlShowDetailsPerson.SetPerson(person);


            ShowFormWithControl(ctrlShowDetailsPerson, "Person Details");
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {


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
      { "InternationalLicenseID", "InternationalLicenseID" },
     { "ApplicationID", "ApplicationID" },
{ "DriverID", "DriverID" },
{ "LicenseID", "LicenseID" }

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

            if (columnName == "InternationalLicenseID" || columnName == "DriverID" || columnName== "ApplicationID" || columnName== "LicenseID")
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
        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select Intertnational driving license for Show License Details .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            Driver driver = Driver.GetInfoDriverByPersonID(Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["DriverID"].Value));
            if (driver == null)
            {
                MessageBox.Show("Driver not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            People person = People.Find(driver.PersonID);
            if (person == null)
            {
                MessageBox.Show("Person not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LicenseHistoryForm licenseHistoryForm = new LicenseHistoryForm(person);
            licenseHistoryForm.Show();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            AddNewInternationalLicenseForm addNewInternationalLicenseForm =new AddNewInternationalLicenseForm()  ;
            addNewInternationalLicenseForm.ShowDialog();
        }
    }
}
