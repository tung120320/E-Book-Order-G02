using System;
using System.Collections.Generic;
namespace Persistence.MODEL
{
    public class Order
    {
        public Order() {}

        
        
        public int? OrderId {get;set;}
        public User OrderUser{get;set;}
        public DateTime OrderDate{get;set;}
        public int OrderStatus{get;set;}
        public Item OrderItem;
        public List<Item> ListItems;

        


    }
}
