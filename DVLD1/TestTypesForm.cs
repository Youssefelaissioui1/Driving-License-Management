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
    public partial class TestTypesForm : Form
    {
        public TestTypesForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // 👈 Affiche le form au centre de l'écran
            dataGridView1.CellMouseClick += dataGridView1_CellMouseClick;
            _RefreshApplicationTypesList();

        }

        private void ctrlTestTypes1_Load(object sender, EventArgs e)
        {

        }

        public void _RefreshApplicationTypesList()
        {
            DataTable allTestTypes = TestTypes.GetTestTypes();
            DataTable filtered = allTestTypes.DefaultView.ToTable(false, "TestTypeID", "TestTypeTitle", "TestTypeDescription", "TestTypeFees");


            dataGridView1.DataSource = filtered;
            dataGridView1.Columns["TestTypeID"].HeaderText = "ID";
            dataGridView1.Columns["TestTypeTitle"].HeaderText = "Title";
            dataGridView1.Columns["TestTypeDescription"].HeaderText = "Description";
            dataGridView1.Columns["TestTypeFees"].HeaderText = "Fees";
        }
private void TestTypesForm_Load(object sender, EventArgs e)
        {
            label1.Text = $"# Records: {dataGridView1.Rows.Count}";


        }
        private void ShowFormWithControl(UserControl control, string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 550,
                Height = 450,
                StartPosition = FormStartPosition.CenterScreen

            };
            control.Dock = DockStyle.Fill;

            form.Controls.Add(control);
            form.ShowDialog();
        }
        private void editTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner Test Type.");
                return;
            }
            int TestTypeID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TestTypeID"].Value);
            CtrlEditTestTypes ctrlEditTestTypes = new CtrlEditTestTypes(TestTypeID);
            ShowFormWithControl(ctrlEditTestTypes, "Edit Test Type");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
