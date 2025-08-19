namespace DVLD1
{
    partial class DetainLicenseForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.linklblShowLicensesHistory = new System.Windows.Forms.LinkLabel();
            this.btnDetain = new System.Windows.Forms.Button();
            this.linkLblShowLicensesInfo = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.gbDetainInfo = new System.Windows.Forms.GroupBox();
            this.lblLicenseID = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label00 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblDetainDate = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDetainID = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblLRApplication = new System.Windows.Forms.Label();
            this.lblCreatedBy = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.l = new System.Windows.Forms.Label();
            this.tbxFineFees = new System.Windows.Forms.TextBox();
            this.ctrlShearchDrivingLicense1 = new DVLD1.ctrlShearchDrivingLicense();
            this.gbDetainInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(444, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 42);
            this.label1.TabIndex = 1;
            this.label1.Text = "Detain License";
            // 
            // linklblShowLicensesHistory
            // 
            this.linklblShowLicensesHistory.AutoSize = true;
            this.linklblShowLicensesHistory.Enabled = false;
            this.linklblShowLicensesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linklblShowLicensesHistory.Location = new System.Drawing.Point(83, 850);
            this.linklblShowLicensesHistory.Name = "linklblShowLicensesHistory";
            this.linklblShowLicensesHistory.Size = new System.Drawing.Size(210, 25);
            this.linklblShowLicensesHistory.TabIndex = 118;
            this.linklblShowLicensesHistory.TabStop = true;
            this.linklblShowLicensesHistory.Text = "Show Licenses History";
            this.linklblShowLicensesHistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblShowLicensesHistory_LinkClicked);
            // 
            // btnDetain
            // 
            this.btnDetain.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDetain.Image = global::DVLD1.Properties.Resources.world;
            this.btnDetain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetain.Location = new System.Drawing.Point(946, 850);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(183, 60);
            this.btnDetain.TabIndex = 120;
            this.btnDetain.Text = "Detain";
            this.btnDetain.UseVisualStyleBackColor = false;
            this.btnDetain.UseWaitCursor = true;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // linkLblShowLicensesInfo
            // 
            this.linkLblShowLicensesInfo.AutoSize = true;
            this.linkLblShowLicensesInfo.Enabled = false;
            this.linkLblShowLicensesInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblShowLicensesInfo.Location = new System.Drawing.Point(342, 850);
            this.linkLblShowLicensesInfo.Name = "linkLblShowLicensesInfo";
            this.linkLblShowLicensesInfo.Size = new System.Drawing.Size(182, 25);
            this.linkLblShowLicensesInfo.TabIndex = 119;
            this.linkLblShowLicensesInfo.TabStop = true;
            this.linkLblShowLicensesInfo.Text = "Show Licenses Info";
            this.linkLblShowLicensesInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblShowLicensesInfo_LinkClicked);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Image = global::DVLD1.Properties.Resources.close;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(748, 850);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 60);
            this.button1.TabIndex = 121;
            this.button1.Text = "CLose";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbDetainInfo
            // 
            this.gbDetainInfo.Controls.Add(this.tbxFineFees);
            this.gbDetainInfo.Controls.Add(this.lblLicenseID);
            this.gbDetainInfo.Controls.Add(this.pictureBox5);
            this.gbDetainInfo.Controls.Add(this.label00);
            this.gbDetainInfo.Controls.Add(this.pictureBox4);
            this.gbDetainInfo.Controls.Add(this.label8);
            this.gbDetainInfo.Controls.Add(this.lblDetainDate);
            this.gbDetainInfo.Controls.Add(this.pictureBox2);
            this.gbDetainInfo.Controls.Add(this.label4);
            this.gbDetainInfo.Controls.Add(this.lblDetainID);
            this.gbDetainInfo.Controls.Add(this.pictureBox1);
            this.gbDetainInfo.Controls.Add(this.lblLRApplication);
            this.gbDetainInfo.Controls.Add(this.lblCreatedBy);
            this.gbDetainInfo.Controls.Add(this.pictureBox8);
            this.gbDetainInfo.Controls.Add(this.l);
            this.gbDetainInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDetainInfo.Location = new System.Drawing.Point(14, 645);
            this.gbDetainInfo.Name = "gbDetainInfo";
            this.gbDetainInfo.Size = new System.Drawing.Size(1186, 182);
            this.gbDetainInfo.TabIndex = 117;
            this.gbDetainInfo.TabStop = false;
            this.gbDetainInfo.Text = "Detain Info";
            // 
            // lblLicenseID
            // 
            this.lblLicenseID.AutoSize = true;
            this.lblLicenseID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicenseID.Location = new System.Drawing.Point(858, 35);
            this.lblLicenseID.Name = "lblLicenseID";
            this.lblLicenseID.Size = new System.Drawing.Size(40, 22);
            this.lblLicenseID.TabIndex = 84;
            this.lblLicenseID.Text = "???";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::DVLD1.Properties.Resources.certificate;
            this.pictureBox5.Location = new System.Drawing.Point(764, 30);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(38, 34);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 83;
            this.pictureBox5.TabStop = false;
            // 
            // label00
            // 
            this.label00.AutoSize = true;
            this.label00.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label00.Location = new System.Drawing.Point(552, 35);
            this.label00.Name = "label00";
            this.label00.Size = new System.Drawing.Size(133, 25);
            this.label00.TabIndex = 82;
            this.label00.Text = " License ID :";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::DVLD1.Properties.Resources.money;
            this.pictureBox4.Location = new System.Drawing.Point(188, 133);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(38, 34);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 80;
            this.pictureBox4.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(44, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(121, 25);
            this.label8.TabIndex = 79;
            this.label8.Text = "Fine Fees :";
            // 
            // lblDetainDate
            // 
            this.lblDetainDate.AutoSize = true;
            this.lblDetainDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainDate.Location = new System.Drawing.Point(253, 90);
            this.lblDetainDate.Name = "lblDetainDate";
            this.lblDetainDate.Size = new System.Drawing.Size(40, 22);
            this.lblDetainDate.TabIndex = 75;
            this.lblDetainDate.Text = "???";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD1.Properties.Resources.calendar;
            this.pictureBox2.Location = new System.Drawing.Point(187, 84);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 74;
            this.pictureBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 25);
            this.label4.TabIndex = 73;
            this.label4.Text = "Detain Date :";
            // 
            // lblDetainID
            // 
            this.lblDetainID.AutoSize = true;
            this.lblDetainID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDetainID.Location = new System.Drawing.Point(253, 41);
            this.lblDetainID.Name = "lblDetainID";
            this.lblDetainID.Size = new System.Drawing.Size(40, 22);
            this.lblDetainID.TabIndex = 72;
            this.lblDetainID.Text = "???";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD1.Properties.Resources.id;
            this.pictureBox1.Location = new System.Drawing.Point(187, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 71;
            this.pictureBox1.TabStop = false;
            // 
            // lblLRApplication
            // 
            this.lblLRApplication.AutoSize = true;
            this.lblLRApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLRApplication.Location = new System.Drawing.Point(51, 39);
            this.lblLRApplication.Name = "lblLRApplication";
            this.lblLRApplication.Size = new System.Drawing.Size(114, 25);
            this.lblLRApplication.TabIndex = 70;
            this.lblLRApplication.Text = "Detain ID :";
            // 
            // lblCreatedBy
            // 
            this.lblCreatedBy.AutoSize = true;
            this.lblCreatedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCreatedBy.Location = new System.Drawing.Point(858, 83);
            this.lblCreatedBy.Name = "lblCreatedBy";
            this.lblCreatedBy.Size = new System.Drawing.Size(40, 22);
            this.lblCreatedBy.TabIndex = 66;
            this.lblCreatedBy.Text = "???";
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::DVLD1.Properties.Resources.calendar;
            this.pictureBox8.Location = new System.Drawing.Point(764, 77);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(38, 34);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 65;
            this.pictureBox8.TabStop = false;
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.l.Location = new System.Drawing.Point(552, 82);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(133, 25);
            this.l.TabIndex = 64;
            this.l.Text = "Created By :";
            // 
            // tbxFineFees
            // 
            this.tbxFineFees.Location = new System.Drawing.Point(257, 137);
            this.tbxFineFees.Name = "tbxFineFees";
            this.tbxFineFees.Size = new System.Drawing.Size(197, 30);
            this.tbxFineFees.TabIndex = 85;
            // 
            // ctrlShearchDrivingLicense1
            // 
            this.ctrlShearchDrivingLicense1.AutoSize = true;
            this.ctrlShearchDrivingLicense1.BackColor = System.Drawing.Color.White;
            this.ctrlShearchDrivingLicense1.Location = new System.Drawing.Point(1, 92);
            this.ctrlShearchDrivingLicense1.Name = "ctrlShearchDrivingLicense1";
            this.ctrlShearchDrivingLicense1.Size = new System.Drawing.Size(1217, 548);
            this.ctrlShearchDrivingLicense1.TabIndex = 0;
            // 
            // DetainLicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1324, 973);
            this.Controls.Add(this.linklblShowLicensesHistory);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.linkLblShowLicensesInfo);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gbDetainInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlShearchDrivingLicense1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetainLicenseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DetainLicenseForm";
            this.Load += new System.EventHandler(this.DetainLicenseForm_Load);
            this.gbDetainInfo.ResumeLayout(false);
            this.gbDetainInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlShearchDrivingLicense ctrlShearchDrivingLicense1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linklblShowLicensesHistory;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.LinkLabel linkLblShowLicensesInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbDetainInfo;
        private System.Windows.Forms.Label lblLicenseID;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label00;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblDetainDate;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDetainID;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblLRApplication;
        private System.Windows.Forms.Label lblCreatedBy;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.TextBox tbxFineFees;
    }
}