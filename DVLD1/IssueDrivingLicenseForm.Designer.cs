namespace DVLD1
{
    partial class IssueDrivingLicenseForm
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
            this.ctrlTestAppointmentInfo1 = new DVLD1.ctrlTestAppointmentInfo();
            this.tbxNotes = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnIssue = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlTestAppointmentInfo1
            // 
            this.ctrlTestAppointmentInfo1.AutoSize = true;
            this.ctrlTestAppointmentInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlTestAppointmentInfo1.Location = new System.Drawing.Point(-1, 0);
            this.ctrlTestAppointmentInfo1.Name = "ctrlTestAppointmentInfo1";
            this.ctrlTestAppointmentInfo1.Size = new System.Drawing.Size(955, 579);
            this.ctrlTestAppointmentInfo1.TabIndex = 0;
            // 
            // tbxNotes
            // 
            this.tbxNotes.BackColor = System.Drawing.Color.White;
            this.tbxNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxNotes.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbxNotes.Location = new System.Drawing.Point(180, 603);
            this.tbxNotes.Multiline = true;
            this.tbxNotes.Name = "tbxNotes";
            this.tbxNotes.Size = new System.Drawing.Size(774, 147);
            this.tbxNotes.TabIndex = 106;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 595);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 25);
            this.label6.TabIndex = 105;
            this.label6.Text = "Notes:";
            // 
            // btnIssue
            // 
            this.btnIssue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnIssue.Image = global::DVLD1.Properties.Resources.id_help__2_;
            this.btnIssue.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssue.Location = new System.Drawing.Point(765, 756);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(192, 60);
            this.btnIssue.TabIndex = 107;
            this.btnIssue.Text = "Issue";
            this.btnIssue.UseVisualStyleBackColor = false;
            this.btnIssue.UseWaitCursor = true;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Image = global::DVLD1.Properties.Resources.close;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(567, 756);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 60);
            this.button1.TabIndex = 108;
            this.button1.Text = "CLose";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD1.Properties.Resources.notes;
            this.pictureBox5.Location = new System.Drawing.Point(129, 595);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(38, 34);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 104;
            this.pictureBox5.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(279, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 42);
            this.label1.TabIndex = 109;
            this.label1.Text = "Issue Driving License";
            // 
            // IssueDrivingLicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(975, 851);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIssue);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbxNotes);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.ctrlTestAppointmentInfo1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IssueDrivingLicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IssueDrivingLicenseForm";
            this.Load += new System.EventHandler(this.IssueDrivingLicenseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlTestAppointmentInfo ctrlTestAppointmentInfo1;
        private System.Windows.Forms.TextBox tbxNotes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}