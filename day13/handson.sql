create database world;
use world;
create table city (ID int, NAME varchar(50), COUNTRYCODE varchar(3), 
	DISTRICT varchar(50), POPULATION int);


INSERT INTO city (ID, NAME, COUNTRYCODE, DISTRICT, POPULATION) VALUES
(1661, 'New York', 'USA', 'New York', 8008278),
(1662, 'Los Angeles', 'USA', 'California', 3694820),
(1663, 'Chicago', 'USA', 'Illinois', 2896016),
(1664, 'Houston', 'USA', 'Texas', 1953631),
(1665, 'Phoenix', 'USA', 'Arizona', 1321045),
(1666, 'San Diego', 'USA', 'California', 1223400);

CREATE TABLE Station (
    ID int,
    CITY varchar(50),
    STATE varchar(50),
    LAT_N float,
    LONG_W float
);

INSERT INTO Station (ID, CITY, STATE, LAT_N, LONG_W) VALUES
(1, 'San Francisco', 'California', 37.7749, 122.4194),
(2, 'NY', 'New York', 40.7128, 74.0060),
(3, 'Houston', 'Texas', 29.7604, 95.3698),
(4, 'Austin', 'Texas', 30.2672, 97.7431);

select * from city;
select * from Station;

create table BST (
    N int,
    P int
);

insert into BST (N, P) values
(1, 2),
(3, 2),
(6, 8),
(9, 8),
(2, 5),
(8, 5),
(5, NULL);

select * from BST;

select * from city where COUNTRYCODE='USA' and POPULATION > 100000;

select * from city where COUNTRYCODE='USA' and POPULATION > 120000;

select * from city;

select * from city where ID=1661;

select city, state from Station;

