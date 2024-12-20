# Library Management Web App

This is a web application designed to manage library assets and patrons, built with ASP.NET Core MVC and Entity Framework Core.

## Features

- **Manage Patrons**: Create, read, update, and delete patron information.
- **Asset Management**: Track library assets such as books, magazines, and other resources.
- **Checkout and Hold**: Patrons can check out and place holds on library assets.
- **Library Branch Management**: Administrators can manage multiple library branches.

## Installation

### Prerequisites

- .NET 5.0 or later
- A compatible SQL database (e.g., SQL Server)

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/jaishmau39/LibraryManagementWeb.git
   ```

2. Navigate to the project directory:

   ```bash
   cd LibraryManagementWeb
   ```

3. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Set up the database by configuring the connection string in `appsettings.json`.

5. Apply database migrations:

   ```bash
   dotnet ef database update
   ```

6. Run the application:

   ```bash
   dotnet run
   ```

The application will be accessible at `http://localhost:5000`.

## Usage

- **Admin Access**: Manage patrons, assets, and transactions.
- **Patron Access**: Browse available assets, place holds, and check out items.
  
## Testing

To run the unit tests:

```bash
dotnet test
```
