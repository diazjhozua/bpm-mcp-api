<p align="center">
  <a href="" rel="noopener">
 <img width=200px height=200px src="https://i.imgur.com/6wj0hh6.jpg" alt="Project logo"></a>
</p>

<h3 align="center">BPM MCP API</h3>


---

<p align="center"> A comprehensive Business Process Management API built for hackathon purposes, providing endpoints for employee expense management, travel requests, asset tracking, and purchase request workflows.
    <br>
</p>

## 📝 Table of Contents

- [About](#about)
- [Getting Started](#getting_started)
- [Deployment](#deployment)
- [Usage](#usage)
- [Built Using](#built_using)
- [API Endpoints](#api_endpoints)
- [Authors](#authors)
- [Acknowledgments](#acknowledgement)

## 🧐 About <a name = "about"></a>

The BPM MCP API is a .NET 8 Web API designed for hackathon demonstration purposes. It simulates common business process management workflows including employee expense tracking, travel request management, asset allocation, and purchase request processing. This application does not reflect any specific business flow in a company and is built purely for educational and demonstration purposes.

The API features comprehensive Swagger documentation, Entity Framework Core integration with SQL Server, and follows clean architecture principles with proper validation and error handling.

## 🏁 Getting Started <a name = "getting_started"></a>

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See [deployment](#deployment) for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them.

```
.NET 8 SDK
SQL Server (LocalDB or full instance)
Visual Studio 2022 or VS Code
```

### Installing

A step by step series of examples that tell you how to get a development env running.

Clone the repository

```
git clone <repository-url>
cd bpm-mcp-api
```

Set up user secrets for database connection

```
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\\mssqllocaldb;Database=BpmMcpDb;Trusted_Connection=true;MultipleActiveResultSets=true"
```

Restore dependencies and run migrations

```
dotnet restore
dotnet ef database update
```

Run the application

```
dotnet run
```

The API will be available at `https://localhost:7139` and `http://localhost:5139` with Swagger UI accessible at `/swagger`.

## 🔧 Running the tests <a name = "tests"></a>

Currently, this project is focused on demonstration and does not include automated tests. For a production environment, consider adding:

### Integration Tests

Test the API endpoints with a test database

```
dotnet test --configuration Release
```

### Unit Tests

Test individual components and business logic

```
dotnet test --logger trx --collect:"XPlat Code Coverage"
```

## 🎈 Usage <a name="usage"></a>

The API provides the following main functionalities:

- **Employee Expense Management**: Submit and track employee expenses
- **Travel Request Processing**: Create travel requests and submit related expenses
- **Asset Management**: View employee assets and available asset types
- **Purchase Request Workflow**: Create purchase requests for multiple items

Access the interactive API documentation at `/swagger` when running the application locally.

## 🚀 Deployment <a name = "deployment"></a>

For production deployment:

1. Update connection strings using environment variables
2. Configure proper logging and monitoring
3. Set up SSL certificates
4. Consider using Azure App Service or similar cloud platform
5. Ensure database is properly configured with backup strategies

## ⛏️ Built Using <a name = "built_using"></a>

- [.NET 8](https://dotnet.microsoft.com/) - Web Framework
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) - ORM
- [SQL Server](https://www.microsoft.com/en-us/sql-server/) - Database
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - API Documentation
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/) - Web API Framework

## 📋 API Endpoints <a name = "api_endpoints"></a>

### Employees
- `POST /api/employees/expenses` - Submit employee expense

### Travels
- `GET /api/travels/requests/{id}` - Get travel request by ID
- `POST /api/travels/expenses` - Submit travel expense

### Assets
- `GET /api/assets` - Get assets by employee (query parameter)
- `GET /api/assets/types` - Get all available asset types

### Purchases
- `POST /api/purchases/requests` - Create purchase request

## ✍️ Authors <a name = "authors"></a>

- [@diazjhozua](https://github.com/diazjhozua) - Idea & Initial work

See also the list of [contributors](https://github.com/kylelobo/The-Documentation-Compendium/contributors) who participated in this project.

## 🎉 Acknowledgements <a name = "acknowledgement"></a>

- Microsoft for the excellent .NET ecosystem
- Entity Framework Core team for the robust ORM
- Swashbuckle team for comprehensive API documentation tools
- The open-source community for inspiration and best practices