using System;
using Persistence.MODEL;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
namespace DAL
{

    public class UserDAL
    {
        
        private MySqlDataReader reader;
        private string query;

        public UserDAL(){}

        public User GetUserByUserNameAndPassWord(string username, string password)
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
            query = $@"select * from Users where userAccount = '{username}' and userPassword = '{password}'";
            reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            User user = null;
            if (reader.Read())
            {
                user = GetUser(reader);
            }
            reader.Close();
            DbHelper.CloseConnection();
            return user;
        }
        public User GetUserById(int? userId)
        {
            if (userId == null)
            {
                return null;
            }
            query = $@"select * from  Users  where userId = {userId};";
            reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            User user = null;

            if (reader.Read())
            {
                user = GetUser(reader);
            }
            reader.Close();
            DbHelper.CloseConnection();
            return user;
        }
        public bool UpdateStatusShoppingCartById(bool isHave, int? userId)
        {
           
            if (userId == null)
            {
                return false;
            }
            switch (isHave)
            {
                case true:
                    query = $@"update Users set userShoppingCart = false where userId = {userId}";
                    break;
                case false:
                    query = $@"update Users set userShoppingCart = true where userId = {userId}";
                    break;
            }
           
            DbHelper.ExecNonQuery(query, DbHelper.OpenConnection());
            DbHelper.CloseConnection();
            return true;
        }
        private User GetUser(MySqlDataReader reader)
        {
            User user = new User();
            user.UserId = reader.GetInt32("userId");
            user.UserAccount = reader.GetString("userAccount");
            user.UserPassword = reader.GetString("userPassword");
            user.Username = reader.GetString("username");
            user.UserEmail = reader.GetString("userEmail");
            user.UserIdCardNo = reader.GetString("userIdCardNo");
            user.UserBalance = reader.GetDouble("userBalance");
            user.UserShoppingCart = reader.GetBoolean("UserShoppingCart");
            return user;
        }

    }

}
