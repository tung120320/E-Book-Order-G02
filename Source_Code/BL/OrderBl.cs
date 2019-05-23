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
        // public List<Order> GetAllOrder(int? userId)
        // {
          
        //     return orderDAL.GetAllOrder(userId);
        // }
       
        public List<Item> ShowShopingCart(int? userId){
            return orderDAL.ShowShopingCart(userId);
        }
        public bool CreateOrder(Order order, double total){
            if(total > order.OrderUser.UserBalance){
                return false;
            }else{
                order.OrderUser.UserBalance -= total;
            }
            
            return orderDAL.CreateOrder(order);
        }
        public bool UpdateUserShoppingCart(bool IsHave, int? userId){
          
            return orderDAL.UpdateUserShoppingCart(IsHave,userId);
        }
        public bool GetStatusShoppingCart(int? userId){
          
            return orderDAL.GetStatusShoppingCart(userId);
        }
        public List<Order> ShowOrder(int? userId){
          
            return orderDAL.ShowOrder(userId);
        }
    }
}