
using System;
using System.Data;
using DVLDDataAccessLayer;


namespace DVLDBusinessLayer
{
    public class People
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }

        public int Gender { set; get; }

        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }
        public string ImagePath { set; get; }



        public People()

        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Gender =-1;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID =-1;
            this.ImagePath = "";



            Mode = enMode.AddNew;

        }

        private People(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
             DateTime DateOfBirth, int Gender, string Address,
             string Phone, string Email, int NationalityCountryID, string ImagePath)

        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;

            Mode = enMode.Update;

        }
        public string FullName()
        {
            return this.FirstName + " " + this.SecondName + " " + this.ThirdName + " " + this.LastName;
        }
        private bool _AddNewPerson()
        {
            //call DataAccess Layer 

            this.PersonID = DVLDDataAccessLayer.People.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender,this.Address,this.Phone,this.Email,this.NationalityCountryID, this.ImagePath);

            return (this.PersonID != -1);
        }


        private bool _UpdatePerson()
        {
            //call DataAccess Layer 

            return DVLDDataAccessLayer.People.UpdatePerson(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

        }

        public static People Find(int PersonID)
        {
            string NationalNo = "", FirstName = "",SecondName = "",ThirdName = "",LastName = "",
            Address = "",
            Phone = "",
            Email = "",
            ImagePath = "";
            int Gender = -1, NationalityCountryID = -1;
           DateTime DateOfBirth = DateTime.Now;
          

            if (DVLDDataAccessLayer.People.GetInfoByPersonID( PersonID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
             ref DateOfBirth, ref Gender, ref Address,
             ref  Phone, ref Email, ref NationalityCountryID, ref ImagePath))

                return new People(PersonID,  NationalNo,  FirstName,  SecondName,  ThirdName,  LastName,
              DateOfBirth,  Gender,  Address,
              Phone,  Email,  NationalityCountryID,  ImagePath);
            else
                return null;

        }


        public static People FindByNationalNo(string NationalNo)
        {
            string  FirstName = "", SecondName = "", ThirdName = "", LastName = "",
            Address = "",
            Phone = "",
            Email = "",
            ImagePath = "";
            int Gender = -1, NationalityCountryID = -1,PersonID=-1;
            DateTime DateOfBirth = DateTime.Now;


            if (DVLDDataAccessLayer.People.GetInfoByPersonNationalNo(ref PersonID,  NationalNo, ref FirstName, ref SecondName, ref ThirdName, ref LastName,
             ref DateOfBirth, ref Gender, ref Address,
             ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))

                return new People(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,
              DateOfBirth, Gender, Address,
              Phone, Email, NationalityCountryID, ImagePath);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdatePerson();

            }




            return false;
        }

        public static DataTable GetAllPeople()
        {
            return DVLDDataAccessLayer.People.GetAllPeople();

        }

        public static bool DeletePerson(int ID)
        {
            try
            {
                // appel réel à la suppression dans la DAL
                DVLDDataAccessLayer.People.DeletePerson(ID);
                return true; // si pas d'exception, succès
            }
            catch
            {
                return false; // en cas d'erreur, échec
            }
        }

        public static bool isPersonExist(int ID)
        {
            return DVLDDataAccessLayer.People.IsPersonExist(ID);
        }


    }
}
