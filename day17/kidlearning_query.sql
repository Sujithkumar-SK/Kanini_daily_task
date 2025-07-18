create database KidsLearningPlatform;
use KidsLearningPlatform;

CREATE TABLE Alphabet (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Letter CHAR(1) NOT NULL CHECK (Letter LIKE '[A-Z]'),
    Word NVARCHAR(50) NOT NULL,
    ImageUrl NVARCHAR(255)
);
/*BULK INSERT Alphabet
FROM 'C:\Users\SUJITH KUMAR\OneDrive\Documents\Kanini\day17\alphabets.csv'
WITH (
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '\n',
    TABLOCK,
    CODEPAGE = '65001'
);*/

INSERT INTO Alphabet (Letter, Word, ImageUrl) VALUES 
('A', 'Apple', 'https://images.pexels.com/photos/102104/pexels-photo-102104.jpeg?auto=compress&cs=tinysrgb&h=350'),
('B', 'Ball', 'https://images.pexels.com/photos/47730/the-ball-stadion-football-the-pitch-47730.jpeg?auto=compress&cs=tinysrgb&h=350'),
('C', 'Cat', 'https://images.pexels.com/photos/45201/kitty-cat-kitten-pet-45201.jpeg?auto=compress&cs=tinysrgb&h=350'),
('D', 'Dog', 'https://images.pexels.com/photos/1108099/pexels-photo-1108099.jpeg?auto=compress&cs=tinysrgb&h=350'),
('E', 'Elephant', 'https://images.pexels.com/photos/1054655/pexels-photo-1054655.jpeg?auto=compress&cs=tinysrgb&h=350'),
('F', 'Fish', 'https://images.pexels.com/photos/128756/pexels-photo-128756.jpeg?auto=compress&cs=tinysrgb&h=350'),
('G', 'Goat', 'https://images.pexels.com/photos/86594/goat-animal-horns-black-and-white-86594.jpeg?auto=compress&cs=tinysrgb&h=350'),
('H', 'Hat', 'https://images.pexels.com/photos/984619/pexels-photo-984619.jpeg?auto=compress&cs=tinysrgb&h=350'),
('I', 'Ice cream', 'https://images.pexels.com/photos/1362534/pexels-photo-1362534.jpeg?auto=compress&cs=tinysrgb&h=350'),
('J', 'Jug', 'https://images.pexels.com/photos/32566372/pexels-photo-32566372.jpeg?auto=compress&cs=tinysrgb&h=350'),
('K', 'Kite', 'https://images.pexels.com/photos/929362/pexels-photo-929362.jpeg?auto=compress&cs=tinysrgb&h=350'),
('L', 'Lion', 'https://images.pexels.com/photos/2220336/pexels-photo-2220336.jpeg?auto=compress&cs=tinysrgb&h=350'),
('M', 'Monkey', 'https://images.pexels.com/photos/1207875/pexels-photo-1207875.jpeg?auto=compress&cs=tinysrgb&h=350'),
('N', 'Nest', 'https://images.pexels.com/photos/972994/pexels-photo-972994.jpeg?auto=compress&cs=tinysrgb&h=350'),
('O', 'Orange', 'https://images.pexels.com/photos/1293120/pexels-photo-1293120.jpeg?auto=compress&cs=tinysrgb&h=350'),
('P', 'Parrot', 'https://images.pexels.com/photos/1463295/pexels-photo-1463295.jpeg?auto=compress&cs=tinysrgb&h=350'),
('Q', 'Queen', 'https://images.pexels.com/photos/260024/pexels-photo-260024.jpeg?auto=compress&cs=tinysrgb&h=350'),
('R', 'Rabbit', 'https://images.pexels.com/photos/6845638/pexels-photo-6845638.jpeg?auto=compress&cs=tinysrgb&h=350'),
('S', 'Sun', 'https://images.pexels.com/photos/301599/pexels-photo-301599.jpeg?auto=compress&cs=tinysrgb&h=350'),
('T', 'Tiger', 'https://images.pexels.com/photos/792381/pexels-photo-792381.jpeg?auto=compress&cs=tinysrgb&h=350'),
('U', 'Umbrella', 'https://images.pexels.com/photos/1715161/pexels-photo-1715161.jpeg?auto=compress&cs=tinysrgb&h=350'),
('V', 'Van', 'https://images.pexels.com/photos/2303781/pexels-photo-2303781.jpeg?auto=compress&cs=tinysrgb&h=350'),
('W', 'Watch', 'https://images.pexels.com/photos/5858713/pexels-photo-5858713.jpeg?auto=compress&cs=tinysrgb&h=350'),
('X', 'Xylophone', 'https://images.pexels.com/photos/165972/pexels-photo-165972.jpeg?auto=compress&cs=tinysrgb&h=350'),
('Y', 'Yak', 'https://images.pexels.com/photos/51952/cow-bull-horns-coat-51952.jpeg?auto=compress&cs=tinysrgb&h=350'),
('Z', 'Zebra', 'https://images.pexels.com/photos/750539/pexels-photo-750539.jpeg?auto=compress&cs=tinysrgb&h=350');

select * from Alphabet

CREATE TABLE Numbers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Number INT NOT NULL CHECK (Number BETWEEN 1 AND 10),
    Name NVARCHAR(20) NOT NULL,
    ImageUrl NVARCHAR(255)
);

INSERT INTO Numbers (Number, Name, ImageUrl) VALUES
(1, 'One', 'https://images.pexels.com/photos/1111372/pexels-photo-1111372.jpeg?auto=compress&cs=tinysrgb&h=350'),
(2, 'Two', 'https://images.pexels.com/photos/1852383/pexels-photo-1852383.jpeg?auto=compress&cs=tinysrgb&h=350'),
(3, 'Three', 'https://images.pexels.com/photos/355948/pexels-photo-355948.jpeg?auto=compress&cs=tinysrgb&h=350'),
(4, 'Four', 'https://images.pexels.com/photos/1111371/pexels-photo-1111371.jpeg?auto=compress&cs=tinysrgb&h=350'),
(5, 'Five', 'https://images.pexels.com/photos/1054713/pexels-photo-1054713.jpeg?auto=compress&cs=tinysrgb&h=350'),
(6, 'Six', 'https://images.pexels.com/photos/1329291/pexels-photo-1329291.jpeg?auto=compress&cs=tinysrgb&h=350'),
(7, 'Seven', 'https://images.pexels.com/photos/1329290/pexels-photo-1329290.jpeg?auto=compress&cs=tinysrgb&h=350'),
(8, 'Eight', 'https://images.pexels.com/photos/1652405/pexels-photo-1652405.jpeg?auto=compress&cs=tinysrgb&h=350'),
(9, 'Nine', 'https://images.pexels.com/photos/376611/pexels-photo-376611.jpeg?auto=compress&cs=tinysrgb&h=350'),
(10, 'Ten', 'https://images.pexels.com/photos/1346154/pexels-photo-1346154.jpeg?auto=compress&cs=tinysrgb&h=350');

select * from Numbers;

CREATE TABLE Dictionary (
    WordId INT IDENTITY(1,1) PRIMARY KEY,
    Word NVARCHAR(50) NOT NULL,
    Definition NVARCHAR(255) NOT NULL,
    SearchCount INT DEFAULT 0
);

INSERT INTO Dictionary (Word, Definition) VALUES
('Apple', 'A red or green fruit that is sweet and crunchy'),
('Ball', 'A round object used to play games'),
('Cat', 'A small pet animal that says meow'),
('Dog', 'A friendly animal that barks'),
('Elephant', 'A very large animal with a trunk');

select * from Dictionary;

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    User_Name NVARCHAR(50) NOT NULL,
    UserType NVARCHAR(10) NOT NULL,
    CONSTRAINT chk_UserType CHECK (UserType IN ('guest', 'admin', 'child'))
);

INSERT INTO Users (User_Name, UserType) VALUES
('Sujith', 'admin'),
('Anu', 'child'),
('GuestUser1', 'guest');

CREATE TABLE UserLog (
    LoginId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    WordId INT NOT NULL,
    LogTime DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_User FOREIGN KEY (UserId) REFERENCES Users(UserId),
    CONSTRAINT fk_Word FOREIGN KEY (WordId) REFERENCES Dictionary(WordId)
);

CREATE or alter PROCEDURE usp_UpdateOrInsertWord
    @UserId INT,
    @Word NVARCHAR(100),
    @Definition NVARCHAR(255) = NULL  
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
    INSERT INTO UserLog (UserId, WordId, LogTime)
    VALUES (@UserId, @WordId, GETDATE());
    select * from dictionary
    select * from userlog
END;

EXEC usp_UpdateOrInsertWord @userid = 1, @Word = 'simple';

ALTER TABLE Dictionary
ALTER COLUMN Definition NVARCHAR(255);

select * from dictionary


-- Show what words each user searched and when

SELECT 
    U.User_Name,
    D.Word,
    D.Definition,
    UL.LogTime
FROM UserLog UL
JOIN Users U ON UL.UserId = U.UserId
JOIN Dictionary D ON UL.WordId = D.WordId
ORDER BY UL.LogTime DESC;

--Show only 'child' users and their search history
SELECT 
    U.User_Name,
    D.Word,
    UL.LogTime
FROM Users U
JOIN UserLog UL ON U.UserId = UL.UserId
JOIN Dictionary D ON D.WordId = UL.WordId
WHERE U.UserType = 'child';

--Get the most searched word
SELECT Word, Definition, SearchCount
FROM Dictionary
WHERE SearchCount = (SELECT MAX(SearchCount) FROM Dictionary);

--Users who searched for a specific word
SELECT User_Name
FROM Users
WHERE UserId IN (
    SELECT UserId
    FROM UserLog
    WHERE WordId = (
        SELECT WordId FROM Dictionary WHERE Word = 'happy'
    )
);

INSERT INTO Dictionary (Word, Definition, SearchCount) VALUES
('Happy', 'Feeling or showing pleasure or contentment', 1),
('Balloon', 'A small bag filled with air or gas that floats',1),
('Queen', 'The female ruler of an independent state',1),
('Sun', 'The star around which the earth orbits',1);





EXEC usp_UpdateOrInsertWord @userid = 2, @Word = 'Happy';
EXEC usp_UpdateOrInsertWord @userid = 2, @Word = 'Sun';
EXEC usp_UpdateOrInsertWord @userid = 2, @Word = 'Cat';

EXEC usp_UpdateOrInsertWord @userid = 3, @Word = 'Queen';
EXEC usp_UpdateOrInsertWord @userid = 3, @Word = 'Happy';


EXEC usp_UpdateOrInsertWord @userid = 1, @Word = 'Apple';
EXEC usp_UpdateOrInsertWord @userid = 1, @Word = 'Happy';



CREATE TABLE LearningContent (
    ContentId INT IDENTITY(1,1) PRIMARY KEY,
    ContentType NVARCHAR(20) NOT NULL CHECK (ContentType IN ('Alphabet', 'Number')),
    Title NVARCHAR(50) NOT NULL
);

ALTER TABLE Alphabet
ADD ContentId INT FOREIGN KEY REFERENCES LearningContent(ContentId);

ALTER TABLE Numbers
ADD ContentId INT FOREIGN KEY REFERENCES LearningContent(ContentId);

INSERT INTO LearningContent (ContentType, Title) VALUES 
('Alphabet', 'A-Z Alphabets'),
('Number', '1 to 10 Numbers');

drop database KidsLearningPlatform