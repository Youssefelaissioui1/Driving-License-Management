namespace DVLD1
{
    partial class ctrlShearchDrivingLicense
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbfiltrer = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlDrivingLicenseInfo1 = new DVLD1.CtrlDrivingLicenseInfo();
            this.gbfiltrer.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbfiltrer
            // 
            this.gbfiltrer.Controls.Add(this.button1);
            this.gbfiltrer.Controls.Add(this.textBox1);
            this.gbfiltrer.Controls.Add(this.label1);
            this.gbfiltrer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbfiltrer.Location = new System.Drawing.Point(8, 17);
            this.gbfiltrer.Name = "gbfiltrer";
            this.gbfiltrer.Size = new System.Drawing.Size(793, 100);
            this.gbfiltrer.TabIndex = 1;
            this.gbfiltrer.TabStop = false;
            this.gbfiltrer.Text = "Fiter";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.Image = global::DVLD1.Properties.Resources.id__3_;
            this.button1.Location = new System.Drawing.Point(571, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 42);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(191, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(346, 30);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "License ID :";
            // 
            // ctrlDrivingLicenseInfo1
            // 
            this.ctrlDrivingLicenseInfo1.BackColor = System.Drawing.Color.White;
            this.ctrlDrivingLicenseInfo1.Location = new System.Drawing.Point(8, 123);
            this.ctrlDrivingLicenseInfo1.Name = "ctrlDrivingLicenseInfo1";
            this.ctrlDrivingLicenseInfo1.Size = new System.Drawing.Size(1206, 422);
            this.ctrlDrivingLicenseInfo1.TabIndex = 2;
            // 
            // ctrlShearchDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ctrlDrivingLicenseInfo1);
            this.Controls.Add(this.gbfiltrer);
            this.Name = "ctrlShearchDrivingLicense";
            this.Size = new System.Drawing.Size(1217, 553);
            this.Load += new System.EventHandler(this.ctrlShearchDrivingLicense_Load);
            this.gbfiltrer.ResumeLayout(false);
            this.gbfiltrer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbfiltrer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private CtrlDrivingLicenseInfo ctrlDrivingLicenseInfo1;
    }
}
