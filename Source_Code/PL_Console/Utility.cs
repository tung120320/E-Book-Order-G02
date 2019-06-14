using System;
using Persistence.MODEL;
using BL;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTables;
using System.Collections.Generic;
using System.Globalization;

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
        public static short MenuDetail(string title, string[] menuItems)
        {

            short choose = 0;

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
            Console.WriteLine(row1);
            Console.WriteLine(title);
            // Console.WriteLine(row2);

            string[] infoUser = { us.UserAccount, us.Username, us.UserEmail, us.UserIdCardNo.ToString(), FormatCurrency(us.UserBalance) };

            // for (int i = 0; i < menuItems.Length; i++)
            // {
            //     Console.WriteLine(menuItems[i] + ": " + infoUser[i]);
            // }
            var table = new ConsoleTable("Tên tài khoản", us.UserAccount);
            table.AddRow("Tên khách hàng", us.Username);
            table.AddRow("Email", us.UserEmail);

            table.AddRow("CMND", us.UserIdCardNo.ToString());
            table.AddRow("Số dư", FormatCurrency(us.UserBalance));

            table.Write();
            Console.WriteLine(row1);
            Console.Write("Nhấn phím bất kì để quay lại ");
            Console.ReadKey();

        }
        public static short showListItems(string title, string[] menuItems, List<Item> items, int UserId)

        {
            Console.Clear();
            Console.Clear();
            short choice = -1;
            var table = new ConsoleTable("Mã sách", "Tên sách", "Giá sách", "Tác giả", "Danh mục");

            OrderBl orderBL = new OrderBl();

            foreach (Item item in items)
            {
                bool bought = false;
                if (item.ItemId != orderBL.CheckItemPurchase(item.ItemId, UserId))
                {
                    bought = true;
                }

                table.AddRow(item.ItemId, item.ItemName, bought == true ? FormatCurrency(item.ItemPrice) : "Đã mua", item.ItemAuthor, item.ItemCategory);

            }
            table.Write();
            if (items.Count <= 0)
            {
                Console.WriteLine("Không tìm thấy sách");
            }
            ItemBl itemBL = new ItemBl();


            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + menuItems[i]);
            }

            Console.WriteLine(row1);
            try
            {
                Console.Write("Chọn: ");
                choice = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (choice < 0 || choice > menuItems.Length)
            {
                do
                {
                    try
                    {
                        Console.Write("#Bạn nhập sai vui lòng nhập lại: ");
                        choice = Int16.Parse(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (choice < 0 || choice > menuItems.Length);
            }
            return choice;

        }
        public static short SelectAnItem(List<Item> items)

        {

            short idItem = -1;
            bool isHave = false;
            try
            {
                Console.Write("Chọn mã sản phẩm: ");

                idItem = Int16.Parse(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            foreach (var item in items)
            {

                if (idItem == item.ItemId)
                {
                    isHave = true;
                }
            }
            if (!isHave)
            {
                do
                {
                    try
                    {
                        Console.Write("#Bạn nhập sai vui lòng nhập lại: ");
                        idItem = Int16.Parse(Console.ReadLine());
                        foreach (var item in items)
                        {

                            if (idItem == item.ItemId)
                            {
                                isHave = true;
                            }
                        }
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (!isHave);
            }
            return idItem;

        }
        public static Rating MenuRating(int? userId, int? itemId)

        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Rating rating = new Rating();
            rating.ItemId = itemId;
            rating.UserId = userId;
            int ratingStars = -1;
            string ratingTitle = string.Empty;
            string ratingContent = string.Empty;

            try
            {
                Console.Write("#Nhập số sao: ");
                ratingStars = Convert.ToInt32(Console.ReadLine());
            }
            catch (System.Exception)
            {

            }
            if (ratingStars < 1 || ratingStars > 5)
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Số sao phải từ 1 đến 5: ");
                        Console.Write("#Mời bạn nhập lại số sao: ");
                        ratingStars = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (ratingStars < 1 || ratingStars > 5);
            }
            try
            {
                Console.Write("#Nhập tiêu đề: ");
                ratingTitle = Console.ReadLine();

            }
            catch (System.Exception)
            {
            }
            if (ratingTitle.Length <= 0)
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Bạn nhập sai");
                        Console.Write("#Mời bạn nhập lại tiêu đề: ");
                        ratingTitle = Console.ReadLine();
                    }
                    catch (System.Exception)
                    {
                        continue;
                    }
                } while (ratingTitle.Length <= 0);
            }

            try
            {
                Console.Write("#Nhập nội dung: ");
                ratingContent = Console.ReadLine();
            }
            catch (System.Exception)
            {

            }
            if (ratingContent.Length <= 0)
            {
                do
                {
                    try
                    {
                        Console.WriteLine("Bạn nhập sai");
                        Console.Write("#Mời bạn nhập lại nội dung: ");
                        ratingContent = Console.ReadLine();
                    }
                    catch (System.Exception)
                    {

                    }
                } while (ratingContent.Length <= 0);
            }

            rating.RatingStars = ratingStars;
            rating.RatingTitle = ratingTitle;
            rating.RatingContent = ratingContent;

            rating.RatingDate = DateTime.Now;
            return rating;
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
        public static string FormatCurrency(double price)
        {
            string a = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VND", price);
            return a;
        }
    }
}
