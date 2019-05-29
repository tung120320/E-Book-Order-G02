using System;
using Xunit;
using Persistence.MODEL;
using System.Collections.Generic;

namespace DAL.Test
{

    public class OrderUnitTest
    {

        OrderDAL orderDAL = new OrderDAL();
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
            Assert.True(orderDAL.CreateShoppingCart(order));
            userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserId); // set userShopping cart to 1
        }
        // [Fact]
        // public void AddToShoppingCartTest()
        // {
        //     Order order = new Order();
        //     UserDAL userDAL = new UserDAL();
        //     order.OrderUser = new User();
        //     Item item = new Item();
        //     order.OrderItem = new Item();


        //     order.OrderUser.UserId = 1;
        //     order.OrderItem.ItemId = 4;
        //     Assert.True(orderDAL.AddToShoppingcart(order));
        // }


        // [Fact]
        // public void AddToShoppingCartTest1()
        // {
        //     Order order = new Order();
        //     order.OrderUser = new User();
        //     Item item = new Item();
        //     order.OrderItem = new Item();


        //     order.OrderItem.ItemId = null;

        //     Assert.False(orderDAL.AddToShoppingcart(order));
        // }
        
        [Fact]
        public void ShowShopingCartByUserIdTest()
        {
            Assert.NotNull(orderDAL.ShowShopingCartByUserId(1));
        }
        [Fact]
        public void ShowShopingCartByUserIdTest1()
        {
            Assert.Null(orderDAL.ShowShopingCartByUserId(null));
        }

        // [Theory]
        // [InlineData(1)]
        // public void ShowOrderByUserIdTest(int? orderId)
        // {
        //     Order order = new Order();
        //     Assert.Null(orderDAL.ShowOrderByUserId(orderId));
        // }

        // [Fact]
        // public void ShowOrderByUserIdTest()
        // {
        //     Assert.NotNull(orderDAL.ShowOrderByUserId(1));
        // }
        [Fact]
        public void ShowOrderByUserIdTest1()
        {
            Assert.Null(orderDAL.ShowOrderByUserId(null));
        }




        // [Fact]
        // public void TestGetAllOrder()
        // {

        //     Assert.NotNull(orderDAL.GetAllOrder(2)); 
        // }


    }
}
