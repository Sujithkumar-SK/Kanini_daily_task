ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = OFF;
DELETE FROM users;

DBCC CHECKIDENT ('Users', RESEED, 0);
select * from users;
SELECT * FROM Courses; 
SELECT * FROM Kids;
select * from Enrollments;

INSERT INTO Courses (Title, Description, Duration, Instructor)
VALUES ('Numbers', 'Learning Numbers', '2 Weeks', 'Teacher A');

INSERT INTO Kids (UserId, Name, Age, Grade, ProfileImage)
VALUES (2, 'Tom', 6, '1st', NULL);


INSERT INTO Kids (UserId, Name, Age, Grade, ProfileImage)
VALUES
(1, 'Santhosh', 7, '2nd', NULL), -- maps Kid user
(2, 'Mani', 6, '1st', NULL),     -- Parent's kid
(2, 'Priya', 8, '3rd', NULL);    -- Parent's kid

INSERT INTO Courses (Title, Description, Duration, Instructor)
VALUES
('Alphabets', 'Learn A to Z with fun activities', '2 Weeks', 'Teacher A'),
('Numbers', 'Learn numbers with games and puzzles', '3 Weeks', 'Teacher B'),
('Rhymes', 'Fun nursery rhymes for kids', '1 Week', 'Teacher C');

INSERT INTO Enrollments (KidId, CourseId, EnrolledDate)
VALUES
(1, 1, GETDATE()),  -- Santhosh → Alphabets
(1, 2, GETDATE()),  -- Santhosh → Numbers
(2, 3, GETDATE()),  -- Mani → Rhymes
(3, 1, GETDATE());  -- Priya → Alphabets

INSERT INTO Kids (Name, Age, Grade, UserId)
VALUES 
('Arjun', 10, '5th', 1),
('Priya', 11, '6th', 1),
('Kavin', 9, '5th', 2);

INSERT INTO Courses (Title, Instructor)
VALUES
('Math Basics', 'Mr. Smith'),
('Science Experiments', 'Mrs. Johnson'),
('History Fun', 'Mr. Smith');

SELECT * FROM Kids;
SELECT * FROM Courses;
INSERT INTO Enrollments (KidId, CourseId, EnrolledDate)
VALUES
(1, 1, '2025-01-05'),
(2, 1, '2025-01-10'),
(1, 2, '2025-02-01'),
(3, 3, '2025-02-15');


SELECT * FROM Kids;
SELECT * FROM Courses;
SELECT * FROM Enrollments;
