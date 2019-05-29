using System;
using DAL;
using Persistence.MODEL;
using System.Text.RegularExpressions;
namespace BL
{
    public class UserBL
    {

        private UserDAL userDAL;
        public UserBL()
        {
            userDAL = new UserDAL();
        }

        public User GetUserByUserNameAndPassWord(string username, string password)
        {
            if (username == null || password == null)
            {
                return null;
            }

            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUsername = regex.Matches(username);
            MatchCollection matchCollectionPassword = regex.Matches(password);
            if (matchCollectionUsername.Count < username.Length || matchCollectionPassword.Count < password.Length)
            {
                return null;
            }

            return userDAL.GetUserByUserNameAndPassWord(username, password);
        }
        public User GetUserById(int? userId)
        {
            return userDAL.GetUserById(userId);
        }
        public bool UpdateStatusShoppingCartById(bool isHave, int? userId)
        {
            return userDAL.UpdateStatusShoppingCartById(isHave, userId);
        }

    }

}
