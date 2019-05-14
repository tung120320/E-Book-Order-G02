using System;
using DAL;
using Persistence.MODEL;
namespace BL
{
    public class UsersBL
    {
        private UsersDAL usersDAL;
        public UsersBL(){
            usersDAL = new UsersDAL();
        }
        public Users GetUserInfo(){
            return usersDAL.GetUserInfo();
        }
        
    }
    
}
