# 📚 BookKeep API

A RESTful API for managing bookkeeping data, built with **.NET 8**, **Entity Framework Core**, and **AutoMapper**. This project uses **SQL Server** for database storage and is documented with **Swagger** via **Swashbuckle**.

---

## 🚀 Tech Stack

- **.NET 8**
- **Entity Framework Core**
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Tools`
- **AutoMapper**
- **Swashbuckle.AspNetCore** (for Swagger/OpenAPI)
- **SQL Server** (managed via **SSMS**)

---

## 📦 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [SSMS (SQL Server Management Studio)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

### 🛠️ Setup Instructions

 - **Clone the repository:**

   ```bash
   git clone https://github.com/your-username/bookkeep-api.git
   cd bookkeep-api
   ```
   

 - **Update the connection string** in `appsettings.json`:
```bash
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=BookKeepDb;Trusted_Connection=True;MultipleActiveResultSets=true; TrustServerCertificate=true"
}
```

 - Restore dependencies and build the project:
```bash
dotnet restore
dotnet build
```

 - Apply EF Core migrations and create the database:
```bash
dotnet ef database update
```

 - Run the application:
```bash
dotnet run
```

 - **Explore the API** via Swagger UI:
 
### 📁 Project Structure
```graphql
BookKeepAPI/
│
├── Controllers/           # API Controllers
├── DTOs/                  # Data Transfer Objects
├── Models/                # Entity Models
├── Helpers/              # Helpers
├── Data/                  # EF Core DbContext and Migrations
├── Program.cs             # Entry point
├── appsettings.json       # Configuration
└── README.md              # Project documentation
```
### 🔧 Useful Commands

 - Add migration:
 ```bash
 dotnet ef migrations add InitialCreate

```

 - Update database:
 ```bash
 dotnet ef database update

```
