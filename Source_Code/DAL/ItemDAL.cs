using System;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
using System.Collections.Generic;
namespace DAL
{
    public class ItemDAL
    {
        
        private MySqlDataReader reader;
        private string query;
        public ItemDAL(){}
        public List<Item> GetListItems()
        {
            DbHelper.OpenConnection();
            query = @"select * from items limit 10;";
            List<Item> items = new List<Item>();
           reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            while (reader.Read())
            {
                items.Add(GetItem(reader));
            }
            reader.Close();
            DbHelper.CloseConnection();

            return items;
        }
        public List<Item> SearchItemName()
        {
            query = @"select * from items;";
            List<Item> items = new List<Item>();
            reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            while (reader.Read())
            {
                items.Add(GetItem(reader));
            }
            reader.Close();
            DbHelper.CloseConnection();

            return items;
        }
        // public List<Item> GetListsItems(int numberPage)
        // {
        //     if (connection == null)
        //     {
        //         connection = DbHelper.OpenConnection();
        //     }
        //     if (connection.State == System.Data.ConnectionState.Closed)
        //     {
        //         connection.Open();
        //     }
        //     int page = numberPage + 2;
        //     query = $@"select * from items where itemId between {numberPage + 1} and {page};";
        //     MySqlCommand command = new MySqlCommand(query, connection);
        //     List<Item> items = null;
        //     using (reader = command.ExecuteReader())
        //     {
        //         items = new List<Item>();
        //         while (reader.Read())
        //         {
        //             items.Add(GetItem(reader));
        //         }
        //     }
        //     connection.Close();
        //     return items;
        // }
        public Item GetAnItemById(int? itemId)
        {
            if (itemId == null)
            {
                return null;
            }
            DbHelper.OpenConnection();
            query = $"select * from items where itemId = {itemId}";

           reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            Item item = null;
            if (reader.Read())
            {
                item = GetItem(reader);
            }
            reader.Close();
            DbHelper.CloseConnection();
            return item;
        }
        public List<Item> SearchITem(int temp)
        {

            DbHelper.OpenConnection();
            switch (temp)
            {
                case 1:
                    query = $"select * from items where itemId = ";
                    break;
            }


            reader = DbHelper.ExecQuery(query,DbHelper.OpenConnection());
            List<Item> items = new List<Item>();
            while (reader.Read())
            {
                items.Add(GetItem(reader));
            }
            reader.Close();
            DbHelper.CloseConnection();
            return items;
        }


        private Item GetItem(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemId = reader.GetInt32("itemId");
            item.ItemName = reader.GetString("itemName");
            item.ItemPrice = reader.GetDecimal("itemPrice");
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