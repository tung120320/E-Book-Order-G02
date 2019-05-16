using System;
using Persistence.MODEL;
using BL;
namespace PL_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            UserBL uBL = new UserBL();
            User user = null;
            Console.WriteLine("Nhập tài khoản");
            string accountName = Console.ReadLine();
            Console.WriteLine("Nhập mật khẩu");
            string password = Console.ReadLine();
            try
            {
                user = uBL.Login(accountName, password);
            }
            catch (System.Exception e)
            {

               Console.WriteLine(e);
            }
          

            if (user == null)
            {
                Console.WriteLine("Sai tai khoan hoac mac khau");
            }
            else
            {
                Console.WriteLine("Dang nhap thanh cong");
            }

        }
    }
}
