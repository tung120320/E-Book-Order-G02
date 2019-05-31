using System;
using System.IO;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace DAL
{

    public class DbHelper
    {
        
    private static MySqlConnection connection = null;
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
            catch
            {
                return null;
            }
        }

        public static MySqlConnection OpenConnection(string connectionString)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection
                {
                    ConnectionString = connectionString
                };
                connection.Open();
                return connection;
            }
            catch
            {
                return null;
            }
        }
        public static void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
        public static MySqlDataReader ExecQuery(string query, MySqlConnection connection)
        {
            MySqlCommand command = new MySqlCommand(query, connection);
            return command.ExecuteReader();
        }
        public static int ExecNonQuery(string query,  MySqlConnection connection)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
           
            return command.ExecuteNonQuery();
        }
        public static bool ExecTransaction(List<string> queries)
        {
            bool result = false;
            OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            MySqlTransaction trans = connection.BeginTransaction();

            command.Connection = connection;
            command.Transaction = trans;
            try
            {
                foreach (var query in queries)
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                trans.Commit();
                result = true;
            }
            catch (System.Exception e)
            {
                result = false;
                trans.Rollback();
                Console.WriteLine(e);
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

    }
}
