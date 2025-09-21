-- SQL DDL generated from the EF entities (approx.)
CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'User',
    CreatedOn DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME()
);

CREATE TABLE Vendors (
    VendorId INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL UNIQUE,
    VendorName NVARCHAR(100) NOT NULL,
    ContactInfo NVARCHAR(200) NULL,
    CONSTRAINT FK_Vendors_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Buses (
    BusId INT IDENTITY PRIMARY KEY,
    VendorId INT NOT NULL,
    BusNumber NVARCHAR(30) NOT NULL,
    BusType NVARCHAR(30) NULL,
    SeatCount INT NOT NULL,
    CONSTRAINT FK_Buses_Vendors FOREIGN KEY (VendorId) REFERENCES Vendors(VendorId)
);

CREATE TABLE Routes (
    RouteId INT IDENTITY PRIMARY KEY,
    Source NVARCHAR(60) NOT NULL,
    Destination NVARCHAR(60) NOT NULL,
    DistanceKm FLOAT NOT NULL,
    Duration BIGINT NOT NULL -- ticks
);

CREATE TABLE Schedules (
    ScheduleId INT IDENTITY PRIMARY KEY,
    BusId INT NOT NULL,
    RouteId INT NOT NULL,
    DepartureTime DATETIME2 NOT NULL,
    ArrivalTime DATETIME2 NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Schedules_Buses FOREIGN KEY (BusId) REFERENCES Buses(BusId),
    CONSTRAINT FK_Schedules_Routes FOREIGN KEY (RouteId) REFERENCES Routes(RouteId)
);

CREATE TABLE Bookings (
    BookingId INT IDENTITY PRIMARY KEY,
    ScheduleId INT NOT NULL,
    UserId INT NOT NULL,
    PNR NVARCHAR(12) NOT NULL UNIQUE,
    SeatNumber INT NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Pending',
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Bookings_Schedules FOREIGN KEY (ScheduleId) REFERENCES Schedules(ScheduleId),
    CONSTRAINT FK_Bookings_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE Payments (
    PaymentId INT IDENTITY PRIMARY KEY,
    BookingId INT NOT NULL UNIQUE,
    UserId INT NOT NULL,
    PaymentDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Amount DECIMAL(18,2) NOT NULL,
    Method NVARCHAR(20) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    CONSTRAINT FK_Payments_Bookings FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId),
    CONSTRAINT FK_Payments_Users FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

CREATE TABLE SeatHolds (
    SeatHoldId INT IDENTITY PRIMARY KEY,
    ScheduleId INT NOT NULL,
    SeatNumber INT NOT NULL,
    ExpiresAt DATETIME2 NOT NULL,
    Token NVARCHAR(64) NOT NULL,
    CONSTRAINT UQ_SeatHold UNIQUE (ScheduleId, SeatNumber),
    CONSTRAINT FK_SeatHolds_Schedules FOREIGN KEY (ScheduleId) REFERENCES Schedules(ScheduleId)
);
