-- 1. Create table with Foreign Key constraint
create table salesman (
SALESMAN_ID int primary key,
NAME varchar(30),
CITY varchar(15),
COMMISSION decimal(5,2)
);

create table orders (
ORD_NO int primary key,
PURCH_AMT decimal(8,2),
ORD_DATE date,
CUSTOMER_ID int,
SALESMAN_ID int foreign key references salesman(SALESMAN_ID)
)
go

sp_help 'orders'
GO

--2. Alter Supplier table with Check Constraint
alter table tblSupplier
add contact varchar(10)
go

alter table tblSupplier
add constraint CHK_contact_lenght
check (len(contact)=10)
go

sp_help 'tblSupplier';
GO

--3. Hotels that took order more than five times

use fooddelivery
go

INSERT INTO orders (order_id, customer_id, hotel_id, partner_id, order_date, order_amount) VALUES
('ORD006', 1001, 'HOTEL001', 'PART001', '2023-01-19', 300),
('ORD007', 1002, 'HOTEL001', 'PART002', '2023-01-20', 250),
('ORD008', 1003, 'HOTEL001', 'PART003', '2023-01-21', 180),
('ORD009', 1004, 'HOTEL001', 'PART004', '2023-01-22', 500),
('ORD010', 1005, 'HOTEL001', 'PART005', '2023-01-23', 230);
GO


select
h.hotel_id,h.hotel_name, count(o.order_id) as no_of_orders
from hotel_details h join orders o 
on h.hotel_id = o.hotel_id
group by h.hotel_id, h.hotel_name
having count(o.order_id) > 5
order by
h.hotel_id asc
go

--4. Hotels not taken orders in a specific month

INSERT INTO orders (order_id, customer_id, hotel_id, partner_id, order_date, order_amount) VALUES
('ORD011', 1002, 'HOTEL002', 'PART001', '2019-05-10', 300),
('ORD012', 1003, 'HOTEL002', 'PART002', '2019-05-11', 250),
('ORD013', 1004, 'HOTEL003', 'PART003', '2019-05-15', 400);
GO


select h.hotel_id,h.hotel_name,h.hotel_type
from hotel_details h where
h.hotel_id not in (select distinct o.hotel_id
					from orders o where month(o.order_date)
					=5 and year(o.order_date)=2019
					)
order by h.hotel_id asc
go

--5. Order details

select
o.order_id,c.customer_name,h.hotel_name,o.order_amount
from orders o
join customer c on o.customer_id = c.customer_id
join hotel_details h on o.hotel_id = h.hotel_id
order by o.order_id asc
go