// using Xunit;
// using System;
// using DAL;
// namespace DAL.Test
// {
//     public class OrderUnitTest
//     {
//         ItemDAL orderDAL = new ItemDAL();
//         [Fact]
//         public void GetOrders()
//         {
//             Assert.NotNull(orderDAL.GetListsItems());
//         }
        
//         [Fact]
//         public void GetAnOrder()
//         {
//             Assert.NotNull(orderDAL.GetAnOrder(1));
//         }
    
//         [Fact]
//         public void GetAnOrder1()
//         {
//             Assert.Null(orderDAL.GetAnOrder(0));
//         }
//         [Fact]
//         public void GetAnOrder2()
//         {
//             Assert.Null(orderDAL.GetAnOrder(null));
//         }
    
//     }
// }