using System;

namespace Persistence.MODEL
{
    public class User
    {
        public User() {}


        public int UserId {get;set;}
        public string UserAccount {get;set;}
        public string UserPassword{get;set;}
        public string Username{get;set;}
        public string UserEmail{get;set;}
        public string UserIdCardNo{get;set;}
        public double UserBalance{get;set;}
        public bool UserShoppingCart{get;set;}
        
        
    }
}
