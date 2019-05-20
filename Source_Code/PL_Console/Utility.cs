using System;
using Persistence.MODEL;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTables;
using System.Collections.Generic;

namespace PL_Console
{
    public class Utility
    {

        private static string row1 = "=====================================================================";
        private static string row2 = "---------------------------------------------------------------------";

        public static short MenuTemplate(string title, string[] menuItems)
        {
            Console.Clear();
            short choose = 0;
            Console.WriteLine(row1);
            Console.WriteLine(title);
            Console.WriteLine(row2);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine(row1);
            try
            {
                Console.Write("#Chọn: ");
                choose = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (choose <= 0 || choose > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("#Bạn nhập sai vui lòng nhập lại: ");
                        choose = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (choose <= 0 || choose > menuItems.Length);
            }
            return choose;
        }
        public static void InfoCustomer(string title, string[] menuItems, User us)
        {
            Console.Clear();
            short choose = 1;
            Console.WriteLine(row1);
            Console.WriteLine(title);
            Console.WriteLine(row2);

            string[] infoUser = { us.UserAccount, us.Username, us.UserEmail, us.UserIdCardNo.ToString(), us.UserBalance.ToString() };

            for (int i = 0; i < menuItems.Length; i++)
            {

                Console.WriteLine(menuItems[i] + ": " + infoUser[i]);


            }
            Console.WriteLine(row1);
            try
            {
                Console.Write("Nhập 0 để quay lại: ");
                choose = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (choose != 0)
            {
                do
                {
                    try
                    {
                        Console.Write("#Bạn nhập sai vui lòng nhập lại: ");
                        choose = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (choose != 0);
            }
           
        }
        public static short showListItems(string title, string[] menuItems, List<Item> items)

        {
            Console.Clear();
            short idItem = -1;
            var table = new ConsoleTable("Id sách", "Tên sách", "Giá sách", "Tác giả", "Danh mục");
            
            foreach (Item item in items)
            {
                table.AddRow(item.ItemId, item.ItemName, item.ItemPrice, item.ItemAuthor, item.ItemCategory);

            }
            table.Write();
            try
            {
                Console.Write("Chọn id sản phẩm muốn mua hoặc ấn 0 để quay trở lại: ");
                idItem = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (idItem < 0 || idItem > items.Count)
            {
                do
                {
                    try
                    {
                        Console.Write("#Bạn nhập sai vui lòng nhập lại: ");
                        idItem = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (idItem < 0 || idItem > items.Count);
            }
            return idItem;
           
        }
         public static string OnlyYN(string printcl)
        {
            string choice;
            Console.Write(printcl);
            choice = Console.ReadLine().ToUpper();
            while (true)
            {
                if (choice != "Y" && choice != "N")
                {
                    Console.Write("Bạn chỉ được nhập (Y/N): ");
                    choice = Console.ReadLine().ToUpper();
                    continue;
                }
                break;
            }

            return choice;
        }
    }
}
