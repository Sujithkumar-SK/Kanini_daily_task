create database orders;
use orders;

create table tblCustomers (
CustomerID int,
CompanyName varchar(50),
ContactName varchar(50),
ContactTitle varchar(50),
Address varchar(50),
City varchar(20),
Region varchar(20),
PostCode varchar(20),
Country varchar(20),
Phone int,
fax varchar(20)
);

create table tblSupplier (
SupplierID int,
Name varchar(30),
Address varchar(50),
City varchar(30),
Province varchar(30)
);

create table tblShippers (
ShopperID int,
CompanyName varchar(30)
);

create table tblProducts (
ProductID int,
SupplierID int,
CatergoryID int,
ProductName varchar(20),
EnglishName varchar(20),
QuantityPerUnit varchar(20),
UnitPrice decimal(10,2),
UnitInStock int,
UnitsOnOrder int,
ReorderLevel int,
Discontinued varchar(20)
);

create table tblOrders (
OrderID int,
CustomerID int,
EmployeeID int,
ShipName varchar(30),
ShipAddress varchar(50),
ShipCity varchar(20),
shipRegion varchar(20),
ShipPostalCode int,
ShipCountry varchar(20),
ShipVia varchar(20),
OrderDate date,
RequireDate date,
ShippedDate date,
Freight decimal(10,2)
);

create table tblOrderDetails (
OrderID int,
ProductID int,
UnitPrice int,
Quantity int,
Discount int
);


alter table tblCustomers
add constraint DF_country default 'Canada' for country;

alter table tblOrderDetails
add constraint CK_quantity check (Quantity > 0);

exec sp_rename 'tblShippers.ShopperID', 'ShipperID', 'column';

alter table tblShippers
add constraint UQ_compnayName unique (CompanyName);

alter table tblOrders
add constraint CK_shiipingdate check (ShippedDate > OrderDate);

alter table tblShippers
alter column ShipperID int not null;
alter table tblShippers
add constraint PK_ShipperID primary key (ShipperID);

alter table tblCustomers
alter column CustomerID int not null;
alter table tblCustomers
add constraint PK_CustomerID primary key (CustomerID);

alter table tblOrders
alter column OrderID int not null;
alter table tblOrders
add constraint PK_OrderID primary key (OrderID);

alter table tblOrderDetails
alter column OrderID int not null;
alter table tblOrderDetails
alter column ProductID int not null;
alter table tblOrderDetails
add constraint PK_OrderIDProductID primary key (OrderID,ProductID);


alter table tblSupplier
alter column SupplierID int not null;
alter table tblSupplier
add constraint PK_SupplierID primary key (SupplierID);

alter table tblProducts
alter column ProductID int not null;
alter table tblProducts
add constraint PK_ProductID primary key (ProductID);


alter table tblProducts
add constraint FK_SupplierID foreign key (SupplierID)
references tblSupplier(SupplierID);

alter table tblOrderDetails
add constraint FK_ProductID foreign key (ProductID)
references tblProducts(ProductID);

alter table tblOrderDetails
add constraint FK_OrderID foreign key (OrderID)
references tblOrders(OrderID);

alter table tblorders
alter column ShipVia int;
alter table tblOrders
add constraint FK_ShipperID foreign key (ShipVia)
references tblShippers(ShipperID);

alter table tblOrders
add constraint FK_CustomerID foreign key (CustomerID)
references tblCustomers(CustomerID);