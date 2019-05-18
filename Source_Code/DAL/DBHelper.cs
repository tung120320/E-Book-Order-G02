using System;
using System.IO;
using MySql.Data.MySqlClient;

namespace DAL
{

    public class DbHelper
    {
        public static MySqlConnection OpenConnection()
        {

            try
            {
                string connectionString;
                FileStream fileStream = File.OpenRead("ConnectionString.txt");
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    connectionString = reader.ReadLine();
                }
                fileStream.Close();

                return OpenConnection(connectionString);

            }
            catch (System.Exception)
            {

                return null;
            }

        }

        public static MySqlConnection OpenConnection(string connectionString)
        {

            try
            {
                MySqlConnection connection = new MySqlConnection { ConnectionString = connectionString };
                connection.Open();
                return connection;
            }
            catch (System.Exception)
            {
                return null;
            }

        }


    }
}
