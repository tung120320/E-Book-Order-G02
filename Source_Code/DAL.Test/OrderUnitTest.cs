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
        public void TestCreateShoppingCart()
        {
            Order order = new Order();
            order.OrderUser = new User();
            order.OrderItem = new Item();

            User user = new User();
            user.UserId = 1;

            Item item = new Item();
            item.ItemId = 2;

            order.OrderStatus = 0;
            order.OrderUser.UserId = user.UserId;
            order.OrderItem.ItemId = item.ItemId;
            
            Assert.True(orderDAL.CreateShoppingCart(order));
        }
        [Fact]
        public void TestAddToShoppingcart()
        {
            Order order = new Order();
            Item item = new Item();
            order.OrderItem = new Item();

            order.OrderId = 1;
            order.OrderItem.ItemId = 4;
            
            Assert.True(orderDAL.AddToShoppingcart(order));
        }
        [Theory]
        [InlineData(1)]

        public void GetAnOrder(int? orderId)
        {
            Order order = new Order();
            Assert.NotNull(orderDAL.ShowShopingCart(orderId));
        }
       
        [Theory]
        
        [InlineData(0)]
        [InlineData(null)]
        
        public void GetAnOrder1(int? orderId)
        {
            Order order = new Order();
            Assert.Null(orderDAL.ShowShopingCart(orderId));
        }
        [Fact]
        public void TestGetAllOrder()
        {
           
            Assert.NotNull(orderDAL.GetAllOrder(2)); 
        }
       
       
    }
}