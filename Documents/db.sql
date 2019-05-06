drop database if exists EBooksStore;
create database if not exists EBooksStore;
use EBooksStore;
create table if not exists Users(
	userId int primary key auto_increment,
    userName varchar(50) not null,
    userPassword varchar(50) not null,
    nameUser nvarchar(50) not null,
    userEmail varchar(50) not null,
    userIdCardNo varchar(12) not null,
    userBalance decimal(10.2) not null,
    userLevel int(1) not null
);
create table if not exists Companies(
	companyId int primary key auto_increment,
    companyName nvarchar(100) not null,
    companyAdress nvarchar(255) not null,
    companyPhone varchar(20)  not null
);
create table if not exists Orders(
	orderId int primary key auto_increment,
    userId int not null,
    orderDate datetime not null,
    OrderPaiddate datetime,
    companyId int not null,
    constraint fk_Orders_Companies foreign key(companyId) references Companies(companyId),
    constraint fk_Orders_Users foreign key(userId) references Users(userId)
);
create table if not exists Items(
	itemId int primary key auto_increment,
    itemName nvarchar(100) not null,
    itemPrice decimal (10.2) not null,
    itemAuthor nvarchar(50) not null,
    itemCategory nvarchar(50),
    itemDescription text,
    itemPreview text not null,
    itemISBN int(13),
    itemPublished datetime,
    itemPublisher nvarchar(150),
    itemLanguage varchar(20),
    itemPages int
);
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
    constraint primary key(itemId,userId),
    constraint fk_Ratings_Items foreign key(itemId) references Items(itemId),
    constraint fk_Ratings_Users foreign key(userId) references Users(userId)
    );