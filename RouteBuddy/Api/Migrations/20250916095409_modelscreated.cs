using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class modelscreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LicenseExpiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.RouteId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfilePicture = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VendorName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorId);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    StopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Landmark = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.StopId);
                    table.ForeignKey(
                        name: "FK_Stops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BusType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    RegistrationNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VendorId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusId);
                    table.ForeignKey(
                        name: "FK_Buses_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BusPhotos",
                columns: table => new
                {
                    BusPhotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusPhotos", x => x.BusPhotoId);
                    table.ForeignKey(
                        name: "FK_BusPhotos_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusSchedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Fare = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    RouteId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusSchedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_BusSchedules_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusSchedules_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "RouteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PNRNo = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BookedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_BusSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "BusSchedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DriverAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_DriverAssignments_BusSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "BusSchedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverAssignments_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fares",
                columns: table => new
                {
                    FareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fares", x => x.FareId);
                    table.ForeignKey(
                        name: "FK_Fares_BusSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "BusSchedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingSegments",
                columns: table => new
                {
                    BookingSegmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingSegments", x => x.BookingSegmentId);
                    table.ForeignKey(
                        name: "FK_BookingSegments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingSegments_BusSchedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "BusSchedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cancellations",
                columns: table => new
                {
                    CancellationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CancelledOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancelledBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PenaltyAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cancellations", x => x.CancellationId);
                    table.ForeignKey(
                        name: "FK_Cancellations_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookedSeats",
                columns: table => new
                {
                    BookedSeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SeatType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    BookingSegmentId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookedSeats", x => x.BookedSeatId);
                    table.ForeignKey(
                        name: "FK_BookedSeats_BookingSegments_BookingSegmentId",
                        column: x => x.BookingSegmentId,
                        principalTable: "BookingSegments",
                        principalColumn: "BookingSegmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookedSeats_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookedSeats_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    RefundId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefundAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    RefundMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RefundStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RefundedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    CreadtedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.RefundId);
                    table.ForeignKey(
                        name: "FK_Refunds_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "DriverId", "CreadtedBy", "CreatedOn", "DriverName", "IsActive", "IsDeleted", "LicenseExpiry", "LicenseNumber", "Phone", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ramesh Kumar", true, false, new DateTime(2030, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DL1234567", "9876512345", null, null });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "RouteId", "CreadtedBy", "CreatedOn", "Destination", "Distance", "Duration", "IsDeleted", "Source", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bangalore", 350.0, new TimeSpan(0, 6, 0, 0, 0), false, "Chennai", null, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreadtedBy", "CreatedOn", "DateOfBirth", "Email", "Gender", "IsActive", "IsDeleted", "PasswordHash", "Phone", "ProfilePicture", "Role", "UpdateBy", "UpdatedOn", "UserName" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@routebuddy.com", "Male", true, false, "hashedpwd123", "9876543210", null, "Admin", null, null, "AdminUser" },
                    { 2, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "cust1@routebuddy.com", "Female", true, false, "hashedpwd456", "9876501234", null, "Customer", null, null, "Customer1" }
                });

            migrationBuilder.InsertData(
                table: "Vendors",
                columns: new[] { "VendorId", "CreadtedBy", "CreatedOn", "Email", "IsDeleted", "Status", "UpdateBy", "UpdatedOn", "VendorName" },
                values: new object[] { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "vendor@kanini.com", false, "Active", null, null, "Kanini Travels" });

            migrationBuilder.InsertData(
                table: "Buses",
                columns: new[] { "BusId", "BusName", "BusType", "CreadtedBy", "CreatedOn", "RegistrationNo", "Status", "TotalSeats", "UpdateBy", "UpdatedOn", "VendorId" },
                values: new object[] { 1, "Kanini Express", "Sleeper", "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TN01AB1234", "Active", 40, null, null, 1 });

            migrationBuilder.InsertData(
                table: "Stops",
                columns: new[] { "StopId", "CreadtedBy", "CreatedOn", "IsDeleted", "Landmark", "Name", "RouteId", "UpdateBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Railway Station", "Chennai Central", 1, null, null },
                    { 2, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Bangalore", "Silk Board", 1, null, null }
                });

            migrationBuilder.InsertData(
                table: "BusSchedules",
                columns: new[] { "ScheduleId", "ArrivalTime", "BusId", "CreadtedBy", "CreatedOn", "DepartureTime", "Fare", "RouteId", "Status", "TravelDate", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, new TimeSpan(0, 4, 0, 0, 0), 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 22, 0, 0, 0), 999m, 1, "Scheduled", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "BusId", "Comment", "CreadtedBy", "CreatedOn", "Rating", "UpdateBy", "UpdatedOn", "UserId" },
                values: new object[] { 1, 1, "Very comfortable ride!", "Customer1", new DateTime(2025, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, null, null, 2 });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "BookedAt", "BusId", "CreadtedBy", "CreatedOn", "PNRNo", "ScheduleId", "Status", "TravelDate", "UpdateBy", "UpdatedOn", "UserId" },
                values: new object[] { 1, new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PNR12345", 1, "Confirmed", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2 });

            migrationBuilder.InsertData(
                table: "DriverAssignments",
                columns: new[] { "AssignmentId", "CreadtedBy", "CreatedOn", "DriverId", "ScheduleId", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, null });

            migrationBuilder.InsertData(
                table: "Fares",
                columns: new[] { "FareId", "CreadtedBy", "CreatedOn", "Price", "ScheduleId", "SeatType", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 999m, 1, "Sleeper", null, null });

            migrationBuilder.InsertData(
                table: "BookingSegments",
                columns: new[] { "BookingSegmentId", "BookingId", "CreadtedBy", "CreatedOn", "ScheduleId", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, 1, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, null, null });

            migrationBuilder.InsertData(
                table: "Cancellations",
                columns: new[] { "CancellationId", "BookingId", "CancelledBy", "CancelledOn", "CreadtedBy", "CreatedOn", "PenaltyAmount", "Reason", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, 1, "Customer1", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m, "Personal reasons", null, null });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Amount", "BookingId", "CreadtedBy", "CreatedOn", "PaymentDate", "PaymentMethod", "PaymentStatus", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, 999m, 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mock", "Success", null, null });

            migrationBuilder.InsertData(
                table: "BookedSeats",
                columns: new[] { "BookedSeatId", "BookingId", "BookingSegmentId", "BusId", "CreadtedBy", "CreatedOn", "SeatNumber", "SeatType", "TravelDate", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, 1, 1, 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A1", "Sleeper", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.InsertData(
                table: "Refunds",
                columns: new[] { "RefundId", "CreadtedBy", "CreatedOn", "PaymentId", "RefundAmount", "RefundMethod", "RefundStatus", "RefundedOn", "UpdateBy", "UpdatedOn" },
                values: new object[] { 1, "System", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 899m, "UPI", "Processed", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_BookedSeats_BookingId",
                table: "BookedSeats",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedSeats_BookingSegmentId",
                table: "BookedSeats",
                column: "BookingSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BookedSeats_BusId",
                table: "BookedSeats",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_BusId",
                table: "Bookings",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ScheduleId",
                table: "Bookings",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSegments_BookingId",
                table: "BookingSegments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSegments_ScheduleId",
                table: "BookingSegments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Buses_RegistrationNo",
                table: "Buses",
                column: "RegistrationNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buses_VendorId",
                table: "Buses",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_BusPhotos_BusId",
                table: "BusPhotos",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusSchedules_BusId",
                table: "BusSchedules",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusSchedules_RouteId",
                table: "BusSchedules",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Cancellations_BookingId",
                table: "Cancellations",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriverAssignments_DriverId",
                table: "DriverAssignments",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAssignments_ScheduleId",
                table: "DriverAssignments",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_LicenseNumber",
                table: "Drivers",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fares_ScheduleId",
                table: "Fares",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_PaymentId",
                table: "Refunds",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BusId",
                table: "Reviews",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stops_RouteId",
                table: "Stops",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookedSeats");

            migrationBuilder.DropTable(
                name: "BusPhotos");

            migrationBuilder.DropTable(
                name: "Cancellations");

            migrationBuilder.DropTable(
                name: "DriverAssignments");

            migrationBuilder.DropTable(
                name: "Fares");

            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "BookingSegments");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "BusSchedules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
