using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
namespace BL
{
    public class OrderBl
    {
        OrderDAL orderDAL = new OrderDAL();
        public bool CreateShoppingCart(Order order)
        {
            
            if (order == null)
            {
                return false;
            }
            return orderDAL.CreateShoppingCart(order);
        }
        public bool AddToShoppingcart(Order order)
        {
          
            return orderDAL.AddToShoppingcart(order);
        }
        public List<Order> GetAllOrder(int? userId)
        {
          
            return orderDAL.GetAllOrder(userId);
        }
        public List<Item> ShowShopingCart(int? userId){
            return orderDAL.ShowShopingCart(userId);
        }
    }
}