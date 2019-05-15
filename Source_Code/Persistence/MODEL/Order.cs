using System;

namespace Persistence.MODEL
{
    public class Order
    {
        public Order() {}

        
        public int OrderId {get;set;}
        public int UserId {get;set;}
        public DateTime OrderDate{get;set;}
        public DateTime OrderPaidDate{get;set;}
        
    }
}
