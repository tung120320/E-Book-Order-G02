// using System;
// using System.IO;
// using MySql.Data.MySqlClient;

// namespace DAL
// {

//     public class DbHelper
//     {
//         // private static MySqlConnection connection;
//         public static MySqlConnection OpenConnection()
//         {
//             try
//             {
//                 string connectionString;
//                 FileStream fileStream = File.OpenRead("ConnectionString.txt");
//                 using (StreamReader reader = new StreamReader(fileStream))
//                 {
//                     connectionString = reader.ReadLine();
//                 }
//                 fileStream.Close();
                
//                 return OpenConnection(connectionString);

//             }
//             catch (System.Exception)
//             {

//                 return null;
//             }

//         }
//         public static MySqlConnection OpenConnection(string connectionString)
//         {
//             MySqlConnection connection;
//             try
//             {
//                  connection = new MySqlConnection{ConnectionString = connectionString};
//             }
//             catch (System.Exception)
//             {
//                 return null;
//             }
//             connection.Open();
            
//             return connection;
//         }
//         // public static void CloseConnection()
//         // {
//         //     if (connection != null)
//         //     {
//         //         connection.Close();
//         //     }
//         // }
//         public static MySqlDataReader ExecuteQuery(string query,MySqlConnection connection)
//         {
//             MySqlCommand command = new MySqlCommand(query, connection);
//             return command.ExecuteReader();
//         }
//     }
// }
