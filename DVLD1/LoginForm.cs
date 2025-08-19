using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;

namespace DVLD1
{
    public partial class LoginForm : Form
    {
        User _user;
        public LoginForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen; // 👈 Affiche le form au centre de l'écran


        }






        private void lblClose_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close(); // Close the whole form

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _user = User.FindUserlogin(txtbPassword.Text, txtbUserName.Text);

            if (_user != null)
            {
                GlobalUser.SetCurrentUser(_user); // 🔥 voilà l'utilisateur global

                // ✅ Sauvegarder ou effacer les identifiants selon la case Remember Me
                if (chbxRememberMe.Checked)
                {
                    LoginSettings.SaveCredentials(txtbUserName.Text, txtbPassword.Text);
                }
                else
                {
                    LoginSettings.ClearCredentials();
                }

                MessageBox.Show("Connexion réussie !");

                // Ouvrir le formulaire principal
                HomeForm mainForm = new HomeForm();
                this.Hide();
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.","Wrong Crendintials",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        private void chbxRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            var credentials = LoginSettings.LoadCredentials();

            if (credentials.HasValue)
            {
                txtbUserName.Text = credentials.Value.username;
                txtbPassword.Text = credentials.Value.password;
                chbxRememberMe.Checked = true;
            }
        }

        private void lblClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclose1_Click(object sender, EventArgs e)
        {
            this.FindForm()?.Close();
        }
    }
}
