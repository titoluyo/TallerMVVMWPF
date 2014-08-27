
--Run this script to setup the initial tables and data

create table Customer
(
	CustomerId int identity(1,1) primary key,
	FirstName varchar(100) not null,
	LastName varchar(100) not null
)

create table Orders
(
	OrderId int identity(1,1) primary key,
	CustomerId int references Customer(CustomerId) on delete cascade,
	[Description] varchar(250) not null,
	Quantity int not null
)

go

--prepopulate with initial data
insert into Customer(FirstName, LastName)
values('Joe', 'Smith')

insert into Orders(CustomerId, [Description], Quantity)
values(1, 'Hammer', 1)

insert into Orders(CustomerId, [Description], Quantity)
values(1, 'Oil Filter', 2)

insert into Customer(FirstName, LastName)
values('Mary', 'Valentine')

insert into Orders(CustomerId, [Description], Quantity)
values(2, 'Nail Polish', 1)

insert into Orders(CustomerId, [Description], Quantity)
values(2, 'Spoons', 5)

go