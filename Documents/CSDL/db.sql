drop database if exists EBooksStore;
create database if not exists EBooksStore;
use EBooksStore;
create table if not exists Users(
	userId int primary key auto_increment,
    userAccount varchar(50) not null,
    userPassword varchar(50) not null,
    userName nvarchar(50) not null,
    userEmail varchar(50) not null,
    userIdCardNo varchar(12) not null,
    userBalance decimal(10.2) not null
);
insert into Users(userAccount,userPassword,userName,userEmail,userIdCardNo,userBalance)
values 
('abc','123','abc','abc@gmail.com','4389768587','1'),
('ghi','452','ghi','ghi@gmail.com','465785762','0'),
('mnp','845','nga','nga@gmail.com','439540524','0'),
('hsadfg','2047238','dfg','dfg@gmail.com','843755762','0'),
('fgah','74365','ghgshe','ghgshe@gmail.com','49372898','0');

create table if not exists Orders(
	orderId int primary key auto_increment,
    userId int not null,
    orderDate datetime not null,
    OrderPaiddate datetime,
    constraint fk_Orders_Users foreign key(userId) references Users(userId)
);
create table if not exists Items(
	itemId int primary key auto_increment,
    itemName nvarchar(100) not null,
    itemPrice decimal (10,3) not null,
    itemAuthor nvarchar(50) not null,
    itemCategory nvarchar(50),
    itemDescription text,
    itemISBN int(13),
    itemPublished datetime,
    itemPublisher nvarchar(150),
    itemLanguage varchar(20),
    itemPages int
);
insert into Items(itemName,itemPrice,itemAuthor,itemCategory,itemDescription,itemISBN,itemPublished,itemPublisher,itemLanguage,itemPages)
values
(' Learning Bootstrap 4 by Building Projects','700.000','Eduonix Learning Solutions','Bootstrap','Bootstrap, the world`s most popular frontend framework, is an open source toolkit for building web applications with HTML, CSS, and JavaScript.
Learning Bootstrap 4 by Building Projects covers the essentials of Bootstrap 4 along with best practices. The book begins by introducing you to the latest features of Bootstrap 4. You will learn different elements and components of Bootstrap, such as the strict grid system, Sass, which replaced Less, flexbox, Font Awesome, and cards. As you make your way through the chapters, you will use a template that will help you to build different kinds of real-world websites, such as a social media website, a company landing page, a media hosting website, and a profile page, with ease.
By the end of this book, you will have built websites that are visually appealing, responsive, and robust.','1789343259','2018-04-03','Packt Publishing','English','218'),
(' Machine Learning and AI for Healthcare','649.000','Arjun Panesar','Big Data','Explore the theory and practical applications of artificial intelligence (AI) and machine learning in healthcare. This book offers a guided tour of machine learning algorithms, architecture design, and applications of learning in healthcare and big data challenges.

You`ll discover the ethical implications of healthcare data analytics and the future of AI in population and patient health optimization. You`ll also create a machine learning model, evaluate performance and operationalize its outcomes within your organization. 

Machine Learning and AI for Healthcare provides techniques on how to apply machine learning within your organization and evaluate the efficacy, suitability, and efficiency of AI applications. These are illustrated through leading case studies, including how chronic disease is being redefined through patient-led data learning and the Internet of Things.

Gain a deeper understanding of key machine learning algorithms and their use and implementation within wider healthcare; Implement machine learning systems, such as speech recognition and enhanced deep learning/AI; Select learning methods/algorithms and tuning for use in healthcare; Recognize and prepare for the future of artificial intelligence in healthcare through best practices, feedback loops and intelligent agents.','1484237986','2019-5-4','Apress','English','368'),
(' Arduino Applied','579.000','	Neil Cameron','Arduino','Extend the range of your Arduino skills, incorporate the new developments in both hardware and software, and understand how the electronic applications function in everyday life. This project-based book extends the Arduino Uno starter kits and increases knowledge of microcontrollers in electronic applications.

Learn how to build complex Arduino projects, break them down into smaller ones, and then enhance them, thereby broadening your understanding of each topic.You`ll use the Arduino Uno in a range of applications such as a blinking LED, route mapping with a mobile GPS system, and uploading information to the internet.

You`ll also apply the Arduino Uno to sensors, collecting and displaying information, Bluetooth and wireless communications, digital image captures, route tracking with GPS, controlling motors, color and sound, building robots, and internet access. With Arduino Applied, prior knowledge of electronics is not required, as each topic is described and illustrated with examples using the Arduino Uno.

Set up the Arduino Uno and its programming environment; Understand the application of electronics in every day systems; Build projects with a microcontroller and readily available electronic components.','1484239598','2019-8-9','Apress','English','552'),
('C++ Concurrency in Action, 2nd Edition','1200.000','Anthony Williams','C++','This bestseller has been updated and revised to cover all the latest changes to C++ 14 and 17! C++ Concurrency in Action, 2nd Edition teaches you everything you need to write robust and elegant multithreaded applications in C++17.

You choose C++ when your applications need to run fast. Well-designed concurrency makes them go even faster. C++ 17 delivers strong support for the multithreaded, multiprocessor programming required for fast graphic processing, machine learning, and other performance-sensitive tasks. This exceptional book unpacks the features, patterns, and best practices of production-grade C++ concurrency.

C++ Concurrency in Action, 2nd Edition is the definitive guide to writing elegant multithreaded applications in C++. Updated for C++ 17, it carefully addresses every aspect of concurrent development, from starting new threads to designing fully functional multithreaded algorithms and data structures. Concurrency master Anthony Williams presents examples and practical tasks in every chapter, including insights that will delight even the most experienced developer.','1617294691','	2019-07-15','Manning','English','	592'),

('Sách Thiên Cơ','78.734','Túng Mã Càn Khôn','Văn học','Một cuốn sách cổ ngàn năm, tiết lộ về một chức nghiệp thần bí rất đỗi cổ xưa, được các đời hoàng đế vô cùng trọng vọng, song đã tuyệt tích trong dân gian - Thiên Cơ đại phu.

Trong cuộc cách mạng Văn hóa thảm khốc, nhân vật chính của cuốn sách, Kỳ Thiên Hạ, đã vô tình tìm thấy cuốn sách này. Nhờ vốn cổ văn có được từ người cha trí thức đã bị tù đày, Kỳ Thiên Hạ bắt tay vào khám phá cổ thư, dấn thân vào những chuyến thám hiểm đầy bất trắc, tiếp nối hành trình của các thế hệ Thiên Cơ đại phu, tìm kiếm bí mật về hào thứ tư đã biến mất từ thiên cổ...','251060782','2015-03-01','Nhà Xuất Bản Văn Học','Tiếng Việt','532'),
('Linux All-In-One For Dummies, 6th Edition','54.000','Emmett Dulaney','Linux','Inside, over 800 pages of Linux topics are organized into eight task-oriented mini books that help you understand all aspects of the latest OS distributions of the most popular open-source operating system in use today. Topics include getting up and running with basics, desktops, networking, internet services, administration, security, scripting, Linux certification, and more. 

This new edition of Linux All-in-One For Dummies has a unique focus on Ubuntu, while still including coverage of Debian, Red Hat, SuSE, and others. The market is looking for administrators, and part of the qualifications needed for job openings is the authentication of skills by vendor-neutral third parties (CompTIA/Linux Professional Institute) - and that`s something other books out there don`t address.

Install and configure peripherals, software packages, and keep everything current; Connect to the internet, set up a local area network (including a primer on TCP/IP, and managing a local area network using configuration tools and files); Browse the web securely and anonymously; Get everything you need to pass your entry-level Linux certification exams.','250360782','2018-03-04','Wiley','English','560'),
('Người Đầu Tiên Trong Danh Sách','78.400','Magdalena Witkiewicz','Văn học','MagdalenaWitkiewicz- tác giả các cuốn sách đã in tại Nhà xuất bản Trẻ: Trường học dành cho các bà vợ, Lâu đài cát… hiện vẫn đang là tác giả có sách bán chạy nhất của văn học Ba Lan hiện đại.
Tác phẩm mới của chị - Người Đầu Tiên Trong Danh Sách - là một tác phẩm ấm áp, xúc động về tình yêu, tình bạn.
Thông điệp của tác phẩm: Không bao giờ là quá muộn để thay đổi một điều gì đó trong cuộc sống. Tình cảm thiêng liêng sẽ đưa con người vượt qua mọi lốc xoáy của số phận "Bạn không thể thay đổi quá khứ, nhưng bạn vẫn còn cơ hội để thay đổi tương lai…"','25196120','2016-10-5','Nhà Xuất Bản Trẻ','Tiếng Việt','360'),
('Đọc Sách, Điểm Sách','32.392','Nguyễn Ngọc Sơn','Kĩ năng sống','Đọc sách - đó là bạn đang trải nghiệm. Điểm sách - đó là bạn đang đúc kết trải nghiệm của mình. Người thì dùng nó để lưu giữ những cảm xúc bên trong, cũng có có người dùng nó để biểu đạt ra bên ngoài, khuyến khích người khác tìm đọc và rút tỉa những bài học khôn ngoan cho riêng mình
Nhưng không phải, nó lúc nào việc viết bài điểm sách cũng dễ dàng lay động, cuốn hút người khác tìm đọc. Nó phụ thuộc vào mức độ cảm thấu nội dung của bạn lẫn cách bạn truyền tải nó trên mặt giấy. Bạn phải hiểu đối tượng mà bạn muốn giới thiệu là ai, cuốn sách này thật sự giúp ích được gì cho họ. Và một điều quan trọng hơn là năng lực sử dụng ngôn ngữ để khiến bài điểm sách trở nên sinh động và có sức hấp dẫn. 
Vậy, 
Làm thể nào để đọc cho dễ hiểu và viết cho thật hay? 
Review là điểm sách hay giới thiệu sách? 
Đặt tiêu đề thế nào cho cuốn hút?
Các bí quyết để viết một bài review hấp dẫn. Tất cả câu hỏi trên sẽ được trình bày một cách rõ ràng và chi tiết trong Đọc sách - Điểm sách.','71221319','2019-03-7','Nhà Xuất Bản Trẻ','Tiếng Việt','164'),
('Combo Bộ Sách Bitcoin','339.349','Mark Gates','Sách kinh tế','Tiền điện tử, với đại diện tiêu biểu nhất là Bitcoin, đang là mối quan tâm hàng đầu của giới tài 
chính toàn cầu. Khả năng thanh toán bằng tiền điện tử mở ra hàng loạt tiềm năng cho thương mại và thay đổi toàn diện thói quen
 tiêu dùng của con người. Hạt nhân của công nghệ hứa hẹn rung chuyển thế giới này được gọi là Blockchain.
 Blockchain được giới công nghệ đánh giá là phát kiến vĩ đại nhất sau khi mạng Internet ra đời.
 Ứng dụng phổ biến nhất của nó là các loại tiền điện tử nổi tiếng (Bitcoin, Ethereum, Ripple...) đang làm mưa làm gió trên thị trường.
 Nhưng nghịch lý là, lại rất ít người nắm được bản chất của tiền điện tử và Blockchain.
 Rốt cuộc, Blockchain là gì? Nó hoạt động như thế nào? Blockchain thật sự là một đột phá trong công nghệ hay chỉ là một trào lưu nhất thời? 
 Blockchain có liên hệ như thế nào với Bitcoin? Liệu Blockchain có nắm giữ tiềm năng thay đổi thế giới?... 
 Thực chất, những bài viết cung cấp thông tin về Blockchain và tiền điện tử không thiếu trên các website hay mạng xã hội, nhưng lại chưa đủ tính bao quát và còn khó tiếp thu. Đó là lý do cuốn sách “BLOCKCHAIN: Bản chất của Blockchain, Bitcoin, tiền điện tử, hợp đồng thông minh và tương lai của tiền tệ” của Mark Gates ra đời. Gates cung cấp một bản tóm lược dễ hiểu nhất, cung cấp nền tảng thiết yếu cho những người mới bắt đầu và cả những ai muốn nghiên cứu sâu hơn về công nghệ Blockchain và tiền điện tử. Đọc cuốn sách này, bạn sẽ hiểu được Blockchain dưới nhiều khía cạnh, trong đó không chỉ có những lời ngợi khen mà còn không thiếu các chỉ trích, bình luận trái chiều.','98844267','2017-7-5','Nhà Xuất Bản Lao Động','Tiếng Việt','452');

create table if not exists OrderDetails(
	orderId int not null,
    itemId int not null,
    OrderDetailsCount int not null,
    constraint primary key(orderId,itemId),
    constraint fk_OrderDetails_Orders foreign key(orderId) references Orders(orderId),
    constraint fk_OrderDetails_Items foreign key (itemId) references Items(itemId)
);
create table if not exists Ratings(
	itemId int ,
    userId int,
    ratingStart tinyint,
    ratingTitle nvarchar(255),
    ratingContent text,
    ratingDate datetime,
    constraint primary key(itemId,userId),
    constraint fk_Ratings_Items foreign key(itemId) references Items(itemId),
    constraint fk_Ratings_Users foreign key(userId) references Users(userId)
    );
    select * from users;
   