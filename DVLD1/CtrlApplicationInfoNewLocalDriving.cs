using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLDBusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD1
{
    public partial class CtrlApplicationInfoNewLocalDriving : UserControl
    {
        public enum enMode { AddNew = 0, Update = 1 };

        private enMode _Mode;
        private User _user;
        private int _PersonID;
        private People currentPerson; // Toujours utiliser currentPerson pour référence cohérente

        private LocalDrivingLicenseApplication _LocalDrivingLicenseApplication ;
        private CtrlApplicationInfoNewLocalDriving _ctrlApplicationInfoNewLocalDriving;
        private UsersForm _UsersForm;
        public Applications application;
        static int selectedID;

        public CtrlApplicationInfoNewLocalDriving()
        {
            InitializeComponent();
            currentPerson = new People();
            _Mode = enMode.AddNew;
            _PersonID = -1;
        }

        public CtrlApplicationInfoNewLocalDriving(People person) : this()  // Appel du constructeur par défaut pour DRY
        {
            //_PersonID = PersonID;

            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;
                currentPerson = new People();
            }
            else
            {
                _Mode = enMode.Update;
                currentPerson = person;
                if (currentPerson == null)
                {
                    MessageBox.Show($"Person avec ID {_PersonID} non trouvé.");
                    currentPerson = new People();
                    _Mode = enMode.AddNew;
                }
            }
        }
        private void LoadLicenseClasses()
        {
            DataTable dt = LicenseClasses.GetAllLicenseClasses();

            cbxLicenseClass.DataSource = dt;
            cbxLicenseClass.DisplayMember = "ClassName";
            cbxLicenseClass.ValueMember = "LicenseClassID"; // très important !
            cbxLicenseClass.SelectedIndex = 6;
        }


        private void CtrlApplicationInfoNewLocalDriving_Load(object sender, EventArgs e)
        {
            lblCreatedby.Text = GlobalUser.CurrentUser.UserName;
            lblApplicationDate.Text = DateTime.Now.ToString();
            lblApplicationFees.Text = "15";
         
            LoadLicenseClasses();


      
            if (cbxLicenseClass.SelectedValue == null)
              {  MessageBox.Show("SelectedValue est null au chargement.");
                return;
            }
        }

        private void lblCreatedby_Click(object sender, EventArgs e)
        {

        }

        public Applications AddApplication(People person)
        {
            // Validation de la personne
            if (person == null)
            {
                MessageBox.Show("Aucune personne sélectionnée.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            // Validation de l'utilisateur courant
            if (GlobalUser.CurrentUser == null)
            {
                MessageBox.Show("Aucun utilisateur connecté.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }



           

            // Création de l'application si nécessaire
            if (application == null)
                application = new Applications();

            // Remplissage des champs de l’application
            application.ApplicationDate = DateTime.Now.Date;
            application.ApplicantPersonID = person.PersonID;
       

            application.PaidFees = 15;
            application.ApplicationStatus = 1; // En attente ou par défaut
            application.CreatedByUserID = GlobalUser.CurrentUser.UserID;
            application.ApplicationTypeID =1;

            LicenseClasses license1 = LicenseClasses.Find(selectedID);

            if (LocalDrivingLicenseApplication.IsExistLocalDrivingLicenseApplicationsInfoByNationalNo_ClassNameAnd_Status(person.NationalNo,license1.ClassName))
            {
                MessageBox.Show("Une demande avec ces informations existe déjà.", "Duplication", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            // Enregistrement de la nouvelle demande
            if (application._AddNewApplications())
            {
                MessageBox.Show("Nouvel ID : " + application.ApplicationID.ToString());
                lblApplicationsID.Text = application.ApplicationID.ToString();
                MessageBox.Show("Demande ajoutée avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LocalDrivingLicenseApplication localDrivingLicenseApplication = new LocalDrivingLicenseApplication();
                localDrivingLicenseApplication.ApplicationID = application.ApplicationID;
                localDrivingLicenseApplication.LicenseClassID = selectedID;
                LocalDrivingLicenseApplication localDrivingLicenseApplication1 = new LocalDrivingLicenseApplication();
                localDrivingLicenseApplication1  = LocalDrivingLicenseApplication.FindByNationalNo_AND_Class(person.NationalNo,license1.ClassName);

                if (localDrivingLicenseApplication1 != null)
                {
                    MessageBox.Show("Une demande avec ces informations existe déjà avec id ."+ localDrivingLicenseApplication1.LocalDrivingLicenseApplicationID +"\navec statut "+localDrivingLicenseApplication1.Status, "Duplication", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }
                _Mode = enMode.AddNew;
                localDrivingLicenseApplication.Save();
                MessageBox.Show("New local driving license has ID : " + localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
                return application;

            }
            else
            {
                MessageBox.Show("Échec lors de l'ajout de la demande.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void lblApplicationsID_Click(object sender, EventArgs e)
        {

        }

        private void cbxLicenseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLicenseClass.SelectedValue != null && cbxLicenseClass.SelectedValue is int)
            {
                selectedID = Convert.ToInt32(cbxLicenseClass.SelectedValue);
                //MessageBox.Show("Selected ID: " + selectedID);
            }
            //else
            //{
            //    MessageBox.Show("SelectedValue est null ou invalide !");
            //}
        }
    }
}
