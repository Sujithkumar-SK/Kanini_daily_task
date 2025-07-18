create database work;
use work;

CREATE TABLE COUNTRY (
    Code VARCHAR(3) PRIMARY KEY,
    Name VARCHAR(50),
    Continent VARCHAR(50)
);

CREATE TABLE CITY (
    ID INT PRIMARY KEY,
    Name VARCHAR(17),
    CountryCode VARCHAR(3),
    District VARCHAR(20),
    Population INT
);

INSERT INTO COUNTRY (Code, Name, Continent) VALUES
('IND', 'India', 'Asia'),
('USA', 'United States', 'North America'),
('CHN', 'China', 'Asia');

INSERT INTO CITY (ID, Name, CountryCode, District, Population) VALUES
(1, 'Mumbai', 'IND', 'Maharashtra', 12442373),
(2, 'Delhi', 'IND', 'Delhi', 11034555),
(3, 'San Francisco', 'USA', 'California', 883305),
(4, 'Los Angeles', 'USA', 'California', 3990456),
(5, 'Beijing', 'CHN', 'Beijing', 21540000);

SELECT SUM(CITY.Population)
FROM CITY
JOIN COUNTRY ON CITY.CountryCode = COUNTRY.Code
WHERE COUNTRY.Continent = 'Asia';


-- Hackers Table
CREATE TABLE Hackers (
    hacker_id INT PRIMARY KEY,
    name VARCHAR(50)
);

-- Submissions Table
CREATE TABLE Submissions (
    submission_id INT PRIMARY KEY,
    hacker_id INT,
    challenge_id INT,
    score INT
);

-- Insert Sample Data
INSERT INTO Hackers (hacker_id, name) VALUES
(4071, 'Rose'),
(74842, 'Lisa'),
(84072, 'Bonnie'),
(4806, 'Angela'),
(26071, 'Frank'),
(80305, 'Kimberly'),
(49438, 'Patrick');

INSERT INTO Submissions (submission_id, hacker_id, challenge_id, score) VALUES
(1, 4071, 19797, 90),
(2, 4071, 49593, 101),
(3, 74842, 19797, 74),
(4, 74842, 63132, 100),
(5, 84072, 49593, 60),
(6, 84072, 63132, 40),
(7, 4806, 19797, 89),
(8, 26071, 19797, 85),
(9, 80305, 19797, 67),
(10, 49438, 19797, 43);


SELECT h.hacker_id, h.name, SUM(MaxScores.max_score) AS total_score
FROM Hackers h
JOIN (
    SELECT hacker_id, challenge_id, MAX(score) AS max_score
    FROM Submissions
    GROUP BY hacker_id, challenge_id
) AS MaxScores ON h.hacker_id = MaxScores.hacker_id
GROUP BY h.hacker_id, h.name
HAVING SUM(MaxScores.max_score) > 0
ORDER BY total_score DESC, h.hacker_id ASC;

-- Challenges Table
CREATE TABLE Challenges (
    challenge_id INT PRIMARY KEY,
    hacker_id INT
);

-- Add more hackers (if not already present)
INSERT INTO Hackers (hacker_id, name) VALUES
(21283, 'Angela'),
(88255, 'Patrick'),
(96196, 'Lisa');

-- Insert Challenges
INSERT INTO Challenges (challenge_id, hacker_id) VALUES
(1, 21283), (2, 21283), (3, 21283),
(4, 21283), (5, 21283), (6, 21283),
(7, 88255), (8, 88255), (9, 88255),
(10, 88255), (11, 88255),
(12, 96196);

WITH ChallengeCount AS (
    SELECT c.hacker_id, h.name, COUNT(*) AS total_challenges
    FROM Challenges c
    JOIN Hackers h ON c.hacker_id = h.hacker_id
    GROUP BY c.hacker_id, h.name
),
MaxCount AS (
    SELECT MAX(total_challenges) AS max_challenges FROM ChallengeCount
)
SELECT cc.hacker_id, cc.name, cc.total_challenges
FROM ChallengeCount cc
JOIN MaxCount mc ON cc.total_challenges = mc.max_challenges
ORDER BY cc.total_challenges DESC, cc.hacker_id ASC;


--5
-- Library Table
CREATE TABLE Library (
    book_id INT PRIMARY KEY,
    title VARCHAR(100),
    author VARCHAR(100),
    published_year INT,
    rating FLOAT
);

-- Sample Data
INSERT INTO Library (book_id, title, author, published_year, rating) VALUES
(1, 'The Great Gatsby', 'F. Scott Fitzgerald', 1925, 4.2),
(2, 'To Kill a Mockingbird', 'Harper Lee', 1960, NULL),
(3, '1984', 'George Orwell', 1949, 4.8),
(4, 'The Catcher in the Rye', 'J.D. Salinger', 1951, NULL),
(5, 'Brave New World', 'Aldous Huxley', 1932, 4.3);

SELECT book_id, title, author, published_year
FROM Library
WHERE rating IS NULL;
