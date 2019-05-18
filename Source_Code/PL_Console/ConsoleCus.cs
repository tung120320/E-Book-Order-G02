using System;
using Persistence.MODEL;
using BL;
using System.Collections.Generic;
using ConsoleTables;
namespace PL_Console
{
    public class ConsoleCus
    {
        private itemBl itemBl = new itemBl();
        public void MenuCus(User us)
        {

            while (true)
            {
                string[] choice = { "Xem thông tin tài khoản", "Xem danh sách sách", "Xem danh sách sách đã mua", "Exit" };
                short choose = Utility.MenuTemplate("Menu", choice);
                switch (choose)
                {
                    case 1:
                        ShowInfoCustomer(us);
                        continue;
                    case 2:

                        List<Item> items = null;
                        items = itemBl.GetListsItems();
                        int? idItem = ShowlistItems(items);
                        ShowAnItem(idItem);

                        continue;
                    case 3:
                        break;
                    case 4:
                        break;
                }
                break;

            }

        }
        public void ShowInfoCustomer(User us)
        {

            string[] listcol = { "Tên tài khoản", "Tên khách hàng", "Email", "CMND", "Số dư" };

            Utility.InfoCustomer("Thông tin khách hàng", listcol, us);

        }
        public int? ShowlistItems(List<Item> items)
        {
            int? idItem;
            string[] listcol = { "ID Sách", "Tên sách", "Giá", "Tác giả", "Danh mục" };
            idItem = Utility.showListItems("Danh sách sách", listcol, items);
            return idItem;
        }
        public void ShowAnItem(int? idItem)
        {
            Console.Clear();
            Item item = new Item();
            item = itemBl.GetAnItem(idItem);
            var table = new ConsoleTable("Tên", Convert.ToString(item.ItemName));
            table.AddRow("Giá:", Convert.ToString(item.ItemPrice));
            table.AddRow("Tác giả:", item.ItemAuthor);
            table.AddRow("Danh mục:", item.ItemCategory);
            table.AddRow("ISBN:", item.ItemISBN);
            table.AddRow("Ngày phát hành:", item.ItemPublished);
            table.AddRow("Nhà xuất bản:", item.ItemPublisher);
            table.AddRow("Ngôn ngữ:", item.ItemLanguage);
            table.AddRow("Số trang:", item.ItemPages);
            table.Write();
            Console.WriteLine("Mô tả");
            Console.WriteLine(item.ItemDescription);
            Console.WriteLine();
            string choice = Utility.OnlyYN("Nhấn Y để thêm vào giỏ hàng hoặc nhấn N để quay trở lại: ");
            Console.WriteLine();
            switch (choice)
            {
                case "Y":
                    AddToCart(item);
                    break;
                case "N":
                    break;
            }

        }
        public void AddToCart(Item item)
        {

        }
    }
}