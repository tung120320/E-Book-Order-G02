// using System;
// using MySql.Data.MySqlClient;
// using Persistence.MODEL;
// namespace DAL
// {
//     public class TestDAL
//     {
//         private MySqlConnection connection;
//         private MySqlDataReader reader;
//         private string query;
//         public TestDAL()
//         {
//             connection = DbHelper.OpenConnection();
//         }
//         public bool CreateOrder(Order order){
//             if(order == null || order.ListItems == null || order.ListItems.Count == 0){
//                 return false;
//             }
//             bool result = true;
//             MySqlConnection connection = DbHelper.OpenConnection();
//             MySqlCommand cmd = connection.CreateCommand();
//             cmd.Connection = connection;
            
//             cmd.CommandText = "Lock tables Customers write, Orders write, Items write, OrderDetails write";
//             cmd.ExecuteNonQuery();
//             MySqlTransaction trans = connection.BeginTransaction();
//             cmd.Transaction = trans;
//              if(order.OrderUser == null || order.OrderUser.Username == null || order.OrderUser.Username == ""){
//                  order.OrderUser = new User(){
//                      UserId = 1
//                  };
//              }
//              try
//              {
//                  if(order.OrderUser.UserId == null){
//                      //insert new customer
//                      cmd.CommandText = @"insert into users(userAccount,userPassword)";
//                  }
//              }
//              catch (System.Exception)
//              {
                 
//                  throw;
//              }
//         }

       
//     }
// }