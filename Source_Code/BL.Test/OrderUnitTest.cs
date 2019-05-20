using System;
using Xunit;
using Persistence.MODEL;
using System.Collections.Generic;
namespace BL.Test
{
    public class OrderUnitTest
    {
        OrderBl orderBL = new OrderBl();
        [Fact]
        public void TestCreateOrder()


        {
            Order order = new Order();
            Item item = new Item();
            Item item1 = new Item();
            order.OrderUser = new User();
            order.OrderUser.UserId = 1;

            item.ItemId = 2;
            item1.ItemId = 3;

            order.OrderStatus = 1;
            order.ListItems = new List<Item>();
        

            order.ListItems.Add(item);

            Assert.True(orderBL.CreateShoppingCart(order));
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