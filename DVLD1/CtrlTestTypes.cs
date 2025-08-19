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
    public partial class CtrlTestTypes : UserControl
    {
        public CtrlTestTypes()
        {
            InitializeComponent();
            _RefreshApplicationTypesList();

        }
        public void _RefreshApplicationTypesList()
        {

            DataTable allApplicationTypes = TestTypes.GetTestTypes();
            DataTable filtered = allApplicationTypes.DefaultView.ToTable(false, "TestTypeID", "TestTypeTitle", "TestTypeDescription", "TestTypeFees");
            //DataTable filtered = allApplicationTypes.DefaultView.ToTable(false, "Test", "Title", "Description", "Fees");


            dataGridView1.DataSource = filtered;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
         
        }


        private void CtrlTestTypes_Load(object sender, EventArgs e)
        {
            label1.Text = $"# Records: {dataGridView1.Rows.Count}";

        }
        private void ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 650,
                Height = 550,
                StartPosition = FormStartPosition.CenterScreen

            };
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            form.ShowDialog();
        }
        private void editApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner Test Type.");
                return;
            }
            int TestID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TestTypeID"].Value);
            CtrlEditTestTypes CtrlEditTestTypes = new CtrlEditTestTypes(TestID);
            ShowFormWithControl(CtrlEditTestTypes, "Edit Test Type");

            _RefreshApplicationTypesList();

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
    }
}
