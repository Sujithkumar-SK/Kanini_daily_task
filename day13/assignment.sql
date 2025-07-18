create database fooddelivery;
use fooddelivery;

create table customer (
customer_id varchar(10) primary key,
customer_name varchar(20),
address varchar(20),
phone_no bigint,
email_id varchar(20)
);

create table delivery_partners (
partner_id varchar(10) primary key,
partner_name varchar(20),
phone_no bigint,
rating int
);

create table hotel_details (
hotel_id varchar(10) primary key,
hotel_name varchar(20),
hotel_type varchar(20),
rating int
);

create table orders (
order_id varchar(10) primary key,
customer_id varchar(10) not null,
hotel_id varchar(10) not null,
partner_id varchar(10) not null,
order_date date,
order_amount int,
foreign key (customer_id) references customer(customer_id),
foreign key (hotel_id) references hotel_details(hotel_id),
foreign key (partner_id) references delivery_partners(partner_id)
);


insert into customer (customer_id, customer_name, address, phone_no, email_id) VALUES
('CUST1001', 'THOMAS', 'KOCHI', 9856325486, 'thomas@gmail.com'),
('CUST1002', 'CHARLES', 'ALLEPY', 9856367486, 'charles@gmail.com'),
('CUST1003', 'SUDHAKAR', 'KOCHI', 9856323869, 'sudhakar@gmail.com'),
('CUST1004', 'JAGADISH', 'ERNAKULAM', 9856326666, 'jagadish@gmail.com'),
('CUST1005', 'ALBERT', 'ALLEPY', 9856328659, 'albert@gmail.com'),
('CUST1006', 'ASHWIN DAS', 'KANNUR', 9856325659, 'ashwin@gmail.com');


insert into delivery_partners (partner_id, partner_name, phone_no, rating) VALUES
('PART001', 'Rahul Sharma', 9123456780, 4),
('PART002', 'Priya Singh', 9234567891, 5),
('PART003', 'Amit Kumar', 9345678901, 3),
('PART004', 'Sneha Reddy', 9456789012, 5),
('PART005', 'Vikram Rao', 9567890123, 2);

insert into hotel_details (hotel_id, hotel_name, hotel_type, rating) VALUES
('HOTEL001', 'A2B', 'VEG', 4),
('HOTEL002', 'Paradise Biryani', 'NON-VEG', 5),
('HOTEL003', 'Saravana Bhavan', 'VEG', 3),
('HOTEL004', 'BBQ Nation', 'MIXED', 4),
('HOTEL005', 'Punjabi Dhaba', 'VEG', 4);


insert into orders (order_id, customer_id, hotel_id, partner_id, order_date, order_amount) VALUES
('ORD001', 'CUST1001', 'HOTEL001', 'PART002', '2023-01-15', 250),
('ORD002', 'CUST1002', 'HOTEL003', 'PART001', '2023-01-16', 300),
('ORD003', 'CUST1003', 'HOTEL002', 'PART003', '2023-01-17', 500),
('ORD004', 'CUST1004', 'HOTEL001', 'PART004', '2023-01-17', 200),
('ORD005', 'CUST1005', 'HOTEL005', 'PART002', '2023-01-18', 350);


select partner_id, partner_name, phone_no from delivery_partners
where rating between 3 and 5 order by partner_id;

update customer set phone_no='9876543210' where customer_id='CUST1004';

select * from customer;

select customer_id, customer_name, address, phone_no from customer
where email_id like '%@gmail.com' order by customer_id;

alter table orders
drop constraint FK__orders__customer__3E52440B;

alter table customer
drop constraint PK__customer__CD65CB852987A96D;

update customer set customer_id = substring(customer_id, 5, len(customer_id));
update orders set
customer_id = substring(customer_id, 5, len(customer_id));

alter table customer
alter column customer_id int not null;

alter table orders
alter column customer_id int not null;

alter table customer
add constraint PK__customer__CD65CB852987A96D primary key (customer_id);

alter table orders
add constraint FK__orders__customer__3E52440B foreign key (customer_id)
references customer (customer_id);

exec sp_rename 'hotel_details.rating', 'hotel_rating';

select * from hotel_details


SELECT hotel_name + ' is a ' + hotel_type + ' hotel' AS hotel_info

FROM
    hotel_details
ORDER BY
    HOTEL_INFO DESC;



CREATE TABLE Pizza (
    pizza_id INT PRIMARY KEY,
    pizza_name VARCHAR(100),
    pizza_size VARCHAR(20),
    amount DECIMAL(6,2)
);


INSERT INTO Pizza VALUES
(1, 'Margherita', 'Medium', 150.00),
(2, 'Pepperoni', 'Extra Large', 450.00),
(3, 'Paneer', 'Extra Large', 400.00),
(4, 'Veggie Delight', 'Small', 120.00);

UPDATE pizza
SET amount = amount * 0.97
WHERE pizza_size = 'Extra Large';


select * from Pizza;