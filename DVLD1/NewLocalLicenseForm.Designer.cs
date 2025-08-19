namespace DVLD1
{
    partial class NewLocalLicenseForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ctrlAddUser1 = new DVLD1.CtrlAddUser();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Image = global::DVLD1.Properties.Resources.diskette;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(651, 776);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(192, 60);
            this.button1.TabIndex = 88;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.Image = global::DVLD1.Properties.Resources.close;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(881, 776);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(192, 60);
            this.button3.TabIndex = 87;
            this.button3.Text = "CLose";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.UseWaitCursor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ctrlAddUser1
            // 
            this.ctrlAddUser1.BackColor = System.Drawing.Color.White;
            this.ctrlAddUser1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctrlAddUser1.Location = new System.Drawing.Point(-5, 0);
            this.ctrlAddUser1.Name = "ctrlAddUser1";
            this.ctrlAddUser1.Size = new System.Drawing.Size(1109, 937);
            this.ctrlAddUser1.TabIndex = 0;
            this.ctrlAddUser1.Load += new System.EventHandler(this.ctrlAddUser1_Load);
            // 
            // NewLocalLicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 860);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ctrlAddUser1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewLocalLicenseForm";
            this.Text = "NewLocalLicenseForm";
            this.Load += new System.EventHandler(this.NewLocalLicenseForm_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private CtrlAddUser ctrlAddUser1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}