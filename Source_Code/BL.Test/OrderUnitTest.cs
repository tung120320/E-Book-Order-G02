using System;
using Xunit;
using Persistence.MODEL;
using System.Collections.Generic;
using DAL;
namespace BL.Test
{
    public class OrderUnitTest
    {
        OrderBl orderBL = new OrderBl();
        [Fact]
        public void TestCreateOrder()


        {
            UserDAL userDAL = new UserDAL();
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            order.OrderStatus = 0;
            order.OrderUser.UserId = 1;
            order.OrderItem.ItemId = 2;
            Assert.True(orderBL.CreateShoppingCart(order));
            userDAL.UpdateStatusShoppingCartById(false, order.OrderUser.UserId); 

            Assert.True(orderBL.CreateShoppingCart(order));
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
        public void ShowOrderByUserIdTest1()
        {
            Assert.Null(orderBL.ShowOrderByUserId(null));
        }
        // [Theory]
        // [InlineData(1)]

        // public void GetAnOrder(int? orderId)
        // {
        //     Order order = new Order();
        //     Assert.NotNull(orderBL.GetAnOrder(orderId));
        // }

        // [Theory]

        // [InlineData(0)]
        // [InlineData(null)]

        // public void GetAnOrder1(int? orderId)
        // {
        //     Order order = new Order();
        //     Assert.Null(orderBL.GetAnOrder(orderId));
        // }

    }
}