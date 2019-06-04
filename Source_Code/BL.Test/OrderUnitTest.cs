using System;
using Xunit;
using Persistence.MODEL;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BL;
namespace DAL.Test
{

    public class OrderUnitTest
    {
        private MySqlDataReader reader;
          private OrderBl orderBL = new OrderBl();
          private OrderDAL orderDAL = new OrderDAL();
        [Fact]
        public void CreateShoppingCartTest()
        {
            UserDAL userDAL = new UserDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.OrderStatus = 0;
            order.OrderUser.UserId = 1;
            order.OrderItem.ItemId = 2;


            Assert.True(orderBL.CreateShoppingCart(order));
            orderBL.DeleteAllItemInShoppingCartByUserID(1);
            // userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserId); // set userShopping cart to 1
        }
        [Fact]
        public void CreateShoppingCartTest1()
        {
            UserDAL userDAL = new UserDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.OrderStatus = 0;
            order.OrderUser.UserId = 0;
            order.OrderItem.ItemId = 0;

            Assert.False(orderBL.CreateShoppingCart(order));
        }
        [Fact]
        public void AddToShoppingCartTest()
        {
            Order order = new Order();
            UserDAL userDAL = new UserDAL();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();


            order.OrderUser.UserId = 1;
            order.OrderItem.ItemId = 4;


            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,orderStatus) values ({order.OrderUser.UserId},0)";
            command.ExecuteNonQuery();
            userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserId); // set userShopping cart to 1

            Assert.True(orderBL.AddToShoppingcart(order));

            orderBL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserId);
            userDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserId); // set userShopping cart to 0
        }
        [Fact]
        public void AddToShoppingCartTest1()
        {
            Order order = new Order();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();


            order.OrderUser.UserId = 0;
            order.OrderItem.ItemId = 0;

            Assert.False(orderBL.AddToShoppingcart(order));
        }
        [Fact]
        public void AddToShoppingCartTest2()
        {
            Order order = new Order();
            UserDAL userDAL = new UserDAL();
            order.OrderUser = new User();
            Item item = new Item();
            order.OrderItem = new Item();


            order.OrderUser.UserId = 1;
            order.OrderItem.ItemId = null;


            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,orderStatus) values ({order.OrderUser.UserId},0)";
            command.ExecuteNonQuery();
            userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserId); // set userShopping cart to 1

            Assert.False(orderBL.AddToShoppingcart(order));

            orderBL.DeleteAllItemInShoppingCartByUserID(order.OrderUser.UserId);
            userDAL.UpdateStatusShoppingCartById(true, order.OrderUser.UserId); // set userShopping cart to 0
        }
        [Fact]
        public void DeleteItemInShoppingCartByIdItemTest()
        {
            int userId = 1;
            int idItem = 1;
            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,orderStatus) values ({userId},0)";
            command.ExecuteNonQuery();
            int orderID = GetLastInsertOrderID(1);
            command.CommandText = $"insert into OrderDetails(orderId,itemId) values ({orderID},{idItem})";
            command.ExecuteNonQuery();

            Assert.True(orderBL.DeleteItemInShoppingCartByIdItem(userId));
            orderBL.DeleteAllItemInShoppingCartByUserID(userId);
        }
        [Fact]
        public void DeleteItemInShoppingCartByIdItemTest1()
        {

            Assert.False(orderBL.DeleteItemInShoppingCartByIdItem(null));

        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest()
        {
            int userId = 1;
            MySqlCommand command = DbHelper.OpenConnection().CreateCommand();
            command.CommandText = $"insert into Orders(orderUser,orderStatus) values ({userId},0)";
            command.ExecuteNonQuery();
            int orderID = GetLastInsertOrderID(1);
            command.CommandText = $"insert into OrderDetails(orderId,itemId) values ({orderID},1)";
            command.ExecuteNonQuery();

            Assert.True(orderBL.DeleteAllItemInShoppingCartByUserID(userId));
        }
        [Fact]
        public void DeleteAllItemInShoppingCartByUserIDTest1()
        {

            Assert.False(orderBL.DeleteAllItemInShoppingCartByUserID(0));
        }

        [Fact]
        public void ShowShopingCartByUserIdTest()
        {
            Assert.NotNull(orderBL.ShowShopingCartByUserId(1));
        }
        [Fact]
        public void ShowShopingCartByUserIdTest1()
        {
            Assert.Null(orderBL.ShowShopingCartByUserId(null));
        }
        [Fact]
        public void CreateOrderTest()
        {
             UserDAL userDAL = new UserDAL();
            ItemDAL itemDAL = new ItemDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.OrderStatus = 0;
            order.OrderItem = itemDAL.GetAnItemById(2);
            order.OrderUser = userDAL.GetUserById(1);
            orderDAL.CreateShoppingCart(order);

            Assert.True(orderBL.CreateOrder(order));

            orderBL.DeleteAllItemInShoppingCartByUserID(1);

           
        }

        [Fact]
        public void ShowOrderByUserIdTest()
        {
            UserDAL userDAL = new UserDAL();
            ItemDAL itemDAL = new ItemDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.OrderStatus = 0;
            order.OrderItem = itemDAL.GetAnItemById(2);
            order.OrderUser = userDAL.GetUserById(1);
            orderBL.CreateShoppingCart(order);
            orderBL.CreateOrder(order);

            Assert.NotNull(orderBL.ShowAllItemOrdered(1));

            orderBL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderByUserIdTest1()
        {
            Assert.Null(orderBL.ShowAllItemOrdered(null));
        }
        [Fact]
        public void ShowOrderUserPaySucessTest()
        {
            UserDAL userDAL = new UserDAL();
            ItemDAL itemDAL = new ItemDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.OrderStatus = 0;
             order.OrderItem = itemDAL.GetAnItemById(2);
            order.OrderUser = userDAL.GetUserById(1);
           
            orderBL.CreateShoppingCart(order);
            orderBL.CreateOrder(order);

            Assert.NotNull(orderBL.ShowOrderUserPaySucess(1));

            orderBL.DeleteAllItemInShoppingCartByUserID(1);
        }
        [Fact]
        public void ShowOrderUserPaySucessTest1()
        {
            Assert.Null(orderBL.ShowOrderUserPaySucess(null));
        }
        public int GetLastInsertOrderID(int userID)
        {
            int orderId = -1;

            string queryLastInsertId = $@"select orderId from orders where orderUser = {userID} order by orderid desc limit 1;";
            reader = DbHelper.ExecQuery(queryLastInsertId, DbHelper.OpenConnection());
            if (reader.Read())
            {
                orderId = reader.GetInt32("orderId");

            }
            reader.Close();
            return orderId;
        }
    }
}
