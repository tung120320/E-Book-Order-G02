using System;
using Persistence.MODEL;
using BL;
using System.Collections.Generic;
using ConsoleTables;
using System.Text.RegularExpressions;
using System.Text;
namespace PL_Console
{
    public class ConsoleCus
    {
        private ItemBl itemBl = new ItemBl();
        private UserBL userBL = new UserBL();
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
            items = itemBl.GetListItems();
            int? idItem;
            while (true)
            {

                string[] listcol = { "Chọn sản phẩm", "Tìm kiếm", "Quay trở lại" };
                int choice = Utility.showListItems("Danh sách sách", listcol, items);
                switch (choice)
                {
                    case 1:
                        idItem = Utility.SelectAnItem(items);
                        ShowAnItem(idItem);
                        continue;
                    case 2:
                        Console.Write("Nhập tên sản phẩm ");
                        string itemName = Console.ReadLine();
                        items = itemBl.SearchItemName(itemName);
                        continue;
                    case 3:
                        break;
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
            while (true)
            {
                Console.Clear();
                Item item = new Item();
                item = itemBl.GetAnItemById(idItem);
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



                string[] choice = { "Thêm vào giỏ hàng", "Xóa khỏi giỏ hàng", "Đánh giá sản phẩm", "Xem tất cả đánh giá", "Quay lại" };
                short choose = Utility.MenuDetail("Menu", choice);
                switch (choose)
                {
                    case 1:
                        AddToCart(item);
                        continue;
                    case 2:
                        DeleteItemFromSPC(item);
                        continue;
                    case 3:
                        RateItem(item);
                        continue;
                    case 4:
                        ShowAllRating(item);
                        continue;
                    case 5:
                        break;
                }
                break;
            }

            // string choice = Utility.OnlyYN("Nhấn Y để thêm vào giỏ hàng hoặc nhấn N để quay trở lại: ");
            // Console.WriteLine();
            // switch (choice)
            // {
            //     case "Y":
            //         AddToCart(item);
            //         break;
            //     case "N":
            //         break;
            // }

        }

        public void RateItem(Item item)
        {
            Console.Clear();
            RatingBL ratingBL = new RatingBL();
            Rating rating = new Rating();

            rating.ItemId = item.ItemId;
            rating.UserId = user.UserId;
            Console.Write("Nhập số sao ");
            rating.RatingStars = Convert.ToInt32(Console.ReadLine());
            Console.Write("Nhập tiêu đề ");
            rating.RatingTitle = Console.ReadLine();
            Console.Write("Nhập nội dung ");
            rating.RatingContent = Console.ReadLine();
            rating.RatingDate = DateTime.Now;

            if (ratingBL.RateItem(rating))
            {
                Console.WriteLine("Đánh giá thành công");
                ShowAllRating(item);
            }
            else
            {
                if (ratingBL.UpdateRateItem(rating))
                {
                    Console.WriteLine("Cập nhập đánh giá thành công");
                }
                else
                {
                    Console.WriteLine("Cập nhập đánh giá không thành công");
                }

                Console.ReadKey();
            }
        }
        public void ShowAllRating(Item item)
        {
            Console.Clear();
            RatingBL ratingBL = new RatingBL();
            UserBL userBL = new UserBL();

            List<Rating> ratings = null;
            ratings = ratingBL.GetAllRating(item.ItemId);
            if (ratings.Count <= 0)
            {
                Console.WriteLine("Chưa có đánh giá");
                Console.ReadKey();
            }
            else
            {
                foreach (var rating in ratings)
                {
                    var table = new ConsoleTable("Tên", userBL.GetUserById(rating.UserId).Username); //lấy tên user qua userBL theo Id
                    table.AddRow("Số sao", ShowStar(rating.RatingStars));
                    table.AddRow("Tiêu đề", rating.RatingTitle);
                    table.AddRow("Nội dung", rating.RatingContent);
                    table.Write();
                    Console.WriteLine("====================================");
                }
                Console.ReadKey();
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

            // user.UserShoppingCart == false : chưa có order
            // user.UserShoppingCart == true : order thành công

            if (userBL.GetUserById(user.UserId).UserShoppingCart)
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
                userBL.UpdateStatusShoppingCartById(false, user.UserId); // set userShopping cart to 1
                order.OrderStatus = 0;
                try
                {
                    if (orderBL.CreateShoppingCart(order))
                    {

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
        public void DeleteItemFromSPC(Item item)
        {

            OrderBl orderBL = new OrderBl();
            if (orderBL.DeleteItemInShoppingCartByIdItem(item.ItemId))
            {
                Console.WriteLine("Xóa khỏi giỏ hàng thành công");
            }
            else
            {
                Console.WriteLine("Sản phẩm chưa có trong trỏ hàng");
            }
            Console.ReadKey();
        }
        public void ShopingCart()
        {
            Console.Clear();
            OrderBl orderBL = new OrderBl();
            List<Item> shoppingCart = new List<Item>();
            shoppingCart = orderBL.ShowShopingCartByUserId(user.UserId);
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
                    total = total + (double)item.ItemPrice;
                    table.AddRow(item.ItemId, item.ItemName, item.ItemPrice);
                }
                table.Write();
                Console.WriteLine("Tổng tiền: {0}", total);
                string[] choice = { "Thanh toán", "Xóa tất cả khỏi giỏ hàng", "Quay lại" };
                short choose = Utility.MenuDetail("Menu", choice);
                switch (choose)
                {
                    case 1:
                        CreateOrder(total);
                        break;
                    case 2:
                        string yorn;
                        yorn = Utility.OnlyYN("Bạn có thực sự muốn xóa?(Y/N) ");
                        if (yorn == "Y")
                        {
                            if (orderBL.DeleteAllItemInShoppingCartByUserID(user.UserId))
                            {
                                Console.WriteLine("Xóa thành công");
                            }
                            else
                            {
                                Console.WriteLine("Xóa không thành công");
                            }

                        }
                        Console.ReadLine();
                        break;
                }
                // string choice = Utility.OnlyYN("Bạn có muốn thanh toán? (Y/N): ");
                // Console.WriteLine();
                // switch (choice)
                // {
                //     case "Y":
                //         CreateOrder(total);
                //         break;
                //     case "N":
                //         break;
                // }
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
                        userBL.UpdateStatusShoppingCartById(true, user.UserId); // set userShopping cart to 0

                        // Order order = null;
                        // order = orderBL.ShowOrderUserPaySucess(user.UserId);
                        // if (order == null)
                        // {
                        //     Console.WriteLine("Mua hàng thất bại");
                        // }
                        // else
                        // {
                        //     var table = new ConsoleTable("Id order", "Tên sách", "Ngày mua");

                        //     table.AddRow(order.OrderId, order.OrderItem.ItemName, order.OrderDate);

                        //     table.Write();
                        // }
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
            Console.Clear();
            OrderBl orderBL = new OrderBl();
            List<Order> listOrder = new List<Order>();
            listOrder = orderBL.ShowOrderByUserId(user.UserId);
            if (listOrder.Count <= 0)
            {
                Console.WriteLine("Bạn chưa mua gì");
            }
            else
            {
                var table = new ConsoleTable("Id order", "Tên sách", "Ngày mua");
                foreach (var item in listOrder)
                {
                    table.AddRow(item.OrderId, item.OrderItem.ItemName, item.OrderDate);
                }
                table.Write();
            }
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
        public string ShowStar(int star)
        {
            StringBuilder stringStar = new StringBuilder();

            for (int i = 0; i < star; i++)
            {
                stringStar.Append("☆  ");
            }
            return stringStar.ToString();
        }
    }
}