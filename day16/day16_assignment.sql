--1. Procedure to display the Employees of a specific Department

CREATE TABLE employee (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    DepartmentID INT
);

INSERT INTO employee (EmployeeID, FirstName, LastName, DepartmentID)
VALUES
(1, 'John', 'Doe', 101),
(2, 'Jane', 'Smith', 102),
(3, 'Mike', 'Johnson', 101),
(4, 'Emily', 'Davis', 103),
(5, 'David', 'Brown', 102);

create or alter procedure EmployeesDept(@DeptNo INT)
as
begin
    select lastname as name
    from employee
    where DepartmentID = @DeptNo;
end

EmployeesDept 102
drop table employee;

--2. Procedure to display all the Departments

CREATE TABLE Department (
    deptno CHAR(3) PRIMARY KEY,
    deptname VARCHAR(36) NOT NULL,
    mgrno CHAR(6),
    admrdept CHAR(3) NOT NULL,
    location CHAR(16)
);

CREATE TABLE Employee (
    empno INT NOT NULL PRIMARY KEY,
    firstname VARCHAR(12) NOT NULL,
    midinit CHAR(1) NOT NULL,
    lastname VARCHAR(15) NOT NULL,
    workdept CHAR(3),  
    phoneno CHAR(10) CHECK (phoneno >= '0000000000' AND phoneno <= '9999999999'),
    hiredate DATE,
    job CHAR(9),
    edlevel SMALLINT,
    sex CHAR(1),
    birthdate DATE,
    salary DECIMAL(9,2),
    bonus DECIMAL(9,2),
    comm DECIMAL(9,2),
    FOREIGN KEY (workdept) REFERENCES Department(deptno)
);


INSERT INTO Department (deptno, deptname, mgrno, admrdept, location)
VALUES
('D01', 'Finance', '000101', 'D01', 'Mumbai'),
('D02', 'Marketing', '000102', 'D01', 'Chennai'),
('D03', 'Engineering', '000103', 'D02', 'Bangalore'),
('D04', 'HR', '000104', 'D01', 'Delhi');

INSERT INTO Employee (empno, firstname, midinit, lastname, workdept, phoneno, hiredate, job, edlevel, sex, birthdate, salary, bonus, comm)
VALUES
(1, 'John', 'A', 'Doe', 'D01', '9876543210', '2020-01-15', 'Manager', 18, 'M', '1990-05-20', 75000.00, 5000.00, 2000.00),
(2, 'Jane', 'B', 'Smith', 'D02', '9123456780', '2019-03-10', 'Analyst', 16, 'F', '1992-08-25', 62000.00, 3000.00, 1500.00),
(3, 'Emily', 'C', 'Clark', 'D01', '9012345678', '2021-06-01', 'Clerk', 14, 'F', '1995-01-30', 45000.00, 1000.00, 800.00),
(4, 'David', 'D', 'Lee', 'D03', '9009876543', '2018-11-20', 'Engineer', 17, 'M', '1988-12-05', 67000.00, 4000.00, 2200.00),
(5, 'Anita', 'E', 'Kumar', 'D04', '9991234567', '2022-04-12', 'Recruiter', 15, 'F', '1994-07-19', 48000.00, 1200.00, 900.00);


create procedure AvailableDepartments
as
begin
    select deptname as name
    from Department;
end

EXEC AvailableDepartments;


--3. Bike-Stores Stored Procedure

CREATE TABLE brands (
    brand_id INT PRIMARY KEY,
    brand_name VARCHAR(50) NOT NULL
);

CREATE TABLE categories (
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50) NOT NULL
);

CREATE TABLE products (
    product_id INT PRIMARY KEY,
    product_name VARCHAR(100) NOT NULL,
    brand_id INT,
    category_id INT,
    FOREIGN KEY (brand_id) REFERENCES brands(brand_id),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
drop table products

INSERT INTO brands (brand_id, brand_name)
VALUES 
(1, 'Trek'),
(2, 'Giant'),
(3, 'Scott');

INSERT INTO categories (category_id, category_name)
VALUES 
(1, 'Mountain Bikes'),
(2, 'Road Bikes'),
(3, 'Electric Bikes');

INSERT INTO products (product_id, product_name, brand_id, category_id)
VALUES
(1, 'Electra Townie Original 21D - 2016', 1, 1),
(2, 'Electra Cruiser 1 (24-Inch) - 2016', 1, 1),
(3, 'Electra Girls Hawaii 1 (16-inch) - 2015/2016', 1, 1),
(4, 'Electra Moto 1 - 2016', 1, 1),
(5, 'Electra Townie Original 7D EQ - 2016', 1, 1),
(6, 'Electra Townie Original 7D EQ - Womens - 2016', 1, 1),
(7, 'Giant TCR Advanced', 2, 3),
(8, 'Trek 3700', 3, 2);


CREATE PROCEDURE usp_MaxNoOfProducts
AS
BEGIN
    DECLARE @TopBrandId INT;

    SELECT TOP 1 @TopBrandId = brand_id
    FROM products
    GROUP BY brand_id
    ORDER BY COUNT(product_id) DESC;

    SELECT 
        b.brand_name AS [Brand Name],
        c.category_name AS [Category Name],
        p.product_name AS [Product Name]
    FROM products p
    JOIN brands b ON p.brand_id = b.brand_id
    JOIN categories c ON p.category_id = c.category_id
    WHERE p.brand_id = @TopBrandId;
END;

EXEC usp_MaxNoOfProducts;

--4. Taxi-Services Stored Procedure
-- USER_TBL
CREATE TABLE USER_TBL (
    Usr_id INT PRIMARY KEY,
    F_name VARCHAR(50),
    L_name VARCHAR(50),
    Contact_no VARCHAR(15),
    Gender VARCHAR(10),
    Address VARCHAR(100)
);

-- DRIVER
CREATE TABLE DRIVER (
    Driver_id INT PRIMARY KEY,
    F_name VARCHAR(50),
    L_name VARCHAR(50),
    Contact_no VARCHAR(15),
    Rating FLOAT
);

-- TAXI
CREATE TABLE TAXI (
    Registration_no VARCHAR(20) PRIMARY KEY,
    Taxi_Model VARCHAR(50),
    Taxi_Year DATE,
    Taxi_type VARCHAR(20),
    Status VARCHAR(20)
);

-- TRIP_DETAILS
CREATE TABLE TRIP_DETAILS (
    Trip_id INT PRIMARY KEY,
    Trip_date DATE,
    Trip_amt DECIMAL(10,2),
    Driver_id INT,
    Usr_id INT,
    Registration_no VARCHAR(20),
    Strt_time TIME,
    End_time TIME,
    FOREIGN KEY (Driver_id) REFERENCES DRIVER(Driver_id),
    FOREIGN KEY (Usr_id) REFERENCES USER_TBL(Usr_id),
    FOREIGN KEY (Registration_no) REFERENCES TAXI(Registration_no)
);

INSERT INTO USER_TBL VALUES
(1, 'Tearsaa', 'Sivakumar', '7507985214', 'Female', '123 BROAD CROSS ST,CHENNAI-48'),
(2, 'ARUN', 'PRAKASH', '8094564234', 'Male', '768 2ND STREET,BENGALURU-20'),
(3, 'LATHA', 'SANKAR', '9578643210', 'Female', '458 3RD STREET ,BENGALURU-20'),
(4, 'AMIT', 'VIKARAM', '9497996990', 'Male', '43 5TH STREET,KOCHI-84'),
(5, 'RAMESH', 'BABU', '9432189760', 'Male', '109 2ND CROSS ST,KOCHI-12'),
(6, 'GANESH', 'KANNAN', '9375237890', 'Male', '45 3rd bengaluru street,HYDERABAD-21'),
(7, 'GAYATHRI', 'RAGHU', '8073245678', 'Female', '23 2ND CROSS ST,BENGALURU-12');

INSERT INTO DRIVER VALUES
(1, 'Victor', 'Morris', '7284238142', 4.8),
(2, 'Lois', 'Bradley', '7663222227', 4.5),
(3, 'Maria', 'King', '7947404222', 3.2),
(4, 'James', 'Foster', '7011656458', 4.1),
(5, 'Frank', 'Reed', '7601316239', 4.0),
(6, 'Linda', 'Chapman', '7201101385', 4.5),
(7, 'Lori', 'Webb', '7939652951', 4.4);

INSERT INTO TAXI VALUES
('KA-05I-2014', 'Maruti Suzuki Swift', '2016-01-08', 'Hatchback', 'Busy'),
('KA-10D-1988', 'Renault Kwid', '2014-09-29', 'Hatchback', 'Available'),
('KA-15R-3367', 'BENZE 300', '2017-03-21', 'SUV', 'Available'),
('KL-210-7623', 'Maruti Swift', '2016-10-11', 'Hatchback', 'Available'),
('KL-74A-4587', 'Nissan Altima', '2016-10-11', 'Sedan', 'Busy'),
('TN-67R-5845', 'Honda odyssey', '2016-11-23', 'Minivan', 'Available');

INSERT INTO TRIP_DETAILS VALUES
(1, '2018-07-14', 105, 1, 1, 'KA-15R-3367', '14:00:00', '14:45:00'),
(2, '2018-07-21', 110, 2, 3, 'TN-67R-5845', '12:15:00', '13:30:00'),
(3, '2018-07-21', 150, 3, 4, 'KL-74A-4587', '15:30:00', '16:30:00'),
(4, '2018-06-26', 111, 1, 1, 'KA-15R-3367', '09:00:00', '09:50:00'),
(5, '2018-06-25', 200, 2, 2, 'KL-210-7623', '08:00:00', '09:00:00'),
(6, '2018-06-20', 250, 3, 3, 'KA-10D-1988', '11:10:00', '09:45:00');

CREATE PROCEDURE usp_GetTaxiDetails
    @state NVARCHAR(50)
AS
BEGIN
    DECLARE @prefix NVARCHAR(5)
    SET @prefix = 
        CASE 
            WHEN @state = 'Kerala' THEN 'KL'
            WHEN @state = 'Karnataka' THEN 'KA'
            WHEN @state = 'Tamil Nadu' THEN 'TN'
            ELSE ''
        END

    SELECT 
        T.Registration_no,
        T.Taxi_Model AS Model,
        T.Taxi_type AS Type,
        SUM(TD.Trip_amt) AS [Total Amount]
    FROM 
        TAXI T
    JOIN 
        TRIP_DETAILS TD ON T.Registration_no = TD.Registration_no
    WHERE 
        T.Registration_no LIKE @prefix + '%'
    GROUP BY 
        T.Registration_no, T.Taxi_Model, T.Taxi_type
END;

EXEC usp_GetTaxiDetails @state = 'Kerala';
