using System;
using Persistence.MODEL;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
namespace DAL
{

    public class UserDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;
        public UserDAL()
        {
            connection = DbHelper.OpenConnection();
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

            if (connection == null)
            {
                connection = DbHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

            User user = GetAccountUser(username, password);
            return user;

        }
        public User GetAccountUser(string username, string password)
        {
            query = $@"select * from Users where userAccount = '{username}' and userPassword = '{password}'";
            MySqlCommand command = new MySqlCommand(query, connection);
            User user = null;
            using (reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = GetUser(reader);
                }
            }

            connection.Close();
            return user;
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
