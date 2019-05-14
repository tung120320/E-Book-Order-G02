using System;

namespace Persistence.MODEL
{
    public class Users
    {
        public Users() {}

        public Users(int userId, string userName, string userPassword, string nameUser, string userEmail, int userIdCardNo, float userBalance)
        {
            this.userId = userId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.nameUser = nameUser;
            this.userEmail = userEmail;
            this.userIdCardNo = userIdCardNo;
            this.userBalance = userBalance;
        }

        public int userId {get;set;}
        public string userName {get;set;}
        public string userPassword{get;set;}
        public string nameUser{get;set;}
        public string userEmail{get;set;}
        public int userIdCardNo{get;set;}
        public float userBalance{get;set;}
    }
}
