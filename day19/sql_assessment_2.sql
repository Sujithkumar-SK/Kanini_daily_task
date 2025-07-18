create database Department_Worker;

use Department_Worker;

create table Worker (
	Worker_ID int identity(1,1) primary key,
	First_Name varchar(50),
	Last_Name varchar(50),
	Salary int,
	Joining_Date date,
	Department varchar(20)
	);
drop table Bonus
create table Title (
	Worker_Ref_ID int,
	Worker_Title varchar(50),
	Affected_From date,
	foreign key (Worker_Ref_ID) references Worker(Worker_ID)
	);
create table Bonus (
	Worker_Ref_ID int,
	Bonus_Amount int,
	Bonus_Date date
	foreign key (Worker_Ref_ID) references Worker(Worker_ID)
	);

--1.Write a stored procedure to display the firstname, salary and title for all workers.

create or alter procedure Display_Details 
as begin
	select w.First_name, w.Salary, t.Worker_Title
	from Worker w join Title t on w.Worker_ID = t.Worker_Ref_ID
end;

Display_Details;

insert into Worker(First_Name, Last_Name, Salary, Joining_Date,Department)
values
('John', 'Doe', 50000, '2018-01-15', 'HR'),
('Jane', 'Smith', 60000, '2017-03-20', 'IT'),
('Alan', 'Brown', 55000, '2020-05-10', 'Finance'),
('Sara', 'Connor', 45000, '2019-11-01', 'HR'),
('Mike', 'Jordan', 70000, '2016-06-22', 'Sales');


insert into Title values (1, 'HR Manager', '2018-01-15');
insert into Title values (1, 'HR Director', '2021-01-01');
insert into Title values (2, 'Senior Developer', '2017-03-20');
insert into Title values (3, 'Accountant', '2020-05-10');
insert into Title values (4, 'HR Assistant', '2019-11-01');
insert into Title values (5, 'Sales Manager', '2016-06-22');

insert into Bonus values (1, 5000, '2021-12-25');
insert into Bonus values (1, 3000, '2022-12-25');
insert into Bonus values (2, 4000, '2023-01-15');
insert into Bonus values (5, 7000, '2022-07-10');

--2. Write a plsql function to display the count of worker(s) based on the nth highestsalary.

insert into worker values('sujith','kumar',70000,'2025-05-19','dev');

create function get_worker_count_by_nth_highest_salary (@num int)
returns int as begin
	declare @nthsal int;
	select @nthsal = Salary
	from (
		select distinct Salary, DENSE_RANK() over(order by Salary desc) as rn
		from Worker) as RankedSalaries where rn = @num;
	return (select count(*) from worker where Salary = @nthsal);
end;

select dbo.get_worker_count_by_nth_highest_salary(1) as NoOFCount

--3. Create a stored procedure GetWorkersWithoutBonus to list all workers who have never received any bonus.
select * from bonus
create or alter procedure GetWorkersWithoutBonus
as begin
	select w.Worker_ID, w.First_Name,w.Last_Name,t.Worker_Title,w.Department
	from Worker w left join Bonus b on w.Worker_ID = b.Worker_Ref_ID
	join Title t on w.Worker_ID = t.Worker_Ref_ID 
	where b.Worker_Ref_ID is null;
end;
GetWorkersWithoutBonus

--5. Create a procedure GetMultiTitleWorkers that returns all workers who have held more than one title.
select * from worker
select * from Bonus
select * from Title
create or alter procedure GetMultiTitleWorkers
as begin
	select w.Worker_ID, w.First_Name, w.Last_Name, count(t.Worker_Title) as Title_Count from worker w
	join Title t on w.Worker_ID = t.Worker_Ref_ID
	group by w.Worker_ID, w.First_Name, w.Last_Name
	having count(t.Worker_Title) >1;
end;
GetMultiTitleWorkers

/*6. Create a user-defined scalar function fn_GetTotalBonus(@WorkerID) that calculates the total bonus 
amount given to a worker from the Bonus table. and Create a stored procedure GetWorkerDetailsWithBonus */
create function fn_GetTotalBonus(@WorkerID int)
returns int as begin
	declare @tot_bonus int;
	select @tot_bonus = isnull(sum(Bonus_Amount),0) from Bonus
	where Worker_Ref_ID = @WorkerID;

	return @tot_bonus;
end;

create or alter procedure GetWorkerDetailsWithBonus
as begin
	select w.First_Name +' '+ w.Last_Name as FullName,
	w.Salary as CurrentSalary,w.Department,t.Worker_Title as JobTitle,
	dbo.fn_GetTotalBonus(w.Worker_ID) as TotalBonus
	from worker w join Title t on w.Worker_ID = t.Worker_Ref_ID;
end;
GetWorkerDetailsWithBonus
select * from bonus
select * from worker
select * from title