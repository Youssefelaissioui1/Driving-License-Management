namespace DVLD1
{
    partial class CtrlAddUser
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
            this.components = new System.ComponentModel.Container();
            this.lblPersonDetails = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.btnAddPerson = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tb1PersonInfo = new System.Windows.Forms.TabPage();
            this.ctrlShowDetailsPerson3 = new DVLD1.CtrlShowDetailsPerson();
            this.btnNext2 = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.tb2Logininfo = new System.Windows.Forms.TabPage();
            this.ctrlLoginInfo3 = new DVLD1.CtrlLoginInfo();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbltittleNewlocal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.gbFilter.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tb1PersonInfo.SuspendLayout();
            this.tb2Logininfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPersonDetails
            // 
            this.lblPersonDetails.AutoSize = true;
            this.lblPersonDetails.Font = new System.Drawing.Font("Microsoft YaHei", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonDetails.ForeColor = System.Drawing.Color.Red;
            this.lblPersonDetails.Location = new System.Drawing.Point(392, 23);
            this.lblPersonDetails.Name = "lblPersonDetails";
            this.lblPersonDetails.Size = new System.Drawing.Size(251, 42);
            this.lblPersonDetails.TabIndex = 47;
            this.lblPersonDetails.Text = "Add New User";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.tbSearch);
            this.gbFilter.Controls.Add(this.comboBox3);
            this.gbFilter.Controls.Add(this.comboBox2);
            this.gbFilter.Controls.Add(this.btnAddPerson);
            this.gbFilter.Controls.Add(this.btnSearch);
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Location = new System.Drawing.Point(18, 32);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(898, 95);
            this.gbFilter.TabIndex = 59;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(368, 34);
            this.tbSearch.Multiline = true;
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(188, 30);
            this.tbSearch.TabIndex = 60;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "PersonID",
            "National No"});
            this.comboBox3.Location = new System.Drawing.Point(165, 34);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(169, 28);
            this.comboBox3.TabIndex = 49;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "PersonID",
            "Gender",
            "Country",
            "FirstName",
            "LastName",
            "ThirdName",
            "SecondName",
            "Phone",
            "National No",
            "Nationality"});
            this.comboBox2.Location = new System.Drawing.Point(368, 36);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(169, 28);
            this.comboBox2.TabIndex = 40;
            // 
            // btnAddPerson
            // 
            this.btnAddPerson.AutoSize = true;
            this.btnAddPerson.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddPerson.Image = global::DVLD1.Properties.Resources.person_boy__3_;
            this.btnAddPerson.Location = new System.Drawing.Point(756, 8);
            this.btnAddPerson.Name = "btnAddPerson";
            this.btnAddPerson.Size = new System.Drawing.Size(54, 54);
            this.btnAddPerson.TabIndex = 39;
            this.btnAddPerson.UseVisualStyleBackColor = true;
            this.btnAddPerson.Click += new System.EventHandler(this.btnAddPerson_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSearch.Image = global::DVLD1.Properties.Resources.person_boy__4_;
            this.btnSearch.Location = new System.Drawing.Point(641, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(54, 54);
            this.btnSearch.TabIndex = 38;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 29);
            this.label1.TabIndex = 35;
            this.label1.Text = "Filter By";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tb1PersonInfo);
            this.tabControl1.Controls.Add(this.tb2Logininfo);
            this.tabControl1.Location = new System.Drawing.Point(25, 88);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1068, 667);
            this.tabControl1.TabIndex = 60;
            // 
            // tb1PersonInfo
            // 
            this.tb1PersonInfo.BackColor = System.Drawing.Color.White;
            this.tb1PersonInfo.Controls.Add(this.ctrlShowDetailsPerson3);
            this.tb1PersonInfo.Controls.Add(this.btnNext2);
            this.tb1PersonInfo.Controls.Add(this.btnNext);
            this.tb1PersonInfo.Controls.Add(this.gbFilter);
            this.tb1PersonInfo.Location = new System.Drawing.Point(4, 29);
            this.tb1PersonInfo.Name = "tb1PersonInfo";
            this.tb1PersonInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tb1PersonInfo.Size = new System.Drawing.Size(1060, 634);
            this.tb1PersonInfo.TabIndex = 0;
            this.tb1PersonInfo.Text = "Person Info";
            // 
            // ctrlShowDetailsPerson3
            // 
            this.ctrlShowDetailsPerson3.BackColor = System.Drawing.Color.White;
            this.ctrlShowDetailsPerson3.Location = new System.Drawing.Point(18, 152);
            this.ctrlShowDetailsPerson3.Name = "ctrlShowDetailsPerson3";
            this.ctrlShowDetailsPerson3.Size = new System.Drawing.Size(997, 406);
            this.ctrlShowDetailsPerson3.TabIndex = 62;
            // 
            // btnNext2
            // 
            this.btnNext2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext2.Image = global::DVLD1.Properties.Resources.next;
            this.btnNext2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext2.Location = new System.Drawing.Point(727, 564);
            this.btnNext2.Name = "btnNext2";
            this.btnNext2.Size = new System.Drawing.Size(123, 54);
            this.btnNext2.TabIndex = 61;
            this.btnNext2.Text = "Next";
            this.btnNext2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext2.UseVisualStyleBackColor = true;
            this.btnNext2.Click += new System.EventHandler(this.btnNext2_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD1.Properties.Resources.next;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(870, 564);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(123, 54);
            this.btnNext.TabIndex = 60;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tb2Logininfo
            // 
            this.tb2Logininfo.BackColor = System.Drawing.Color.White;
            this.tb2Logininfo.Controls.Add(this.ctrlLoginInfo3);
            this.tb2Logininfo.Location = new System.Drawing.Point(4, 29);
            this.tb2Logininfo.Name = "tb2Logininfo";
            this.tb2Logininfo.Padding = new System.Windows.Forms.Padding(3);
            this.tb2Logininfo.Size = new System.Drawing.Size(1060, 634);
            this.tb2Logininfo.TabIndex = 1;
            this.tb2Logininfo.Text = "Login Info";
            // 
            // ctrlLoginInfo3
            // 
            this.ctrlLoginInfo3.BackColor = System.Drawing.Color.White;
            this.ctrlLoginInfo3.Location = new System.Drawing.Point(21, 26);
            this.ctrlLoginInfo3.Name = "ctrlLoginInfo3";
            this.ctrlLoginInfo3.Size = new System.Drawing.Size(833, 460);
            this.ctrlLoginInfo3.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSave.Image = global::DVLD1.Properties.Resources.diskette;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(649, 775);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(192, 60);
            this.btnSave.TabIndex = 74;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.UseWaitCursor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Image = global::DVLD1.Properties.Resources.close;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(914, 775);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 60);
            this.button1.TabIndex = 73;
            this.button1.Text = "CLose";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // lbltittleNewlocal
            // 
            this.lbltittleNewlocal.AutoSize = true;
            this.lbltittleNewlocal.Font = new System.Drawing.Font("Microsoft YaHei", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltittleNewlocal.ForeColor = System.Drawing.Color.Red;
            this.lbltittleNewlocal.Location = new System.Drawing.Point(233, 23);
            this.lbltittleNewlocal.Name = "lbltittleNewlocal";
            this.lbltittleNewlocal.Size = new System.Drawing.Size(633, 42);
            this.lbltittleNewlocal.TabIndex = 75;
            this.lbltittleNewlocal.Text = "New Local Driving License Application";
            // 
            // CtrlAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbltittleNewlocal);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblPersonDetails);
            this.Name = "CtrlAddUser";
            this.Size = new System.Drawing.Size(1326, 977);
            this.Load += new System.EventHandler(this.CtrlAddUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tb1PersonInfo.ResumeLayout(false);
            this.tb2Logininfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPersonDetails;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private CtrlShowDetailsPerson ctrlShowDetailsPerson1;
        private CtrlLoginInfo ctrlLoginInfo1;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btnAddPerson;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private CtrlShowDetailsPerson ctrlShowDetailsPerson2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tb1PersonInfo;
        private System.Windows.Forms.TabPage tb2Logininfo;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button button1;
        private CtrlLoginInfo ctrlLoginInfo2;
        private System.Windows.Forms.Button btnNext2;
        private CtrlShowDetailsPerson ctrlShowDetailsPerson3;
        private CtrlLoginInfo ctrlLoginInfo3;
        private System.Windows.Forms.Label lbltittleNewlocal;
    }
}
