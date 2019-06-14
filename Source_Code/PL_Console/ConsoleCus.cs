using System;
using Persistence.MODEL;
using BL;
using System.Collections.Generic;
using ConsoleTables;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.Linq;

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
                OrderBl orderBl = new OrderBl();
                string[] choice = { "Xem thông tin tài khoản", "Xem danh sách sách", $"Xem giỏ hàng ({orderBl.ShowShopingCartByUserId(user.UserId).Count} sp)", "Xem danh sách sách đã mua", "Thoát" };
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

            Utility.InfoCustomer("Thông tin tài khoản", listcol, us);

        }
        public void ShowlistItems()
        {
            List<Item> items = null;

            items = itemBl.GetListItems();
            if (items == null)
            {
                Console.ReadKey();
            }
            else
            {
                while (true)
                {


                    int? idItem;
                    string[] listcol = { "Chọn sản phẩm", "Tìm kiếm", "Quay trở lại" };
                    int choice = Utility.showListItems("Danh sách sách", listcol, items, user.UserId);
                    switch (choice)
                    {
                        case 1:
                            if (items.Count <= 0)
                            {
                                Console.WriteLine("Không có cuốn sách nào");
                                Console.WriteLine("Nhấn phím bất kì để hiển thị danh sách sách");
                                Console.ReadKey();
                                items = itemBl.GetListItems();
                            }
                            else
                            {
                                idItem = Utility.SelectAnItem(items);
                                ShowAnItem(idItem);
                            }
                            continue;
                        case 2:
                            Console.Write("Nhập tên sản phẩm: ");
                            Console.InputEncoding = Encoding.Unicode;
                            Console.OutputEncoding = Encoding.Unicode;
                            string itemName = Console.ReadLine();
                            items = itemBl.SearchItemName(itemName);
                            continue;

                        case 3:
                            break;

                    }
                    break;
                }
            }

        }
        // public string SuggestItemName()
        // {
        //     string[] data = new string[10];
        //     List<string> hihi = new List<string>();
        //     List<Item> items = itemBl.SearchItemName();
        //     foreach (var item in items)
        //     {
        //         hihi.Add(item.ItemName);
        //     }
        //     data = hihi.ToArray();

        //     var builder = new StringBuilder();
        //     var input = Console.ReadKey(intercept: true);

        //     while (input.Key != ConsoleKey.Enter)
        //     {
        //         var currentInput = builder.ToString();
        //         if (input.Key == ConsoleKey.Tab)
        //         {
        //             var match = data.FirstOrDefault(item => item != currentInput && item.StartsWith(currentInput, true, CultureInfo.InvariantCulture));
        //             if (string.IsNullOrEmpty(match))
        //             {
        //                 input = Console.ReadKey(intercept: true);
        //                 continue;
        //             }

        //             ClearCurrentLine();
        //             builder.Clear();

        //             Console.Write(match);
        //             builder.Append(match);
        //         }
        //         else
        //         {
        //             if (input.Key == ConsoleKey.Backspace && currentInput.Length > 0)
        //             {
        //                 builder.Remove(builder.Length - 1, 1);
        //                 ClearCurrentLine();

        //                 currentInput = currentInput.Remove(currentInput.Length - 1);
        //                 Console.Write(currentInput);
        //             }
        //             else
        //             {
        //                 var key = input.KeyChar;
        //                 builder.Append(key);
        //                 Console.Write(key);
        //             }
        //         }

        //         input = Console.ReadKey(intercept: true);
        //     }
        //     Console.Write(input.KeyChar);

        //     return builder.ToString();
        // }
        private static void ClearCurrentLine()
        {
            var currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }
        public void ShowAnItem(int? idItem)
        {
            while (true)
            {

                Console.Clear();
                Console.Clear();
                Item item = new Item();
                RatingBL ratingBL = new RatingBL();
                item = itemBl.GetAnItemById(idItem);
                List<Rating> ratings = ratingBL.GetAllRating(item.ItemId);
                int rateStar = 0;
                if (ratings.Count > 0)
                {
                    foreach (var rate in ratings)
                    {
                        rateStar += rate.RatingStars;
                    }
                    rateStar /= ratings.Count;
                }

                var table = new ConsoleTable("Tên", Convert.ToString(item.ItemName));
                table.AddRow("Giá:", FormatCurrency(item.ItemPrice));
                table.AddRow("Tác giả:", item.ItemAuthor);
                table.AddRow("Đánh giá:", ShowStar(rateStar) == " " ? "Chưa có đánh giá" : ShowStar(rateStar));
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

                OrderBl orderBL = new OrderBl();
                if (item.ItemId != orderBL.CheckItemPurchase(item.ItemId, user.UserId))
                {
                    string[] choice = { "Thêm vào giỏ hàng", "Đánh giá sản phẩm", "Xem tất cả đánh giá", "Quay lại" };
                    short choose = Utility.MenuDetail("Menu", choice);
                    switch (choose)
                    {
                        case 1:
                            AddToCart(item);
                            continue;
                        case 2:
                            RateItem(item);
                            continue;
                        case 3:
                            ShowAllRating(item);
                            continue;
                        case 4:
                            break;
                    }
                }
                else
                {
                    string[] choice = { "Đánh giá sản phẩm", "Xem tất cả đánh giá", "Quay lại" };
                    short choose = Utility.MenuDetail("Menu", choice);
                    switch (choose)
                    {

                        case 1:
                            RateItem(item);
                            continue;
                        case 2:
                            ShowAllRating(item);
                            continue;
                        case 3:
                            break;

                    }
                }

                break;
            }

        }

        public void RateItem(Item item)
        {

            Console.Clear();
            RatingBL ratingBL = new RatingBL();
            Rating rating = new Rating();

            if (ratingBL.CheckItemRatedByUserId(user.UserId, item.ItemId) != null)
            {
                string yorN = Utility.OnlyYN("Bạn đánh giá rồi. Bạn có muốn cập nhập? (Y/N):");
                switch (yorN)
                {
                    case "Y":
                        rating = Utility.MenuRating(user.UserId, item.ItemId);
                        if (ratingBL.UpdateRateItem(rating))
                        {
                            Console.WriteLine("Cập nhập đánh giá thành công");
                            Console.WriteLine("Nhấn phím bất kì để tiếp tục");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Cập nhập đánh giá không thành công");
                        }
                        ShowAllRating(item);
                        break;
                    case "N":
                        break;
                }

            }
            else
            {
                rating = Utility.MenuRating(user.UserId, item.ItemId);
                if (ratingBL.RateItem(rating))
                {
                    Console.WriteLine("Đánh giá thành công");
                    ShowAllRating(item);
                }
                else
                {
                    Console.WriteLine("Đánh giá thất bại");
                }
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
                    table.Write(Format.Minimal);
                    Console.WriteLine("-------------------------------------------------------------------------");
                }
                Console.WriteLine("Nhấn phím bất kì để tiếp tục");
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
            Console.WriteLine("Nhấn phím bất kì để tiếp tục");
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
            Console.WriteLine("Nhấn phím bất kì để tiếp tục");
            Console.ReadKey();
        }
        public void ShopingCart()
        {
            while (true)
            {
                Console.Clear();
                OrderBl orderBL = new OrderBl();
                List<Item> shoppingCart = new List<Item>();
                shoppingCart = orderBL.ShowShopingCartByUserId(user.UserId);
                if (shoppingCart == null)
                {
                    Console.ReadKey();
                    break;
                }
                double total = 0;
                if (shoppingCart.Count <= 0)
                {
                    Console.WriteLine("Chưa có sách");
                    Console.WriteLine("Nhấn phím bất kì để tiếp tục");
                    Console.ReadKey();
                    break;
                }
                else
                {

                    Console.WriteLine($"Bạn có {shoppingCart.Count} quyển sách trong giỏ hàng");
                    var table = new ConsoleTable("Mã sách", "Tên sách", "Giá sách");
                    foreach (var item in shoppingCart)
                    {
                        total = total + (double)item.ItemPrice;
                        table.AddRow(item.ItemId, item.ItemName, FormatCurrency(item.ItemPrice));
                    }
                    table.AddRow("", "", "");
                    table.AddRow("Tổng tiền", "", FormatCurrency(total));
                    table.Write();
                    // Console.WriteLine("Tổng tiền: {0}", FormatCurrency(total));
                    Console.WriteLine("Số tiền trong tài khoản của bạn: {0}", FormatCurrency(user.UserBalance));
                    Console.WriteLine();
                    string[] choice = { "Thanh toán", "Xóa sách khỏi giỏ hàng", "Quay lại" };
                    short choose = Utility.MenuDetail("Menu", choice);
                    string yorn;
                    switch (choose)
                    {
                        case 1:
                            yorn = Utility.OnlyYN("Bạn có muốn thanh toán?(Y/N) ");
                            if (yorn == "Y")
                            {
                                CreateOrder(total);
                            }
                            continue;
                        case 2:
                            Console.Write("Nhập mã sách muốn xóa: ");
                            // int idItem = Int32.Parse(Console.ReadLine());  
                            int idItem;
                            bool c = Int32.TryParse(Console.ReadLine(), out idItem);
                            if (!c)
                            {
                                Console.WriteLine("Bạn chỉ được nhập số. Nhấn nút bất kì để quay lại");
                                Console.ReadKey();
                                continue;
                            }

                            bool y = false;
                            foreach (var item in shoppingCart)
                            {
                                if (item.ItemId == idItem)
                                {
                                    y = true;
                                }
                            }
                            if (y)
                            {

                                string yOrN = Utility.OnlyYN("Bạn có thực sụ muốn xóa(Y/N): ");
                                if (yOrN == "Y")
                                {
                                    orderBL.DeleteItemInShoppingCartByIdItem(idItem);
                                }

                            }
                            else
                            {
                                Console.WriteLine("Không có sản phẩm này trong giỏ hàng");
                                Console.WriteLine("Nhấn phím bất kì để tiếp tục");
                                Console.ReadKey();
                            }

                            continue;
                    }
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
                        Console.Clear();
                        // Console.WriteLine("Mua hàng thành công");
                        userBL.UpdateStatusShoppingCartById(true, user.UserId); // set userShopping cart to 0

                        List<Order> shoppingCart = new List<Order>();
                        shoppingCart = orderBL.ShowOrderUserPaySucess(user.UserId);
                        Console.WriteLine("HÓA ĐƠN");
                        Console.WriteLine("TÊN KHÁCH HÀNG: {0}", shoppingCart[0].OrderUser.Username);
                        Console.WriteLine("EMAIL KHÁCH HÀNG: {0}", shoppingCart[0].OrderUser.UserEmail);

                        Console.WriteLine("MÃ ĐƠN HÀNG: {0}", shoppingCart[0].OrderId);
                        var table = new ConsoleTable("MÃ SÁCH", "TÊN SÁCH", "GIÁ SÁCH");
                        foreach (var item in shoppingCart)
                        {
                            table.AddRow(item.OrderItem.ItemId, item.OrderItem.ItemName, FormatCurrency(item.OrderItem.ItemPrice));
                        }
                        table.AddRow("", "", "");
                        table.AddRow("TỔNG TIỀN", "", FormatCurrency(total));
                        table.AddRow("NGÀY MUA", "", shoppingCart[0].OrderDate?.ToString("yyyy-MM-dd"));
                        table.Write();

                        Console.WriteLine("CÁM ƠN QUÝ KHÁCH");
                        Console.WriteLine("HẸN GẶP LẠI");
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
            Console.WriteLine("Nhấn phím bất kì để tiếp tục");
            Console.ReadKey();

        }
        public void ShowOrder()
        {
            Console.Clear();
            OrderBl orderBL = new OrderBl();
            List<Order> listOrder = new List<Order>();
            listOrder = orderBL.ShowAllItemOrdered(user.UserId);
            if (listOrder == null)
            {
                Console.ReadKey();
            }
            else
            {
                if (listOrder.Count <= 0)
                {
                    Console.WriteLine("Bạn chưa mua gì");
                }
                else
                {
                    var table = new ConsoleTable("Mã sách", "Tên sách", "Ngày mua");
                    foreach (var item in listOrder)
                    {
                        table.AddRow(item.OrderItem.ItemId, item.OrderItem.ItemName, item.OrderDate?.ToString("yyyy-MM-dd"));
                    }
                    table.Write();
                }
                Console.WriteLine("Nhấn phím bất kì để tiếp tục");
                Console.ReadKey();
            }

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
            if (star == 0)
            {
                return " ";
            }
            StringBuilder stringStar = new StringBuilder();

            for (int i = 0; i < star; i++)
            {
                stringStar.Append("☆  ");
            }
            return stringStar.ToString();
        }
        public string FormatCurrency(double price)
        {
            string a = string.Format(new CultureInfo("vi-VN"), "{0:#,##0} VNĐ", price);
            return a;
        }
    }
}