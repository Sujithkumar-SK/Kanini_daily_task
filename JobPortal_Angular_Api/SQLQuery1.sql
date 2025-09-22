select * from Users;
select * from UserDetails;
select * from Resumes;
select * from Jobs;
select * from Skills;
select * from JobSkills;
select * from Applications;
select * from CompanyProfiles;

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF;

delete from Users where User = 'test@gmail.com';

DBCC CHECKIDENT ('Users', RESEED, 3);