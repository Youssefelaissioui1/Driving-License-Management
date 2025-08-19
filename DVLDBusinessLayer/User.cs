
using System;
using System.Data;
using DVLDDataAccessLayer;


namespace DVLDBusinessLayer
{
    public class User
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }

        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string PassWord { set; get; }
        public bool IsActive { set; get; }
    



        public User()

        {
            this.UserID = -1;
            this.PersonID = -1;
            this.UserName = "";
            this.PassWord = "";
            this.IsActive = false;
        



            Mode = enMode.AddNew;

        }

        private User(int UserID,int PersonID, string UserName, string Password,  bool IsActive)

        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.PassWord = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;

        }

        private bool _AddNewUser()
        {
            //call DataAccess Layer 

            this.UserID = DVLDDataAccessLayer.Users.AddNewUser(this.PersonID, this.UserName, this.PassWord, this.IsActive);

            return (this.UserID != -1);
        }


        private bool _UpdateUser()
        {
            //call DataAccess Layer 

            return DVLDDataAccessLayer.Users.UpdateUser(this.UserID,this.PersonID, this.UserName, this.PassWord, this.IsActive);

        }

        public static User Find(int UserID)
        {
            int PersonID = -1;string UserName = "", PassWord = "";bool IsActive =false;
            DateTime DateOfBirth = DateTime.Now;


            if (DVLDDataAccessLayer.Users.GetInfoByUsersID(UserID, ref PersonID, ref UserName, ref PassWord, ref IsActive))

                return new User(UserID,PersonID, UserName, PassWord, IsActive);
            else
                return null;

        }

        public static User FindByPersonID(int PersonID)
        {
            int UserID = -1; string UserName = "", PassWord = ""; bool IsActive = false;
            DateTime DateOfBirth = DateTime.Now;


            if (DVLDDataAccessLayer.Users.GetInfoByPersonID(ref UserID,  PersonID, ref UserName, ref PassWord, ref IsActive))

                return new User(UserID, PersonID, UserName, PassWord, IsActive);
            else
                return null;

        }

        public static User FindUserlogin(string Password,string UserName)
        {
            int UserID = -1; bool IsActive = false; int PersonID = -1;
            DateTime DateOfBirth = DateTime.Now;


            if (DVLDDataAccessLayer.Users.GetUserForLogin(ref UserID, PersonID, ref UserName, ref Password, ref IsActive))

                return new User(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        public bool Save()
        {


            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }




            return false;
        }

        public static DataTable GetAllUsers()
        {
            return DVLDDataAccessLayer.Users.GetAllUsers();

        }

        public static bool DeleteUser(int ID)
        {
            return DVLDDataAccessLayer.Users.DeleteUser(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return DVLDDataAccessLayer.Users.IsUserExist(ID);
        }


    }
}
