using System;
using DAL;
using Persistence.MODEL;
using System.Collections.Generic;
namespace BL
{
    public class OrderBl
    {
        OrderDAL orderDAL;

        public OrderBl()
        {
            orderDAL = new OrderDAL();
        }

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
        public bool DeleteItemInShoppingCartByIdItem(int? itemId)
        {

            return orderDAL.DeleteItemInShoppingCartByIdItem(itemId);
        }
        // public List<Order> GetAllOrder(int? userId)
        // {

        //     return orderDAL.GetAllOrder(userId);
        // }

        public List<Item> ShowShopingCartByUserId(int? userId)
        {
            return orderDAL.ShowShopingCartByUserId(userId);
        }
        public bool CreateOrder(Order order, double total)
        {
            if (total > order.OrderUser.UserBalance)
            {
                return false;
            }
            else
            {
                order.OrderUser.UserBalance -= total;
            }

            return orderDAL.CreateOrder(order);
        }
        //for test
        public bool CreateOrder(Order order)
        {

            return orderDAL.CreateOrder(order);
        }

        public List<Order> ShowAllItemOrdered(int? userId)
        {

            return orderDAL.ShowAllItemOrdered(userId);
        }
        public List<Order> ShowOrderUserPaySucess(int? userId)
        {
            if(userId == null){
                return null;
            }
            List<Order> listOrders = orderDAL.ShowOrderUserPaySucess(userId);
            foreach (var item in listOrders)
            {
                item.OrderItem.ItemName.ToUpper();
            }
            return listOrders;
        }
        public bool DeleteAllItemInShoppingCartByUserID(int? userId)
        {

            return orderDAL.DeleteAllItemInShoppingCartByUserID(userId);
        }
        public int? CheckItemPurchase(int? itemId, int? userId)
        {

            return orderDAL.CheckItemPurchase(itemId, userId);
        }
    }
}