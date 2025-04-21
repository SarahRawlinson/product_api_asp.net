# ProductApi

![.NET Version](https://img.shields.io/badge/.NET-8.0-blue)

A simple ASP.NET Core Web API project built with .NET 8 for managing products.  
Includes full environment variable support and Swagger UI for testing.

---

## Setup Instructions

### 1. Clone the repository

```bash
    git clone https://github.com/SarahRawlinson/product_api_asp.net.git
    cd ProductApi
```

### 2. Restore dependencies

```bash
    dotnet restore
```

## Environment Setup

This project uses a .env file to manage the database connection string securely (like Laravel).

### 1. Create your .env file

   Copy .env.example:
```bash
    cp .env.example .env
```

### 2. Edit .env and set your database connection

```env
    DB_CONNECTION=Server=YOUR_SERVER_NAME\\YOUR_INSTANCE;Database=ProductApiDb;Trusted_Connection=True;TrustServerCertificate=True;
```
Example for SQL Express:
```env
    DB_CONNECTION=Server=localhost\\SQLEXPRESS;Database=ProductApiDb;Trusted_Connection=True;TrustServerCertificate=True;
```

## Database Setup

### 1. Run database migrations

```bash
  dotnet ef database update
```
(If dotnet ef is missing, install Entity Framework CLI globally:)
```bash
  dotnet tool install --global dotnet-ef
```
(if creating)
```bash
  dotnet ef migrations add InitialCreate
```

## Running the Application

```bash
  dotnet run
```
The API will be available at:
```url
  http://localhost:5073/swagger
```

## Notes
[Entity Framework Core tools reference](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)