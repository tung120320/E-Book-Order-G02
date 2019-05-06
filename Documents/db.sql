drop database if exists EBooksStore;
create database if not exists EBooksStore;
use EBooksStore;
create table if not exists Users(
	userId int primary key auto_increment,
    userName varchar(30) not null,
    userPassword varchar(30) not null,
    nameUser nvarchar(30) not null,
    userEmail varchar(30) not null,
    userIdCardNo int(10) not null,
    userBalance decimal(10.2) not null,
    userLevel int(1) not null
);
create table if not exists Companys(
	companyId int primary key auto_increment,
    companyName nvarchar(100) not null,
    companyAdress text not null,
    companyPhone int not null
);
create table if not exists Orders(
	orderId int primary key auto_increment,
    userId int not null,
    orderDate datetime not null,
    OrderPaiddate datetime,
    companyId int not null,
    constraint fk_Orders_Companys foreign key(companyId) references Companys(companyId),
    constraint fk_Orders_Users foreign key(userId) references Users(userId)
);
create table if not exists Items(
	itemId int primary key auto_increment,
    itemName nvarchar(60) not null,
    itemPrice decimal (10.2) not null,
    itemAuthor nvarchar(30) not null,
    itemCategory nvarchar(30),
    itemDescription text,
    itemPreview text not null,
    itemISBN int(13),
    itemPublished datetime,
    itemPublisher nvarchar(50),
    itemLanguage varchar(20),
    itemPages int
);
create table if not exists OrderDetails(
	orderId int not null,
    itemId int not null,
    OrderDetailsCount int not null,
    constraint fk_OrderDetails_Orders foreign key(orderId) references Orders(orderId),
    constraint fk_OrderDetails_Items foreign key (itemId) references Items(itemId)
);
