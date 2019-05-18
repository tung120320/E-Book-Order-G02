using System;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
using System.Collections.Generic;
namespace DAL
{
    public class ItemDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;
        public ItemDAL()
        {
            connection = DbHelper.OpenConnection();
        }

        public List<Item> GetListsItems()
        {
            if (connection == null)
            {
                connection = DbHelper.OpenConnection();
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = "select * from items;";
            MySqlCommand command = new MySqlCommand(query, connection);
            List<Item> items = null;
            using (reader = command.ExecuteReader())
            {
                items = new List<Item>();
                while (reader.Read())
                {
                    items.Add(GetItem(reader));
                }
            }
            connection.Close();
            return items;
        }
        public Item GetAnItem(int? itemId)
        {
            if (itemId == null)
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
            query = $"select * from items where itemId = {itemId}";
            MySqlCommand command = new MySqlCommand(query, connection);
            Item item = null;
            using (reader = command.ExecuteReader())
            {
                if(reader.Read()){
                    item = GetItem(reader);
                }
                
            }
            connection.Close();
            return item;
        }

        private Item GetItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemId = reader.GetInt32("itemId");
            item.ItemName = reader.GetString("itemName");
            item.ItemPrice = reader.GetDouble("itemPrice");
            item.ItemAuthor = reader.GetString("itemAuthor");
            item.ItemCategory = reader.GetString("itemCategory");
            item.ItemDescription = reader.GetString("itemDescription");
            item.ItemISBN = reader.GetInt32("itemISBN");
            item.ItemPublished = reader.GetDateTime("itemPublished");
            item.ItemPublisher = reader.GetString("itemPublisher");
            item.ItemLanguage = reader.GetString("itemLanguage");
            item.ItemPages = reader.GetInt32("itemPages");

            return item;
        }
    }
}