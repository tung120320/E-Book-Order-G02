using System;
using Persistence.MODEL;
using BL;
using System.Collections.Generic;
using ConsoleTables;
using System.Text.RegularExpressions;
namespace PL_Console
{
    public class ConsoleCus
    {
        private ItemBl itemBl = new ItemBl();
        private User user = new User();

        private Order order = new Order();


        public void MenuCus(User us)
        {
            user = us;
            while (true)
            {
                string[] choice = { "Xem thông tin tài khoản", "Xem danh sách sách", "Xem giỏ hàng", "Xem danh sách sách đã mua", "Exit" };
                short choose = Utility.MenuTemplate("Menu", choice);
                switch (choose)
                {
                    case 1:
                        ShowInfoCustomer(us);
                        continue;
                    case 2:
                        ShowlistItems();
                        continue;
                    case 3:
                        ShopingCart();
                        continue;
                    case 4:
                        ShowOrder();
                        continue;
                }
                break;

            }

        }
        public void ShowInfoCustomer(User us)
        {

            string[] listcol = { "Tên tài khoản", "Tên khách hàng", "Email", "CMND", "Số dư" };

            Utility.InfoCustomer("Thông tin khách hàng", listcol, us);

        }
        public void ShowlistItems()
        {
            List<Item> items = null;
            items = itemBl.GetListsItems();
            int? idItem;
            while (true)
            {

                string[] listcol = { "ID Sách", "Tên sách", "Giá", "Tác giả", "Danh mục" };
                idItem = Utility.showListItems("Danh sách sách", listcol, items);
                switch (idItem)
                {
                    case 0:
                        break;
                    default:
                        ShowAnItem(idItem);
                        continue;
                }
                break;
            }

        }
        // public void ShowlistItems()
        // {
        //     int numberPage = 0;
        //     List<Item> items = null;
        //     items = itemBl.GetListsItems(numberPage);
        //     int? idItem = 0;
        //     while (true)
        //     {

        //         string[] listcol = { "ID Sách", "Tên sách", "Giá", "Tác giả", "Danh mục" };
        //         Utility.showListItems("Danh sách sách", listcol, items);

        //         Console.Write("Chọn mã sản phẩm muốn mua hoặc ấn 0 để quay trở lại: ");
        //         var ch = Console.ReadKey(false).Key;

        //         // if (idItem < 0 || idItem > items.Count)
        //         // {
        //         //     do
        //         //     {
        //         //         try
        //         //         {
        //         //             Console.Write("#Bạn nhập sai vui lòng nhập lại: ");
        //         //             idItem = Int16.Parse(Console.ReadLine());
        //         //         }
        //         //         catch (System.Exception)
        //         //         {
        //         //             continue;
        //         //         }
        //         //     } while (idItem < 0 || idItem > items.Count);
        //         // }

        //         switch (ch)
        //         {
        //             case ConsoleKey.RightArrow:
        //                 Console.Clear();
        //                 numberPage += 3;
        //                 items = itemBl.GetListsItems(numberPage);
        //                 continue;
        //             case ConsoleKey.LeftArrow:
        //                 Console.Clear();
        //                 if (numberPage < 3)
        //                 {
        //                     numberPage = 0;
        //                     continue;
        //                 }
        //                 else
        //                 {
        //                     numberPage -= 3;
        //                     items = itemBl.GetListsItems(numberPage);
        //                 }
        //                 continue;
        //             case ConsoleKey.Enter:
        //                 try
        //                 {
        //                     Console.Write("Chọn mã sản phẩm muốn mua hoặc ấn 0 để quay trở lại: ");
        //                     idItem = Int16.Parse(Console.ReadLine());
        //                 }
        //                 catch (System.Exception)
        //                 {

        //                 }
        //                 if (idItem < 0 || idItem > items.Count)
        //                 {
        //                     do
        //                     {
        //                         try
        //                         {
        //                             Console.Write("#Bạn nhập sai vui lòng nhập lại: ");
        //                             idItem = Int16.Parse(Console.ReadLine());
        //                         }
        //                         catch (System.Exception)
        //                         {
        //                             continue;
        //                         }
        //                     } while (idItem < 0 || idItem > items.Count);
        //                 }else{
        //                     ShowAnItem(idItem);
        //                 }
        //                 continue;

        //         }

        //         // switch (idItem)
        //         // {
        //         //     case 0:
        //         //         break;
        //         //     default:
        //         //         ShowAnItem(idItem);
        //         //         continue;
        //         // }
        //         break;
        //     }

        // }
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
            WriteLineWordWrap(item.ItemDescription);

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

            OrderBl orderBL = new OrderBl();
            order.OrderUser = new User();
            order.OrderItem = new Item();
            order.ListItems = new List<Item>();
            order.OrderUser.UserId = user.UserId;
            order.OrderItem.ItemId = item.ItemId;


            // GetStatusShoppingCart == false : chưa có order
            // GetStatusShoppingCart == true : order thành công

            if (orderBL.GetStatusShoppingCart(user.UserId))
            {
                try
                {
                    if (orderBL.AddToShoppingcart(order))
                    {
                        Console.WriteLine("Thêm vào giỏ hàng thành công");
                    }
                    else
                    {
                        Console.WriteLine("Sản phẩm đã có trong giỏ hàng");
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }

            }
            else
            {
                orderBL.UpdateUserShoppingCart(false, user.UserId); // set userShopping cart to 1
                order.OrderStatus = 0;
                try
                {
                    if (orderBL.CreateShoppingCart(order))
                    {
                        Console.WriteLine("Chưa có giỏ hàng");
                        Console.WriteLine("Thêm vào giỏ hàng thành công");
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }


            }

            Console.ReadKey();
        }
        public void ShopingCart()
        {
            Console.Clear();
            OrderBl orderBL = new OrderBl();
            List<Item> shoppingCart = new List<Item>();
            shoppingCart = orderBL.ShowShopingCart(user.UserId);
            double total = 0;
            if (shoppingCart.Count <= 0)
            {
                Console.WriteLine("Chưa có sách");
                Console.ReadKey();
            }
            else
            {
                var table = new ConsoleTable("Id sách", "Tên sách", "Giá sách");
                foreach (var item in shoppingCart)
                {
                    total += item.ItemPrice;
                    table.AddRow(item.ItemId, item.ItemName, item.ItemPrice);
                }
                table.Write();
                Console.WriteLine("Tổng tiền: {0}", total);
                string choice = Utility.OnlyYN("Bạn có muốn thanh toán? (Y/N): ");
                Console.WriteLine();
                switch (choice)
                {
                    case "Y":
                        CreateOrder(total);
                        break;
                    case "N":
                        break;
                }
            }
        }
        public void CreateOrder(double total)
        {
            order.OrderUser = new User();
            OrderBl orderBL = new OrderBl();
            order.OrderUser = user;
            if (order.OrderUser.UserBalance < total)
            {
                Console.WriteLine("Bạn không đủ tiền vui lòng nạp thêm tiền");
            }
            else
            {
                try
                {
                    if (orderBL.CreateOrder(order, total))
                    {
                        Console.WriteLine("Mua hàng thành công");
                        orderBL.UpdateUserShoppingCart(true, user.UserId); // set userShopping cart to 0
                    }
                    else
                    {
                        Console.WriteLine("Mua hàng thất bại");
                    }
                }
                catch (System.Exception)
                {

                    throw;
                }

            }
            Console.ReadKey();

        }
        public void ShowOrder()
        {
            OrderBl orderBL = new OrderBl();
            List<Order> listOrder = new List<Order>();
            listOrder = orderBL.ShowOrder(user.UserId);

            var table = new ConsoleTable("Id order", "Tên sách", "Ngày mua");
            foreach (var item in listOrder)
            {
                table.AddRow(item.OrderId, item.OrderItem.ItemName, item.OrderDate);
            }
            table.Write();
            Console.ReadKey();
        }
        public static void WriteLineWordWrap(string paragraph, int tabSize = 8)
        {
            string[] lines = paragraph
                .Replace("\t", new String(' ', tabSize))
                .Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                string process = lines[i];
                List<String> wrapped = new List<string>();

                while (process.Length > Console.WindowWidth)
                {
                    int wrapAt = process.LastIndexOf(' ', Math.Min(Console.WindowWidth - 1, process.Length));
                    if (wrapAt <= 0) break;

                    wrapped.Add(process.Substring(0, wrapAt));
                    process = process.Remove(0, wrapAt + 1);
                }

                foreach (string wrap in wrapped)
                {
                    Console.WriteLine(wrap);
                }

                Console.WriteLine(process);
            }
        }
    }
}