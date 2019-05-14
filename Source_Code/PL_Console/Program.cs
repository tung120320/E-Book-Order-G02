using System;
using Persistence.MODEL;
using BL;
namespace PL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersBL userBL =new UsersBL();
            Users user = userBL.GetUserInfo();
            Console.WriteLine("id: {0}",user.userId);
            Console.WriteLine("name: {0}",user.userName);
            Console.WriteLine("name: {0}",user.userEmail);
            Console.WriteLine("name: {0}",user.userBalance);
            Console.WriteLine("name: {0}",user.userPassword);
            Console.WriteLine("name: {0}",user.nameUser);
        }
    }
}
