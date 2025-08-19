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
using static DVLD1.LocalDrivingLicenseApplicationForm_Table_View;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD1
{
    public partial class LocalDrivingLicenseApplicationForm_Table_View : Form
    {
        static Applications _Applications; 
        public delegate void DataBackEventHandler(object sender);
        //declare an event and using the delegate 
        public event DataBackEventHandler DataBack;
        public event EventHandler DataChanged;

        public enum TestMode { Vision = 0, Writing = 1, Street = 2 ,Issue=3};
        public static TestMode _Modetest;

        public LocalDrivingLicenseApplicationForm_Table_View()
        {
            InitializeComponent();
            //notifyIcon1.Icon = SystemIcons.Application;

        }

        public   void _RefreshUsersList()
        {
            DataTable allUsers = LocalDrivingLicenseApplication.GetAll();

            if (allUsers != null && allUsers.Rows.Count > 0)
            {
               

                DataTable filtered = allUsers.DefaultView.ToTable(false,
                    "LocalDrivingLicenseApplicationID", "ClassName", "NationalNo", "FullName", "ApplicationDate", "PassedTestCount", "Status");
             

                dataGridView1.DataSource = filtered;
                dataGridView1.Columns["PassedTestCount"].HeaderText = "Passed Test";
                dataGridView1.Columns["ClassName"].HeaderText = "Driving Class";
                dataGridView1.Columns["LocalDrivingLicenseApplicationID"].HeaderText = "L.D.L.ApplID ";

            }
            else
            {
                dataGridView1.DataSource = null;
            }

            lblRecord.Text = $"# Records: {dataGridView1.Rows.Count}";
        }

        public void RefreshMenuAccordingToSelection()
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                SetMenuEnabled(toolStripMenuItem3, false);
                SetMenuEnabled(toolStripMenuItem4, false);
                SetMenuEnabled(toolStripMenuItem5, false);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow.Cells["PassedTestCount"].Value != DBNull.Value)
            {
                int passedCount = Convert.ToInt32(selectedRow.Cells["PassedTestCount"].Value);
                int Statut = 0; // valeur par défaut

                if (passedCount == 0)
                {
                    SetMenuEnabled(IssueDrivingLicenseToolStripMenuItem1, false);
                    SetMenuEnabled(toolStripMenuItem1, true);
                    SetMenuEnabled(toolStripMenuItem3, true);
                    SetMenuEnabled(toolStripMenuItem4, false);
                    SetMenuEnabled(toolStripMenuItem5, false);
                }
                else if (passedCount == 1)
                {
                    SetMenuEnabled(IssueDrivingLicenseToolStripMenuItem1, false);
                    SetMenuEnabled(toolStripMenuItem1, true);
                    SetMenuEnabled(toolStripMenuItem3, false);
                    SetMenuEnabled(toolStripMenuItem4, true);
                    SetMenuEnabled(toolStripMenuItem5, false);
                }
                else if (passedCount == 2)
                {
                    SetMenuEnabled(IssueDrivingLicenseToolStripMenuItem1, false);
                    SetMenuEnabled(toolStripMenuItem1, true);
                    SetMenuEnabled(toolStripMenuItem3, false);
                    SetMenuEnabled(toolStripMenuItem4, false);
                    SetMenuEnabled(toolStripMenuItem5, true);
                }
                else if (passedCount == 3)
                {
                    SetMenuEnabled(toolStripMenuItem1, false);
                    SetMenuEnabled(IssueDrivingLicenseToolStripMenuItem1, true);


                }
                if (selectedRow.Cells["Status"] != null && selectedRow.Cells["Status"].Value != DBNull.Value)
                {
                    var cellValue = selectedRow.Cells["Status"].Value.ToString();


                    if (cellValue == "New")
                    {
                        SetMenuEnabled(ShowLicenseToolStripMenuItem1, false);

                    }
                    if (cellValue == "Cancelled")
                    {
                        SetMenuEnabled(toolStripMenuItem1, false);
                        SetMenuEnabled(ShowLicenseToolStripMenuItem1, false);

                    }
                    if (cellValue == "Completed")
                    {
                        SetMenuEnabled(IssueDrivingLicenseToolStripMenuItem1, false);
                        SetMenuEnabled(ShowLicenseToolStripMenuItem1, true);
                        SetMenuEnabled(editToolStripMenuItem3, false);
                        SetMenuEnabled(deleteToolStripMenuItem1, false);
                        SetMenuEnabled(toolStripMenuItem2, false);
                    }

                }

            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            RefreshMenuAccordingToSelection();
        }


        private void LocalDrivingLicenseApplicationForm_Table_View_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] {
                    "LocalDrivingLicenseApplicationID",  "NationalNo", "FullName", "Status"
        });
            comboBox1.Text = "None";
            

            comboBox2.Visible = false;
            textBox1.Visible = false;
            _RefreshUsersList();
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;



        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Visible = true;

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
        { "LocalDrivingLicenseApplicationID", "LocalDrivingLicenseApplicationID" },
        { "NationalNo", "NationalNo" },
        { "FullName", "FullName" },
        { "Status", "Status" }
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

            if (columnName == "LocalDrivingLicenseApplicationID")
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


        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (comboBox1.SelectedIndex == 0 )
)
            {
                e.Handled = true;
            }
        }

 
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedField = comboBox1.SelectedItem?.ToString();

            if (selectedField == "Status")
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            NewLocalLicenseForm form = new NewLocalLicenseForm();
            form.Size = new Size(760, 600); // Définir la taille avant
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            _RefreshUsersList();
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

  

        private void deleteToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select local driving license for delete .");
                return;
            }

            int ApplicationId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value);
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet local driving license ?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (LocalDrivingLicenseApplication.Delete(ApplicationId))
                {
                    MessageBox.Show("application is Deleted Successfully");
                    _RefreshUsersList();

                }
                else
                {
                    MessageBox.Show("application is not deleted due to data connected do it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("select local driving license for Cancel .");
                return;
            }

            int ApplicationId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value);
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir Cancel cet local driving license ?", "Confirmer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (LocalDrivingLicenseApplication.Cancel(ApplicationId))
                {
                    MessageBox.Show("application is Deleted Successfully");
                    _RefreshUsersList();

                }
                else
                {
                    MessageBox.Show("application is not Canceled due to data connected do it.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Exemple de méthode qui active ou désactive un menu par paramètre
        public void SetMenuEnabled(ToolStripMenuItem menuItem, bool isEnabled)
        {
            menuItem.Enabled = isEnabled;
        }
        protected void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }
        public void ScheduleTests(TestMode mode)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une Application.");
                return;
            }

            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value);
            LocalDrivingLicenseApplication LocalDrivingLicenseApplication = LocalDrivingLicenseApplication.Find(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Application non trouvée.");
                return;
            }

            LocalDrivingLicenseApplication LocalDrivingLicenseApplication1 = LocalDrivingLicenseApplication.Find2notview(LocalDrivingLicenseApplicationID);

            Applications application = Applications.GetApplicationsbyID(LocalDrivingLicenseApplication1.ApplicationID);
            _Applications=application;
            if (application == null)
            {
                MessageBox.Show("Application non trouvée.");
                return;
            }

            VisionTestAppointmentsForm visionTestAppointmentsForm = new VisionTestAppointmentsForm(LocalDrivingLicenseApplication, application, mode);
            visionTestAppointmentsForm.DataBack += (sender) =>
            {
                _RefreshUsersList();
                RefreshMenuAccordingToSelection();
            };
            visionTestAppointmentsForm.ShowDialog();

        }
    
        public void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ScheduleTests(TestMode.Vision);
            _Modetest= TestMode.Vision;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ScheduleTests(TestMode.Writing);
            _Modetest = TestMode.Writing;

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ScheduleTests(TestMode.Street);
            _Modetest = TestMode.Street;
        }

        private void IssueDrivingLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une Application.");
                return;
            }

            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value);
            LocalDrivingLicenseApplication LocalDrivingLicenseApplication1 = LocalDrivingLicenseApplication.Find2notview(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication1 == null)
            {
                MessageBox.Show("Application non trouvée.");
                return;
            }


            Applications application = Applications.GetApplicationsbyID(LocalDrivingLicenseApplication1.ApplicationID);
            if (application == null)
            {
                MessageBox.Show("Application non trouvée.");
                return;
            }

            //VisionTestAppointmentsForm visionTestAppointmentsForm = new VisionTestAppointmentsForm(LocalDrivingLicenseApplication, application, mode);
            //visionTestAppointmentsForm.DataBack += (sender) =>
            //{
            //    _RefreshUsersList();
            //    RefreshMenuAccordingToSelection();
            //};
            //visionTestAppointmentsForm.ShowDialog();
            _Modetest = TestMode.Issue;
            IssueDrivingLicenseForm issueDrivingLicenseForm = new IssueDrivingLicenseForm(LocalDrivingLicenseApplication1, application,_Modetest);
            issueDrivingLicenseForm.ShowDialog();
            _RefreshUsersList();

        }

        protected void OnDataChanged1()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }


        private void ShowLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une Application.");
                return;
            }

            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value);
            LocalDrivingLicenseApplication LocalDrivingLicenseApplication1 = LocalDrivingLicenseApplication.Find2notview(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication1 == null)
            {
                MessageBox.Show("Application non trouvée.");
                return;
            }


            Applications application = Applications.GetApplicationsbyID(LocalDrivingLicenseApplication1.ApplicationID);
            Licence licence = new Licence();
            licence =Licence.GetLicenseInfoByApplicationID(application.ApplicationID);
            if(licence== null)
            {
                MessageBox.Show("Licence is null.");
                return;
            }
            People people = new People();
            people=People.Find(application.ApplicantPersonID);

            if (application == null)
            {
                MessageBox.Show("Application is null.");
                return;
            }

            ShowLicenseForm showLicenseForm = new ShowLicenseForm(LocalDrivingLicenseApplication1, application, licence, people);
         
                _RefreshUsersList();
                RefreshMenuAccordingToSelection();


            showLicenseForm.ShowDialog();
        
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner une Application.");
                return;
            }

            int LocalDrivingLicenseApplicationID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LocalDrivingLicenseApplicationID"].Value);
            LocalDrivingLicenseApplication LocalDrivingLicenseApplication1 = LocalDrivingLicenseApplication.Find2notview(LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseApplication1 == null)
            {
                MessageBox.Show("Application non trouvée.");
                return;
            }


            Applications application = Applications.GetApplicationsbyID(LocalDrivingLicenseApplication1.ApplicationID);
            Licence licence = new Licence();
            licence = Licence.GetLicenseInfoByApplicationID(application.ApplicationID);
            //if (licence == null)
            //{
            //    MessageBox.Show("Licence is null.");
            //    return;
            //}
            People people = new People();
            people = People.Find(application.ApplicantPersonID);

            if (application == null)
            {
                MessageBox.Show("Application is null.");
                return;
            }

            LicenseHistoryForm LicenseHistoryForm = new LicenseHistoryForm( people);

            LicenseHistoryForm.ShowDialog();
        }
    }
    
}
