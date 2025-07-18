create database learning
USE learning

CREATE TABLE Courses (
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255)
);


CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    UserType NVARCHAR(50) NOT NULL
);


CREATE TABLE Dictionary (
    WordId INT IDENTITY(1,1) PRIMARY KEY,
    Word NVARCHAR(100) NOT NULL UNIQUE,
    Definition NVARCHAR(255),
    SearchCount INT DEFAULT 1
);

CREATE TABLE UserLog (
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT,
    WordId INT NULL,
    CourseId INT NULL,
    LogType NVARCHAR(50),
    LogTime DATETIME DEFAULT GETDATE(),

    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (WordId) REFERENCES Dictionary(WordId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);

-- Trigger: Log insert into Courses table
CREATE OR ALTER TRIGGER trg_LogInsert_Courses
ON Courses
AFTER INSERT
AS
BEGIN
    INSERT INTO UserLog (CourseId, LogType, LogTime)
    SELECT CourseId, 'CourseInsert', GETDATE()
    FROM inserted;
END;

-- Trigger: Log insert into Users table
CREATE OR ALTER TRIGGER trg_LogInsert_Users
ON Users
AFTER INSERT
AS
BEGIN
    INSERT INTO UserLog (UserId, LogType, LogTime)
    SELECT UserId, 'UserInsert', GETDATE()
    FROM inserted;
END;

-- Trigger: Log insert into Dictionary table
CREATE OR ALTER TRIGGER trg_LogInsert_Dictionary
ON Dictionary
AFTER INSERT
AS
BEGIN
    INSERT INTO UserLog (WordId, LogType, LogTime)
    SELECT WordId, 'WordInsert', GETDATE()
    FROM inserted;
END;

-- Stored Procedure: Insert or Update word and log the action
CREATE OR ALTER PROCEDURE usp_UpdateOrInsertWord
    @UserId INT,
    @Word NVARCHAR(100),
    @Definition NVARCHAR(255) = NULL,
    @CourseId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @WordId INT;

    IF EXISTS (SELECT 1 FROM Dictionary WHERE Word = @Word)
    BEGIN
        UPDATE Dictionary
        SET SearchCount = ISNULL(SearchCount, 0) + 1
        WHERE Word = @Word;

        SELECT @WordId = WordId FROM Dictionary WHERE Word = @Word;
    END
    ELSE
    BEGIN
        INSERT INTO Dictionary (Word, Definition, SearchCount)
        VALUES (@Word, @Definition, 1);
        SET @WordId = SCOPE_IDENTITY();
    END

    INSERT INTO UserLog (UserId, WordId, CourseId, LogType, LogTime)
    VALUES (@UserId, @WordId, @CourseId, 'SearchOrInsert', GETDATE());

    SELECT * FROM Dictionary WHERE WordId = @WordId;
END;

-- User-Defined Function: Get search count of a specific word
CREATE OR ALTER FUNCTION udf_GetSearchCount (@Word NVARCHAR(100))
RETURNS INT
AS
BEGIN
    DECLARE @Count INT;
    SELECT @Count = SearchCount FROM Dictionary WHERE Word = @Word;
    RETURN ISNULL(@Count, 0);
END;

--Show user, course, and dictionary log details
SELECT 
    u.UserId,
    u.UserType,
    d.Word,
    d.Definition,
    c.CourseName,
    ul.LogType,
    ul.LogTime
FROM UserLog ul
JOIN Users u ON ul.UserId = u.UserId
LEFT JOIN Dictionary d ON ul.WordId = d.WordId
LEFT JOIN Courses c ON ul.CourseId = c.CourseId
ORDER BY ul.LogTime DESC;

--Most searched word(s)
SELECT Word, SearchCount
FROM Dictionary
WHERE SearchCount = (
    SELECT MAX(SearchCount) FROM Dictionary
);

--Top 5 searched words
SELECT TOP 5 Word, SearchCount
FROM Dictionary
ORDER BY SearchCount DESC;

--Most active users
SELECT u.UserId, u.UserType, COUNT(*) AS TotalActions
FROM UserLog ul
JOIN Users u ON ul.UserId = u.UserId
GROUP BY u.UserId, u.UserType
ORDER BY TotalActions DESC;

--Word searches per course
SELECT c.CourseName, COUNT(ul.WordId) AS WordSearches
FROM UserLog ul
JOIN Courses c ON ul.CourseId = c.CourseId
GROUP BY c.CourseName;

INSERT INTO Courses (CourseName, Description) VALUES 
('Alphabet Course', 'A to Z learning'),
('Numbers Course', 'Counting 1 to 10');

INSERT INTO Users (UserType) VALUES 
('Student'), ('Teacher');

EXEC usp_UpdateOrInsertWord @UserId = 1, @Word = 'Apple', @Definition = 'A fruit', @CourseId = 1;
EXEC usp_UpdateOrInsertWord @UserId = 1, @Word = 'Banana', @Definition = 'Another fruit', @CourseId = 1;
EXEC usp_UpdateOrInsertWord @UserId = 2, @Word = 'Five', @Definition = 'Number after four', @CourseId = 2;


INSERT INTO Courses (CourseName, Description) VALUES 
('Alphabet Course', 'Learn letters A to Z'),
('Numbers Course', 'Learn numbers 1 to 10'),
('Color Course', 'Learn basic colors');

INSERT INTO Users (UserType) VALUES 
('Student'),
('Teacher'),
('Parent');


INSERT INTO Dictionary (Word, Definition, SearchCount) VALUES 
('Apple', 'A red fruit', 3),
('Ball', 'A round object used in games', 2),
('Cat', 'A small domesticated animal', 5),
('Dog', 'A loyal animal', 4),
('Elephant', 'A large wild animal', 1);

SELECT * FROM Courses;
SELECT * FROM Users;
SELECT * FROM Dictionary;

EXEC usp_UpdateOrInsertWord @UserId = 1, @Word = 'Apple', @Definition = 'A fruit', @CourseId = 1;
EXEC usp_UpdateOrInsertWord @UserId = 1, @Word = 'Ball', @Definition = 'Used in sports', @CourseId = 1;
EXEC usp_UpdateOrInsertWord @UserId = 2, @Word = 'Cat', @Definition = 'Pet animal', @CourseId = 2;
EXEC usp_UpdateOrInsertWord @UserId = 3, @Word = 'Dog', @Definition = 'Friendly animal', @CourseId = 2;
EXEC usp_UpdateOrInsertWord @UserId = 2, @Word = 'Elephant', @Definition = 'Big animal', @CourseId = 3;


-- Check logs
SELECT * FROM UserLog;

-- Check dictionary
SELECT * FROM Dictionary;

-- Check search count using UDF
SELECT dbo.udf_GetSearchCount('Apple') AS AppleSearchCount;


SELECT * FROM Users;
SELECT * FROM Courses;







-- Step 1: Disable foreign key constraints temporarily
ALTER TABLE UserLog NOCHECK CONSTRAINT ALL;

-- Step 2: Delete data from the dependent table first
DELETE FROM UserLog;

-- Step 3: Now delete from the other tables
DELETE FROM Dictionary;
DELETE FROM Users;
DELETE FROM Courses;

-- Step 4: Re-enable foreign key constraints
ALTER TABLE UserLog WITH CHECK CHECK CONSTRAINT ALL;

-- Reset IDENTITY to 1
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Courses', RESEED, 0);
DBCC CHECKIDENT ('Dictionary', RESEED, 0);

