using System;
using Persistence.MODEL;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
namespace DAL
{

    public class UserDAL
    {
        public UserDAL()
        {

            DbHelper.OpenConnection();
        }
        public User Login(string username, string password)
        {

            if ((username == null) || (password == null))
            {
                return null;
            }

            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionUserName = regex.Matches(username);
            MatchCollection matchCollectionPassword = regex.Matches(password);
            if (matchCollectionUserName.Count < username.Length || matchCollectionPassword.Count < password.Length)
            {
                return null;
            }
            User user = GetUserInfo(username,password);
            return user;

        }
        public User GetUserInfo(string username,string password)
        {
            string query = $@"select * from Users where userAccount = '{username}' and userPassword = '{password}'";
            MySqlDataReader reader = DbHelper.ExecuteQuery(query);
            User u = null;
            if (reader.Read())
            {
                u = GetUser(reader);
            }
            DbHelper.CloseConnection();
            return u;
        }
        private static User GetUser(MySqlDataReader reader)
        {
            User user = new User();
            user.UserAccount = reader.GetString("userAccount");
            user.UserPassword = reader.GetString("userPassword");
            return user;
        }

    }

}
