create database FlightDB;
use FlightDB;

create table Flight_details (
	Flight_id varchar(20) primary key,
	Flight_name varchar(20),
	Flight_source varchar(20),
	Flight_destination varchar(20),
	Flight_date date,
	Flight_seat int
	);

create table Passengers (
	Pass_id varchar(20) primary key,
	Pass_Name varchar(20),
	DOB date,
	Address varchar(20),
	Phone_no bigint,
	Email varchar(20)
	);
create table Booking (
	Booking_id varchar(20) primary key,
	Flight_id varchar(20),
	Booking_date date,
	Amount int,
	foreign key (Flight_id) references Flight_details(Flight_id)
	);
create table Booking_Details (
	Booking_id varchar(20),
	Pass_id varchar(20),
	primary key (Booking_id, Pass_id),
	foreign key (Booking_id) references Booking(Booking_id),
	foreign key (pass_id) references Passengers(Pass_id)
	);

INSERT INTO Flight_details VALUES 
('F101', 'IndiGo', 'Chennai', 'Delhi', '2025-12-25', 180);

INSERT INTO Passengers VALUES 
('P101', 'Arun', '1995-05-10', 'Coimbatore', 9876543210, 'arun@gmail.com'),
('P102', 'Donna', '1990-08-15', 'Chennai', 9876543211, 'donna@gmail.com');

INSERT INTO Booking VALUES 
('B862','F101','2025-07-10',7000),
('B861', 'F101', '2025-05-09', 6500);

INSERT INTO Booking_Details VALUES 
('B861', 'P101'),
('B862', 'P102');

--1A
create or alter procedure findBookings @BookingID varchar(20)
as
begin
	if exists (select 1 from Booking where Booking_id = @BookingId)
	begin
		select
		p.pass_name + 'bought the plane ticket more than '+
		cast(DATEDIFF(day, b.Booking_date, f.Flight_date) as varchar(10) )+
		' days in advance.' as result
		from
		Booking b
		join Booking_Details bd on b.Booking_id = bd.Booking_id
		join Passengers p on bd.Pass_id = p.Pass_id
		join Flight_details f on B.Flight_id = f.Flight_id
		where b.Booking_id = @BookingId
		order by p.Pass_Name desc;
	end
	else
	begin
		print 'No booking found for the provided booking ID'
	end
end;

EXEC findBookings 'B862';

INSERT INTO Passengers VALUES 
('P103', 'Lina', '1992-12-01', 'Bangalore', 9876543212, 'lina@gmail.com'),
('P104', 'Ravi', '1993-09-18', 'Hyderabad', 9876543213, 'ravi@gmail.com'),
('P105', 'Sara', '1991-03-22', 'Mumbai', 9876543214, 'sara@gmail.com');

INSERT INTO Booking VALUES 
('B863','F101','2025-06-12',6000),  
('B864','F101','2025-06-15',7500),  
('B865','F101','2025-03-01',8000),  
('B866','F101','2025-02-05',5500),  
('B867','F101','2025-01-07',6200);  

INSERT INTO Booking_Details VALUES 
('B863', 'P103'),  
('B864', 'P104'),
('B865', 'P105'),
('B866', 'P104'),
('B867', 'P102');


--2A
with Pass_Booking as (
	select p.Pass_Name as Passenger_Name,
	count(bd.Booking_id) as Number_of_Bookings
	from Passengers p
	join
	Booking_Details bd on p.pass_id = bd.pass_id
	group by
	p.Pass_Name
)
select top 3 *from Pass_Booking order by Number_of_Bookings desc;
