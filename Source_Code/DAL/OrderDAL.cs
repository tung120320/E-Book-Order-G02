using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence.MODEL;
namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private string query;
        public OrderDAL()
        {
            connection = DbHelper.OpenConnection();
        }
        // public List<Order> GetAllOrder(int? userId)
        // {
        //     if (userId == null)
        //     {
        //         return null;
        //     }
        //     if (connection.State == System.Data.ConnectionState.Closed)
        //     {
        //         connection = DbHelper.OpenConnection();
        //     }

        //     List<Order> listOrder = new List<Order>();
        //     // query = $@"select orderStatus from Orders ord inner join OrderDetails ordl on ord.orderId = ordl.orderId where orderUser = {userId};";
        //     query = $@"select * from Orders where orderUser = @orderUser ";

        //     MySqlCommand command = new MySqlCommand(query, connection);
        //     command.Parameters.AddWithValue("@orderUser", userId);
        //     using (reader = command.ExecuteReader())
        //     {
        //         while (reader.Read())
        //         {
        //             listOrder.Add(GetOrder(reader));

        //         }
        //     }

        //     connection.Close();
        //     return listOrder;
        // }
        public bool GetStatusShoppingCart(int? userId)
        {
            if (userId == null)
            {
                return false;
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection = DbHelper.OpenConnection();
            }

            query = $@"select UserShoppingCart from  Users  where userId = {userId};";
            MySqlCommand command = new MySqlCommand(query, connection);
            User user = null;
            using (reader = command.ExecuteReader())
            {
                user = new User();
                if (reader.Read())
                {
                    user.UserShoppingCart = GetStatusCart(reader);
                }
            }
            connection.Close();
            if (user.UserShoppingCart == false)
            {
                return false;
            }

            return true;
        }
        public bool UpdateUserShoppingCart(bool isHave, int? userId)
        {
            if (userId == null)
            {
                return false;
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection = DbHelper.OpenConnection();
            }
            MySqlCommand command = connection.CreateCommand();
            switch (isHave)
            {
                case true:
                    command.CommandText = $@"update Users set userShoppingCart = false where userId = {userId}";
                    break;
                case false:
                    command.CommandText = $@"update Users set userShoppingCart = true where userId = {userId}";
                    break;

            }
            command.ExecuteNonQuery();

            connection.Close();
            return true;
        }

        public bool CreateShoppingCart(Order order)
        {

            bool result = false;
            if (order == null)
            {
                return result;
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

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
                // foreach (var Orderitem in order.ListItems)
                // {
                command.Parameters.Clear();
                command.CommandText = "insert into OrderDetails(orderId,itemId) values (@orderId,@itemId)";
                command.Parameters.AddWithValue("@orderId", order.OrderId);
                command.Parameters.AddWithValue("@itemId", order.OrderItem.ItemId);
                command.ExecuteNonQuery();
                //}
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
        public bool AddToShoppingcart(Order order)
        {

            bool result = false;

            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
            }
            catch (System.Exception)
            {

                throw;
            }

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
                connection.Clone();
            }

            return result;

        }

        public List<Item> ShowShopingCart(int? userId)
        {
            if (userId == null)
            {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection = DbHelper.OpenConnection();
            }
            List<Item> listItems = new List<Item>();
            query = $@"select it.itemId, it.itemName, it.itemPrice from 
            orders ord inner join orderDetails ordt on ord.orderId = ordt.orderId 
            inner join Items it on ordt.itemId = it.itemId where ord.orderUser = {userId} and ord.orderStatus = 0 ;";
            MySqlCommand command = new MySqlCommand(query, connection);

            using (reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listItems.Add(GetItemShoppingCart(reader));
                }

            }

            connection.Close();
            return listItems;

        }
        public bool CreateOrder(Order order)
        {

            bool result = false;
            if (order == null)
            {
                return result;
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }

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
        public List<Order> ShowOrder(int? userId)
        {
            if (userId == null)
            {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection = DbHelper.OpenConnection();
            }
            List<Order> listOrders = new List<Order>();
            query = $@"select ord.orderId, ord.orderDate, it.itemName from 
            orders ord inner join orderDetails ordt on ord.orderId = ordt.orderId 
            inner join Items it on ordt.itemId = it.itemId
             where ord.orderUser = {userId} and ord.orderStatus = 1";
            MySqlCommand command = new MySqlCommand(query, connection);

            using (reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    listOrders.Add(GetAllOrder(reader));
                }

            }
            connection.Close();
            return listOrders;
        }
        private Item GetItemShoppingCart(MySqlDataReader reader)
        {
            Item item = new Item();
            item.ItemId = reader.GetInt32("itemId");
            item.ItemName = reader.GetString("itemName");
            item.ItemPrice = reader.GetDouble("itemPrice");
            return item;
        }

        private Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderId = reader.GetInt32("orderId");
            order.OrderUser.UserId = reader.GetInt32("orderUser");
            order.OrderStatus = reader.GetInt32("orderStatus");
            return order;
        }
        private bool GetStatusCart(MySqlDataReader reader)
        {
            User user = new User();
            user.UserShoppingCart = reader.GetBoolean("UserShoppingCart");
            return user.UserShoppingCart;
        }
         private Order GetAllOrder(MySqlDataReader reader)
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