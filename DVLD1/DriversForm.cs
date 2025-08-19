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
    public partial class DriversForm : Form
    {
        public DriversForm()
        {
            InitializeComponent();
        }

    


 
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }

        private void _RefreshList()
        {
            DataTable allPeople = Driver.GetAllDrivers();
            DataTable filtered = allPeople.DefaultView.ToTable(false, "DriverID", "PersonID", "NationalNO", 
                "FullName", "CreatedDate", "NumberOfActiveLicenses");

            dataGridView1.DataSource = filtered;
            lblRecord.Text = $"# Records: {dataGridView1.Rows.Count}";

        }

        private void DriversForm_Load(object sender, EventArgs e)
        {
            _RefreshList();
            comboBox1.SelectedIndex = 0; 
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
      { "DriverID", "DriverID" },
     { "PersonID", "PersonID" },
{ "NationalNO", "NationalNO" },
{ "FullName", "FullName" }


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

            if (columnName == "PersonID" || columnName=="DriverID")
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
            ApplyFilter();

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            ApplyFilter();

        }
    }
}
