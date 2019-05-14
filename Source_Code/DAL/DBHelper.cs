using System;
using MySql.Data.MySqlClient;
namespace DAL
{

    public class DbHelper
    {
        private static MySqlConnection connection;
        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                connection = new MySqlConnection
                {
                    ConnectionString = @"server = localhost;
                    userid = root;
                    password = thanhtung1203;
                    port = 3306;
                    database = ebooksstore;"
                };
            }
            return connection;
        }
        public static MySqlConnection OpenConnection()
        {
            if (connection == null)
            {
                GetConnection();
            }
            connection.Open();
            return connection;
        }
        public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
        public static MySqlDataReader ExecuteQuery(string query)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command.ExecuteReader();
        }
    }

}
