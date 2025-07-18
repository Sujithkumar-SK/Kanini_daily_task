CREATE TABLE Alphabet (
    Letter CHAR(1) NOT NULL CHECK (Letter LIKE '[A-Z]'),
    Word NVARCHAR(100) NOT NULL,
    ImageUrl NVARCHAR(255)
);

BULK INSERT Alphabet
FROM 'C:\Users\SUJITH KUMAR\OneDrive\Documents\Kanini\day17\alphabets.csv'
WITH (
    FIRSTROW = 2,
    FIELDTERMINATOR = ',',
    ROWTERMINATOR = '0x0a', --/n
    TABLOCK
);

DROP TABLE IF EXISTS Alphabet;
SELECT * FROM Alphabet;

ALTER TABLE Alphabet
ADD Id INT;

WITH RowNumbers AS (
    SELECT *,
           ROW_NUMBER() OVER (ORDER BY Letter) AS rn
    FROM Alphabet
)
UPDATE RowNumbers
SET Id = rn;

