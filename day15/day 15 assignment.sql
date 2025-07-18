create database weatherForecast;
use weatherForecast;

create table Cities (
CityId int not null primary key,
CityName varchar(20),
Country varchar(20)
);

create table Weather (
CityId int foreign key references Cities(CityId),
Date date,
Temperature decimal(5,2),
Precipitation decimal(5,2)
);

INSERT INTO Cities (CityId, CityName, Country) VALUES
(1, 'Chennai', 'India'),
(2, 'Mumbai', 'India'),
(3, 'New York', 'USA'),
(4, 'Los Angeles', 'USA'),
(5, 'London', 'UK');

-- Chennai
INSERT INTO Weather VALUES
(1, '2025-06-01', 35.2, 0),
(1, '2025-06-02', 36.5, 5.2),
(1, '2025-06-03', 37.8, 2.1),
(1, '2025-06-05', 36.0, 0),
(1, '2025-06-06', 35.0, 0),
(1, '2025-06-07', 34.2, 0),
(1, '2025-06-08', 36.7, 0),
(1, '2025-06-11', 34.0, 0);

-- Mumbai
INSERT INTO Weather VALUES
(2, '2025-06-01', 33.0, 20.5),
(2, '2025-06-02', 32.5, 22.3),
(2, '2025-06-03', 34.0, 25.0),
(2, '2025-06-06', 31.0, 15.0),
(2, '2025-06-07', 30.5, 18.0),
(2, '2025-06-08', 30.0, 10.0),
(2, '2025-06-11', 30.0, 0);

-- New York
INSERT INTO Weather VALUES
(3, '2025-06-01', 20.0, 0),
(3, '2025-06-02', 22.0, 1.5),
(3, '2025-06-03', 23.5, 3.0),
(3, '2025-06-06', 25.0, 0),
(3, '2025-06-07', 24.0, 0),
(3, '2025-06-08', 26.0, 0),
(3, '2025-06-11', 24.0, 0);

-- Los Angeles
INSERT INTO Weather VALUES
(4, '2025-06-01', 28.0, 0),
(4, '2025-06-02', 30.0, 0),
(4, '2025-06-03', 31.5, 0.5),
(4, '2025-06-06', 33.0, 0),
(4, '2025-06-07', 32.0, 0),
(4, '2025-06-08', 33.5, 0),
(4, '2025-06-11', 33.0, 0);

-- London
INSERT INTO Weather VALUES
(5, '2025-06-01', 15.0, 1.2),
(5, '2025-06-02', 16.5, 2.5),
(5, '2025-06-03', 18.0, 5.5),
(5, '2025-06-06', 16.0, 0.5),
(5, '2025-06-07', 14.0, 0),
(5, '2025-06-08', 15.5, 0),
(5, '2025-06-11', 17.0, 0);


--1. Hot City Per Country (Latest Date)
select * from Cities;
select * from Weather;


with CountryMaxDate as (
	select c.Country, max(w.Date) as maxDate
	from Weather w join Cities c on w.CityId = c.CityId
	group by c.Country
),
WeatherOnMaxDate as (
	select c.CityName, c.Country, w.Date, w.Temperature
	from Weather w
	join Cities c on c.CityId = w.CityId
	join CountryMaxDate cmd on c.Country = cmd.Country
	and w.Date = cmd.maxDate
),
Ranked as (
	select *, rank() over (partition by Country order by 
	Temperature desc) as rnk
	from WeatherOnMaxDate
)
select CityName, Country, Temperature, Date from Ranked
where rnk =1;

--2. 7-Day Moving Average of Temperature
SELECT 
    CityId,
    Date,
    Temperature,
    ROUND(
        AVG(Temperature) OVER (
            PARTITION BY CityId 
            ORDER BY Date 
            ROWS BETWEEN 6 PRECEDING AND CURRENT ROW
        ), 2
    ) AS MovingAvg_7Days
FROM Weather;

--3. Top 3 Rainiest Cities This Month
SELECT TOP 3 
    C.CityName,
    SUM(W.Precipitation) AS TotalRain
FROM Weather W
JOIN Cities C ON W.CityId = C.CityId
WHERE MONTH(W.Date) = MONTH(GETDATE()) 
  AND YEAR(W.Date) = YEAR(GETDATE())
GROUP BY C.CityName
ORDER BY TotalRain DESC;


--4. Temperature Difference Over Time
WITH TempDates AS (
    SELECT 
        CityId,
        MIN(Date) AS FirstDate,
        MAX(Date) AS LastDate
    FROM Weather
    GROUP BY CityId
),
TempChange AS (
    SELECT 
        W1.CityId,
        W1.Temperature AS FirstTemp,
        W2.Temperature AS LastTemp
    FROM TempDates T
    JOIN Weather W1 ON T.CityId = W1.CityId AND T.FirstDate = W1.Date
    JOIN Weather W2 ON T.CityId = W2.CityId AND T.LastDate = W2.Date
)
SELECT 
    C.CityName,
    T.LastTemp - T.FirstTemp AS TemperatureChange
FROM TempChange T
JOIN Cities C ON T.CityId = C.CityId;

--5. Days with Above-Average Temperature
SELECT 
    C.CityName,
    W.Date,
    W.Temperature,
    A.AvgTemp
FROM Weather W
JOIN Cities C ON W.CityId = C.CityId
JOIN (
    SELECT 
        CityId, 
        AVG(Temperature) AS AvgTemp
    FROM Weather
    GROUP BY CityId
) A ON W.CityId = A.CityId
WHERE W.Temperature > A.AvgTemp;

--6. Second Coldest Day Per City
WITH TempRank AS (
    SELECT 
        C.CityName,
        W.Date,
        W.Temperature,
        ROW_NUMBER() OVER (
            PARTITION BY C.CityId 
            ORDER BY W.Temperature ASC
        ) AS rnk
    FROM Weather W
    JOIN Cities C ON W.CityId = C.CityId
)
SELECT 
    CityName,
    Date,
    Temperature
FROM TempRank
WHERE rnk = 2;




INSERT INTO Weather VALUES
(3, '2025-06-08', 25.5, 0),
(3, '2025-06-11', 26.0, 0);


INSERT INTO Weather VALUES
(4, '2025-06-10', 32.0, 0),
(4, '2025-06-13', 33.0, 0);

-- Chennai – had rainfall (should NOT appear)
INSERT INTO Weather VALUES
(1, '2025-06-09', 34.2, 4.0);

-- Mumbai – had rainfall (should NOT appear)
INSERT INTO Weather VALUES
(2, '2025-06-10', 31.5, 5.0);

-- New York – NO rainfall (should appear)
INSERT INTO Weather VALUES
(3, '2025-06-11', 25.0, 0.0);

-- Los Angeles – NO rainfall (should appear)
INSERT INTO Weather VALUES
(4, '2025-06-12', 33.0, 0.0);

-- London – had rainfall (should NOT appear)
INSERT INTO Weather VALUES
(5, '2025-06-08', 16.5, 2.5);



--7. Cities Without Rainfall Last 15 Days
SELECT DISTINCT C.CityName
FROM Cities C
WHERE C.CityId NOT IN (
    SELECT DISTINCT W.CityId
    FROM Weather W
    WHERE W.Date >= DATEADD(DAY, -15, '2025-06-13')
      AND W.Precipitation > 0
);


--8. Max Temperature by Country Using CTE
WITH CountryTemps AS (
    SELECT 
        C.Country,
        W.Temperature
    FROM Weather W
    JOIN Cities C ON W.CityId = C.CityId
)

SELECT 
    Country,
    MAX(Temperature) AS MaxTemperature
FROM CountryTemps
GROUP BY Country;

--9. Weather Change Alert: Sudden Drop

WITH TempWithPrevDay AS (
    SELECT 
        C.CityName,
        W.Date,
        W.Temperature,
        LAG(W.Temperature) OVER (
            PARTITION BY W.CityId
            ORDER BY W.Date
        ) AS PrevDayTemp
    FROM Weather W
    JOIN Cities C ON W.CityId = C.CityId
)

SELECT 
    CityName,
    Date,
    Temperature,
    PrevDayTemp,
    PrevDayTemp - Temperature AS TempDrop
FROM TempWithPrevDay
WHERE PrevDayTemp - Temperature >= 10;


--10. Rank Cities by Rainfall for a Given Date
SELECT 
    C.CityName,
    W.Date,
    W.Precipitation,
    RANK() OVER (
        ORDER BY W.Precipitation DESC
    ) AS RainfallRank
FROM Weather W
JOIN Cities C ON W.CityId = C.CityId
WHERE W.Date = '2025-06-02';


--11. Cross Join to Generate Forecast Combinations
WITH Next7Days AS (
    SELECT CAST('2025-06-14' AS DATE) AS ForecastDate
    UNION ALL SELECT DATEADD(DAY, 1, '2025-06-14')
    UNION ALL SELECT DATEADD(DAY, 2, '2025-06-14')
    UNION ALL SELECT DATEADD(DAY, 3, '2025-06-14')
    UNION ALL SELECT DATEADD(DAY, 4, '2025-06-14')
    UNION ALL SELECT DATEADD(DAY, 5, '2025-06-14')
    UNION ALL SELECT DATEADD(DAY, 6, '2025-06-14')
)
SELECT 
    C.CityName,
    D.ForecastDate
FROM Cities C
CROSS JOIN Next7Days D
ORDER BY C.CityName, D.ForecastDate;

--12. Correlated Subquery: Hottest Day by City
SELECT 
    C.CityName,
    W.Date,
    W.Temperature
FROM Weather W
JOIN Cities C ON W.CityId = C.CityId
WHERE W.Temperature = (
    SELECT MAX(W2.Temperature)
    FROM Weather W2
    WHERE W2.CityId = W.CityId
);

--13. Average Weekly Rainfall
SELECT 
    C.CityName,
    DATEPART(WEEK, W.Date) AS WeekNumber,
    ROUND(AVG(W.Precipitation), 2) AS AvgWeeklyRainfall
FROM Weather W
JOIN Cities C ON W.CityId = C.CityId
GROUP BY C.CityName, DATEPART(WEEK, W.Date)
ORDER BY C.CityName, WeekNumber;

--14. Weather Trends by Country
SELECT 
    C.Country,
    MONTH(W.Date) AS MonthNumber,
    ROUND(AVG(W.Temperature), 2) AS AvgMonthlyTemp
FROM Weather W
JOIN Cities C ON W.CityId = C.CityId
GROUP BY C.Country, MONTH(W.Date)
ORDER BY C.Country, MonthNumber;

--15. Anomalies: Temperature Spike
WITH TempCheck AS (
    SELECT 
        C.CityName,
        W.Date,
        W.Temperature,
        LAG(W.Temperature) OVER (
            PARTITION BY W.CityId
            ORDER BY W.Date
        ) AS YesterdayTemp
    FROM Weather W
    JOIN Cities C ON W.CityId = C.CityId
)

SELECT 
    CityName,
    Date,
    Temperature,
    YesterdayTemp,
    Temperature - YesterdayTemp AS TempJump
FROM TempCheck
WHERE Temperature - YesterdayTemp > 5;

