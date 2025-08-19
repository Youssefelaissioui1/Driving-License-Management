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
    public partial class LicenseHistoryForm : Form
    {
        Licence _Licence;
        People _People;
        public LicenseHistoryForm(People people)
        {
            InitializeComponent();
            _People = people;

        }

        private void LicenseHistoryForm_Load(object sender, EventArgs e)
        {
        

        }

        public void Refresh_list_Local_License()
        {
            DataTable allUsers = Licence.GetAllLicense_Of_Person(_People.PersonID);

            if (allUsers != null && allUsers.Rows.Count > 0)
            {
                if (!allUsers.Columns.Contains("Class Name"))
                {
                    allUsers.Columns.Add("Class Name", typeof(string));
                }
              

              
                foreach (DataRow row in allUsers.Rows)
                {
                    int classId = Convert.ToInt32(row["LicenseClass"]);
                    LicenseClasses licenseClass = LicenseClasses.Find(classId);
                    row["Class Name"] = licenseClass != null ? licenseClass.ClassName : "Unknown";
                  
                }

                DataTable filtered = allUsers.DefaultView.ToTable(false,
                  "LicenseID", "Class Name", "ApplicationID", "IssueDate", "ExpirationDate", "IsActive");

            

                dataGridView1.DataSource = filtered;
                dataGridView1.Columns["LicenseID"].HeaderText = "Lic.ID";
                dataGridView1.Columns["ApplicationID"].HeaderText = "App.ID";
           


            }
            else
            {
                dataGridView1.DataSource = null;
            }

            lblRecords.Text = $"# Records: {dataGridView1.Rows.Count}";
        }


        public void Refresh_list_International_License()
        {
            Driver driver=Driver.GetInfoDriver(_People.PersonID);
            if (driver == null)
            {
                dataGridView2.DataSource = null;
                lblRecordsInternational.Text = "No Driver Information Available";
                return;
            }
            DataTable allUsers = Licence.GetAllInternationalLicense(driver.DriverID);

            if (allUsers != null && allUsers.Rows.Count > 0)
            {



                DataTable filtered = allUsers.DefaultView.ToTable(false,
                  "internationalLicenseID", "ApplicationID", "LicenseID", "IssueDate", "ExpirationDate", "IsActive");



                dataGridView2.DataSource = filtered;
                dataGridView2.Columns["LicenseID"].HeaderText = "Lic.ID";
                dataGridView2.Columns["ApplicationID"].HeaderText = "App.ID";
                dataGridView2.Columns["internationalLicenseID"].HeaderText = "Int License ID";




            }
            else
            {
                dataGridView2.DataSource = null;
            }

            lblRecordsInternational.Text = $"# Records: {dataGridView2.Rows.Count}";
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        
        }
        private void LicenseHistoryForm_Load_1(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            textBox1.Text = _People.PersonID.ToString();
            comboBox1.SelectedIndex = 0;
            ctrlShowDetailsPerson1.RefreshData(_People);
            Refresh_list_Local_License();
            Refresh_list_International_License();
        }

        private void ctrlShowDetailsPerson1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }

        private void tpInternational_Click(object sender, EventArgs e)
        {

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select   license for Show License Info  .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            Licence licence = Licence.GetLicenseInfoByLicenseID(Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["LicenseID"].Value));
            ShowLicenseForm showLicenseForm = new ShowLicenseForm(licence);
            showLicenseForm.LoadData();
            showLicenseForm.ShowDialog();
        }

        private void dataGridView1_CellMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
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

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dataGridView2.ClearSelection();
                dataGridView2.Rows[e.RowIndex].Selected = true;

                var cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                var cellLocation = dataGridView1.PointToScreen(cellRect.Location);
                contextMenuStrip2.Show(cellLocation.X + cellRect.Width, cellLocation.Y);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //if (dataGridView1.SelectedRows.Count == 0)
            //{
            //    MessageBox.Show("select international license for Show License Info  .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            //InternationalLicenseApplication inter = InternationalLicenseApplication.(Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["LicenseID"].Value));
            //ShowLicenseForm showLicenseForm = new ShowLicenseForm(licence);
            //showLicenseForm.LoadData();
            //showLicenseForm.ShowDialog();
        }
    }
    }

