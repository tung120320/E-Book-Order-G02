using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
namespace DAL
{
    public class OrderDAL
    {

        private MySqlDataReader reader;
        private string query;
        public OrderDAL() { }

        public bool CreateShoppingCart(Order order)
        {

            bool result = false;
            if (order == null)
            {
                return result;
            }

            MySqlConnection connection = DbHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();

            command.CommandText = @"lock tables Orders write, Items write, OrderDetails write";
            command.ExecuteNonQuery();

            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            try
            {
                command.CommandText = "insert into Orders(orderUser,orderStatus) values (@userId,@OrderStatus)";
                command.Parameters.AddWithValue("@userId", order.OrderUser.UserId);
                command.Parameters.AddWithValue("@OrderStatus", order.OrderStatus);
                command.ExecuteNonQuery();

                string queryLastInsertId = $@"select orderId from orders where orderUser = {order.OrderUser.UserId} order by orderid desc limit 1;";
                MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
                using (reader = selectLastId.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order.OrderId = reader.GetInt32("orderId");

                    }
                }

                command.Parameters.Clear();
                command.CommandText = "insert into OrderDetails(orderId,itemId) values (@orderId,@itemId)";
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@itemId", order.OrderItem.ItemId);
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }
            catch (System.Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();

            }

            return result;

        }
        public bool AddToShoppingcart(Order order)
        {

            bool result = false;

            MySqlConnection connection = DbHelper.OpenConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Orders write, Items write, OrderDetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            string queryLastInsertId = $@"select orderId from orders where orderUser = {order.OrderUser.UserId} order by orderid desc limit 1;";
            MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
            using (reader = selectLastId.ExecuteReader())
            {
                if (reader.Read())
                {
                    order.OrderId = reader.GetInt32("orderId");
                }
            }
            try
            {

                command.CommandText = "insert into OrderDetails(orderId,itemId) values (@orderId,@itemId)";
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@itemId", order.OrderItem.ItemId);
                command.ExecuteNonQuery();
                transaction.Commit();
                result = true;

            }
            catch (System.Exception)
            {
                transaction.Rollback();
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Close();

            }

            return result;

        }
        public bool DeleteItemInShoppingCartByIdItem(int? itemId)
        {
            if (itemId == null)
            {
                return false;
            }
            query = $@"DELETE FROM orderDetails where itemId = {itemId};";

            MySqlConnection connection = DbHelper.OpenConnection();
            if (DbHelper.ExecNonQuery(query, connection) == 0)
            {
                DbHelper.CloseConnection();
                return false;
            }
            DbHelper.CloseConnection();
            return true;

        }
        public bool DeleteAllItemInShoppingCartByUserID(int? userId)
        {
            bool result = false;
            int orderId = -1;
            MySqlConnection connection = DbHelper.OpenConnection();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Users write, Orders write, Items write, OrderDetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                string queryLastInsertId = $@"select max(orderId) as orderId from orders where orderUser = {userId} order by orderid desc limit 1;";
                MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
                using (reader = selectLastId.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        orderId = reader.GetInt32("orderId");

                    }
                }

                command.Parameters.Clear();
                command.CommandText = $@"DELETE FROM orderDetails where orderId = {orderId}";
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = $@"DELETE FROM orders where orderId = {orderId}";
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }

            catch (System.Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Clone();

            }

            return result;



        }
        public List<Item> ShowShopingCartByUserId(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            List<Item> listItems = new List<Item>();
            query = $@"select it.itemId, it.itemName, it.itemPrice from 
            orders ord inner join orderDetails ordt on ord.orderId = ordt.orderId 
            inner join Items it on ordt.itemId = it.itemId where ord.orderUser = {userId} and ord.orderStatus = 0 ;";
            reader = DbHelper.ExecQuery(query, DbHelper.OpenConnection());
            while (reader.Read())
            {
                listItems.Add(GetItemShoppingCart(reader));
            }
            DbHelper.CloseConnection();
            return listItems;

        }
        public bool CreateOrder(Order order)
        {

            bool result = false;
            if (order == null)
            {
                return result;
            }
            MySqlConnection connection = DbHelper.OpenConnection();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"lock tables Users write, Orders write, Items write, OrderDetails write";
            command.ExecuteNonQuery();
            MySqlTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;

            try
            {
                string queryLastInsertId = $@"select orderId from orders where orderUser = {order.OrderUser.UserId} order by orderid desc limit 1;";
                MySqlCommand selectLastId = new MySqlCommand(queryLastInsertId, connection);
                using (reader = selectLastId.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order.OrderId = reader.GetInt32("orderId");

                    }
                }

                command.Parameters.Clear();
                command.CommandText = $@"UPDATE Orders SET orderStatus = 1, orderDate = NOW() where orderUser = {order.OrderUser.UserId} and orderId = {order.OrderId};";
                command.ExecuteNonQuery();

                command.Parameters.Clear();
                command.CommandText = $@"UPDATE Users SET userBalance = {order.OrderUser.UserBalance} where userId = {order.OrderUser.UserId}";
                command.ExecuteNonQuery();

                transaction.Commit();
                result = true;
            }

            catch (System.Exception e)
            {
                transaction.Rollback();
                Console.WriteLine(e);
                return result;
            }
            finally
            {
                command.CommandText = "unlock tables";
                command.ExecuteNonQuery();
                connection.Clone();

            }

            return result;

        }
        public List<Order> ShowOrderByUserId(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            List<Order> listOrders = new List<Order>();
            query = $@"select ord.orderId, ord.orderDate, it.itemName from 
            orders ord inner join orderDetails ordt on ord.orderId = ordt.orderId 
            inner join Items it on ordt.itemId = it.itemId
             where ord.orderUser = {userId} and ord.orderStatus = 1";
            reader = DbHelper.ExecQuery(query, DbHelper.OpenConnection());
            while (reader.Read())
            {
                listOrders.Add(GetOrder(reader));
            }
            DbHelper.CloseConnection();
            return listOrders;
        }
        public Order ShowOrderUserPaySucess(int? userId)
        {
            if (userId == null)
            {
                return null;
            }

            Order order = null;
            query = $@"select max(ord.orderId) as orderId, ord.orderDate, it.itemName from 
            orders ord inner join orderDetails ordt on ord.orderId = ordt.orderId 
            inner join Items it on ordt.itemId = it.itemId
             where ord.orderUser = {userId} ";
            reader = DbHelper.ExecQuery(query, DbHelper.OpenConnection());
            if (reader.Read())
            {
                order = GetOrder(reader);
            }
            DbHelper.CloseConnection();
            return order;
        }
        private Item GetItemShoppingCart(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemId = reader.GetInt32("itemId");
            item.ItemName = reader.GetString("itemName");
            item.ItemPrice = reader.GetDecimal("itemPrice");
            return item;
        }
        private Order GetOrder(MySqlDataReader reader)
        {
            Item item = new Item();
            Order order = new Order();
            order.OrderItem = new Item();
            order.OrderId = reader.GetInt32("orderId");
            order.OrderDate = reader.GetDateTime("orderDate");
            order.OrderItem.ItemName = reader.GetString("itemName");
            return order;
        }

    }
}