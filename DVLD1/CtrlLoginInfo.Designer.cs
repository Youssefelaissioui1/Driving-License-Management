namespace DVLD1
{
    partial class CtrlLoginInfo
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
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panelContainer = new System.Windows.Forms.Panel();
            this.ckbIsActive = new System.Windows.Forms.CheckBox();
            this.lblUserID = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbConfirmPassword = new System.Windows.Forms.TextBox();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.ckbIsActive);
            this.panelContainer.Controls.Add(this.lblUserID);
            this.panelContainer.Controls.Add(this.tbPassword);
            this.panelContainer.Controls.Add(this.tbConfirmPassword);
            this.panelContainer.Controls.Add(this.tbUserName);
            this.panelContainer.Controls.Add(this.label5);
            this.panelContainer.Controls.Add(this.label4);
            this.panelContainer.Controls.Add(this.label3);
            this.panelContainer.Controls.Add(this.label2);
            this.panelContainer.Controls.Add(this.pictureBox12);
            this.panelContainer.Controls.Add(this.pictureBox11);
            this.panelContainer.Controls.Add(this.pictureBox10);
            this.panelContainer.Controls.Add(this.pictureBox9);
            this.panelContainer.Location = new System.Drawing.Point(17, 3);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(726, 561);
            this.panelContainer.TabIndex = 0;
            // 
            // ckbIsActive
            // 
            this.ckbIsActive.AutoSize = true;
            this.ckbIsActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ckbIsActive.Location = new System.Drawing.Point(276, 360);
            this.ckbIsActive.Name = "ckbIsActive";
            this.ckbIsActive.Size = new System.Drawing.Size(151, 33);
            this.ckbIsActive.TabIndex = 70;
            this.ckbIsActive.Text = "    Is Active";
            this.ckbIsActive.UseVisualStyleBackColor = true;
            this.ckbIsActive.UseWaitCursor = true;
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblUserID.Location = new System.Drawing.Point(337, 83);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(49, 29);
            this.lblUserID.TabIndex = 69;
            this.lblUserID.Text = "???";
            this.lblUserID.UseWaitCursor = true;
            this.lblUserID.Click += new System.EventHandler(this.lblUserID_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(330, 222);
            this.tbPassword.Multiline = true;
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(241, 38);
            this.tbPassword.TabIndex = 68;
            this.tbPassword.UseWaitCursor = true;
            this.tbPassword.Validating += new System.ComponentModel.CancelEventHandler(this.tbPassword_Validating_1);
            // 
            // tbConfirmPassword
            // 
            this.tbConfirmPassword.Location = new System.Drawing.Point(330, 282);
            this.tbConfirmPassword.Multiline = true;
            this.tbConfirmPassword.Name = "tbConfirmPassword";
            this.tbConfirmPassword.PasswordChar = '*';
            this.tbConfirmPassword.Size = new System.Drawing.Size(241, 38);
            this.tbConfirmPassword.TabIndex = 67;
            this.tbConfirmPassword.UseWaitCursor = true;
            this.tbConfirmPassword.Validating += new System.ComponentModel.CancelEventHandler(this.tbConfirmPassword_Validating_1);
            // 
            // tbUserName
            // 
            this.tbUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbUserName.Location = new System.Drawing.Point(333, 154);
            this.tbUserName.Multiline = true;
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(241, 38);
            this.tbUserName.TabIndex = 58;
            this.tbUserName.UseWaitCursor = true;
            this.tbUserName.Validating += new System.ComponentModel.CancelEventHandler(this.tbUserName_Validating_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(37, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 29);
            this.label5.TabIndex = 62;
            this.label5.Text = "Confirm Password";
            this.label5.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(127, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 29);
            this.label4.TabIndex = 61;
            this.label4.Text = "Password";
            this.label4.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(127, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 29);
            this.label3.TabIndex = 60;
            this.label3.Text = "UserID";
            this.label3.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(127, 154);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 29);
            this.label2.TabIndex = 59;
            this.label2.Text = "UserName";
            this.label2.UseWaitCursor = true;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = global::DVLD1.Properties.Resources.key;
            this.pictureBox12.Location = new System.Drawing.Point(272, 279);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(32, 32);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox12.TabIndex = 66;
            this.pictureBox12.TabStop = false;
            this.pictureBox12.UseWaitCursor = true;
            // 
            // pictureBox11
            // 
            this.pictureBox11.Image = global::DVLD1.Properties.Resources.key;
            this.pictureBox11.Location = new System.Drawing.Point(272, 222);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(32, 32);
            this.pictureBox11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox11.TabIndex = 65;
            this.pictureBox11.TabStop = false;
            this.pictureBox11.UseWaitCursor = true;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = global::DVLD1.Properties.Resources.man;
            this.pictureBox10.Location = new System.Drawing.Point(272, 80);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(32, 32);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox10.TabIndex = 64;
            this.pictureBox10.TabStop = false;
            this.pictureBox10.UseWaitCursor = true;
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = global::DVLD1.Properties.Resources.man;
            this.pictureBox9.Location = new System.Drawing.Point(272, 154);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(32, 32);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox9.TabIndex = 63;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.UseWaitCursor = true;
            // 
            // CtrlLoginInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelContainer);
            this.Name = "CtrlLoginInfo";
            this.Size = new System.Drawing.Size(1142, 649);
            this.Load += new System.EventHandler(this.CtrlLoginInfo_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.CheckBox ckbIsActive;
        private System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbConfirmPassword;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox9;
    }
}
