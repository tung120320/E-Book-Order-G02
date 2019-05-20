using System;
using Persistence.MODEL;
using BL;
using System.Collections.Generic;
using ConsoleTables;
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
                        List<Item> items = null;
                        items = itemBl.GetListsItems();
                        ShowlistItems(items);
                        continue;
                    case 3:
                        ShopingCart();
                        continue;
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
        public void ShowlistItems(List<Item> items)
        {
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

            OrderBl orderBL = new OrderBl();
            order.OrderUser = new User();
            order.OrderItem = new Item();
            order.ListItems = new List<Item>();
            int orderStatus;
            try
            {
                var listOrder = orderBL.GetAllOrder(user.UserId);
                orderStatus = listOrder.FindIndex(x => x.OrderStatus == 0);
            }
            catch (System.Exception)
            {

                throw;
            }
            // orderStatus == -1 : chưa tạo order
            // orderStatus == 0 : đẫ tạo order
            // orderStatus == 1 : order thành công
            Console.WriteLine(orderStatus);
            if (orderStatus == -1)
            {
                order.OrderStatus = 0;
                order.OrderUser.UserId = user.UserId;
                order.OrderItem = item;
                if (orderBL.CreateShoppingCart(order))
                {
                    Console.WriteLine("Chưa có giỏ hàng");
                    Console.WriteLine("Thêm vào giỏ hàng thành công");
                }
            }
            else if (orderStatus == 0)
            {
                order.OrderItem.ItemId = item.ItemId;
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

            Console.ReadKey();
        }
        public void ShopingCart()
        {
            Console.Clear();
            OrderBl orderBL = new OrderBl();
            List<Item> Listitems = new List<Item>();
            Listitems = orderBL.ShowShopingCart(user.UserId);
            double total = 0;
            if (Listitems.Count <= 0)
            {
                Console.WriteLine("Chưa có sách");
            }
            else
            {
                var table = new ConsoleTable("Id sách", "Tên sách", "Giá sách");
                foreach (var item in Listitems)
                {
                    total += item.ItemPrice;
                    table.AddRow(item.ItemId, item.ItemName, item.ItemPrice);
                }
                table.Write();
                Console.WriteLine("Tổng tiền: {0}", total);
            }
            string choice = Utility.OnlyYN("Bạn có muốn thanh toán? (Y/N)");
            Console.WriteLine();
            switch (choice)
            {
                case "Y":
                    CreateOrder();
                    break;
                case "N":
                    break;
            }
        }
        public void CreateOrder(){
            
        }
    }
}