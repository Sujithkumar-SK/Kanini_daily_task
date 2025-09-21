# RouteBuddy – EF Core MVC Starter

This is a **starter scaffold** for your RouteBuddy group project (5 members) using **ASP.NET Core MVC + EF Core (SQL Server)**.

> Created: 2025-08-17

## Quick Start
1. Create a new MVC project (so you get SDK assets & .csproj):
   ```bash
   dotnet new mvc -n RouteBuddy
   ```
2. Replace the generated `RouteBuddy` folder with `/src/RouteBuddy` from this zip.
3. Install packages:
   ```bash
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
   dotnet add package Microsoft.AspNetCore.Authentication.Cookies
   ```
4. Update **appsettings.json** connection string.
5. Run migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```
6. Run:
   ```bash
   dotnet run
   ```

## Modules (Team of 5)
- Member 1: Auth & Profile (AccountController + Views)
- Member 2: Search (SearchController + Views)
- Member 3: Booking & Seat (BookingController + Views)
- Member 4: Payment (PaymentController + Views)
- Member 5: Admin & Vendor (AdminController + Views)

## Important
- Concurrency: seat lock uses a short-lived hold + transaction; see `BookingService.cs` and comments.
- Payment is simulated. Replace with real gateway later.
- This scaffold is intentionally simple: cookie auth, no Identity to keep learning scope focused.
