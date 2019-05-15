// using System;
// using Persistence.MODEL;
// using MySql.Data.MySqlClient;
// using System.Text.RegularExpressions;
// namespace DAL
// {

//     public class UserDAL
//     {
//         private MySqlConnection connection;

//         public UserDAL()
//         {

//             connection=DbHelper.OpenConnection();
//         }
//         public User Login(string username, string password)
//         {

//             if ((username == null) || (password == null))
//             {
//                 return null;
//             }
//             if (connection==null)
//             {
//               connection = DbHelper.OpenConnection();

//             }
//             if (connection.State == System.Data.ConnectionState.Closed)
//             {
//                 connection.Open();
//             }
//             Regex regex = new Regex("[a-zA-Z0-9_]");
//             MatchCollection matchCollectionUserName = regex.Matches(username);
//             MatchCollection matchCollectionPassword = regex.Matches(password);
//             if (matchCollectionUserName.Count < username.Length || matchCollectionPassword.Count < password.Length)
//             {
//                 return null;
//             }
//             User user = GetUserInfo(username);
//             return user;

//         }
//         public User GetUserInfo(string username)
//         {
//             string query = $@"select * from Users where userAccount = '"+ username + "';";
//             MySqlDataReader reader = DbHelper.ExecuteQuery(query,connection);
//             User u = null;
//             if (reader.Read())
//             {
//                 u = GetUser(reader);
//             }
//             connection.Close();
//             return u;
//         }

//         private User GetUser(MySqlDataReader reader)
//         {
//             User user = new User();
//             user.UserAccount = reader.GetString("userAccount");
//             return user;
//         }

//     }

// }
