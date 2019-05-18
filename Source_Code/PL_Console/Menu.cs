using System;
using Persistence.MODEL;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTables;
namespace PL_Console
{
    public class Menu
    {
        public void MenuChoice()
        {
            while (true)
            {
                string[] choice = { "Đăng nhập", "Thoát" };
                // short choose = Mainmenu("EBook Store", choice);
                short choose = Utility.MenuTemplate("EBook Store", choice);
                switch (choose)
                {
                    case 1:
                        MenuLogin();
                        continue;
                    case 2:
                        Environment.Exit(0);
                        break;
                }
            }


        }
        public void MenuLogin()
        {
            Console.Clear();
            UserBL userBL = new UserBL();
            User user = null;
            string username = null;
            string password = null;
            string choice;
            while (true)
            {
                string row1 = "=====================================================================";
                string row2 = "---------------------------------------------------------------------";
                Console.WriteLine(row1);
                Console.WriteLine(" ĐĂNG NHẬP");
                Console.WriteLine(row2);
                Console.Write("Tên đăng nhập: ");
                username = Console.ReadLine();
                Console.Write("Mật khẩu: ");
                password = Password();
                Console.WriteLine();
                if (ValidateLogin(username) == false || ValidateLogin(password) == false)
                {
                    Console.WriteLine("Tên đăng nhập hoặc mật khẩu không được chứa kí tự đặc biệt");
                    choice = Utility.OnlyYN("Bạn có muốn tiếp tục? Y/N: ");
                    switch (choice)
                    {
                        case "Y":
                            continue;
                        case "N":
                            MenuChoice();
                            break;
                        default:
                            continue;
                    }
                }
                try
                {
                    user = userBL.Login(username, password);
                }
                catch (System.Exception e)
                {

                    Console.WriteLine(e);
                }

                if (user == null)
                {
                    Console.WriteLine("Tài khoản hoặc mật khẩu không đúng");
                    choice = Utility.OnlyYN("Bạn có muốn tiếp tục? Y/N: ");
                    switch (choice)
                    {
                        case "Y":
                            continue;
                        case "N":
                            MenuChoice();
                            break;
                        default:
                            continue;
                    }
                }
                else
                {
                    ConsoleCus cc = new ConsoleCus();
                    cc.MenuCus(user);
                    break;
                }
            }
        }
        public bool ValidateLogin(string infoLogin)
        {
            Regex regex = new Regex("[a-zA-Z0-9_]");
            MatchCollection matchCollectionInfoLogin = regex.Matches(infoLogin);
            if (matchCollectionInfoLogin.Count < infoLogin.Length)
            {
                return false;
            }
            else if (infoLogin == " ")
            {
                return false;
            }
            else
            {
                return true;
            }


        }

       

        public string Password()
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        sb.Length--;
                    }
                    continue;
                }
                Console.Write('*');

                sb.Append(cki.KeyChar);
            }
            return sb.ToString();
        }
    }
}