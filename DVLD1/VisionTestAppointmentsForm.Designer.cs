namespace DVLD1
{
    partial class VisionTestAppointmentsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTestTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TakeTesttoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlTestAppointmentInfo1 = new DVLD1.ctrlTestAppointmentInfo();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 599);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 25);
            this.label3.TabIndex = 50;
            this.label3.Text = "Appointments:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 641);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(925, 145);
            this.textBox1.TabIndex = 51;
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblRecord.Location = new System.Drawing.Point(18, 822);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(135, 26);
            this.lblRecord.TabIndex = 60;
            this.lblRecord.Text = "# Records: 0";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTestTypesToolStripMenuItem,
            this.TakeTesttoolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 72);
            // 
            // editTestTypesToolStripMenuItem
            // 
            this.editTestTypesToolStripMenuItem.Name = "editTestTypesToolStripMenuItem";
            this.editTestTypesToolStripMenuItem.Size = new System.Drawing.Size(160, 34);
            this.editTestTypesToolStripMenuItem.Text = "Edit ";
            this.editTestTypesToolStripMenuItem.Click += new System.EventHandler(this.editTestTypesToolStripMenuItem_Click);
            // 
            // TakeTesttoolStripMenuItem1
            // 
            this.TakeTesttoolStripMenuItem1.Name = "TakeTesttoolStripMenuItem1";
            this.TakeTesttoolStripMenuItem1.Size = new System.Drawing.Size(160, 34);
            this.TakeTesttoolStripMenuItem1.Text = "Take Test";
            this.TakeTesttoolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Image = global::DVLD1.Properties.Resources.field_date;
            this.linkLabel1.Location = new System.Drawing.Point(813, 586);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(97, 40);
            this.linkLabel1.TabIndex = 77;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "        ";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(745, 807);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(192, 60);
            this.btnClose.TabIndex = 61;
            this.btnClose.Text = "CLose";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlTestAppointmentInfo1
            // 
            this.ctrlTestAppointmentInfo1.AutoSize = true;
            this.ctrlTestAppointmentInfo1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ctrlTestAppointmentInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlTestAppointmentInfo1.Location = new System.Drawing.Point(2, 4);
            this.ctrlTestAppointmentInfo1.Name = "ctrlTestAppointmentInfo1";
            this.ctrlTestAppointmentInfo1.Size = new System.Drawing.Size(955, 579);
            this.ctrlTestAppointmentInfo1.TabIndex = 79;
            this.ctrlTestAppointmentInfo1.Load += new System.EventHandler(this.ctrlTestAppointmentInfo1_Load);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 660);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(754, 99);
            this.dataGridView1.TabIndex = 78;
            // 
            // VisionTestAppointmentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1003, 1014);
            this.Controls.Add(this.ctrlTestAppointmentInfo1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VisionTestAppointmentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisionTestAppointmentsForm";
            this.Load += new System.EventHandler(this.VisionTestAppointmentsForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editTestTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TakeTesttoolStripMenuItem1;
        private ctrlTestAppointmentInfo ctrlTestAppointmentInfo1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}