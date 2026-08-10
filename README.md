# Movimentos Manuais

A web application for registering and consulting **Manual Movements**, built with **ASP.NET WebForms**, following **Clean Architecture** principles and the **Model-View-Presenter (MVP)** pattern.

## About the Project

This system allows users to register manual financial movements with the following features:

- Register new manual movements
- Product and Cosif selection (cascading dropdowns)
- Automatic generation of the launch number
- Listing of registered movements
- Business and form validations

## Technologies

- **Language:** C#
- **Framework:** ASP.NET WebForms (.NET Framework 4.8)
- **IDE:** Visual Studio 2026
- **Data Access:** ADO.NET
- **Database:** SQL Server
- **Architecture:** Clean Architecture + MVP

## Architecture

The solution follows **Clean Architecture** with the following layers:

```
src/
├── MovimentosManuais.Domain          → Entities and Interfaces (core)
├── MovimentosManuais.Application     → Use cases / Services
├── MovimentosManuais.Infrastructure  → Implementations (ADO.NET)
└── MovimentosManuais.Web             → UI (WebForms + MVP)
```


### Applied Principles

- Dependency Inversion
- Separation of Concerns
- Single Responsibility Principle
- Model-View-Presenter (MVP) in the presentation layer

## Project Structure
```
MovimentosManuais.sln
│
├── src
│   ├── MovimentosManuais.Domain
│   │   ├── Entities
│   │   └── Interfaces
│   │
│   ├── MovimentosManuais.Application
│   │   ├── Interfaces
│   │   └── Services
│   │
│   ├── MovimentosManuais.Infrastructure
│   │   └── Data/Repositories
│   │
│   └── MovimentosManuais.Web
│       ├── Presenters
│       ├── Views
│       └── MovimentosManuaisHome.aspx
```

## Getting Started

### Prerequisites

- Visual Studio 2022 or 2026
- .NET Framework 4.8
- SQL Server (LocalDB, Express, or full instance)

### Steps

1. Clone the repository
2. Open the solution `MovimentosManuais.sln` in Visual Studio
3. Configure the connection string in `web.config`:

```xml
<connectionStrings>
  <add name="MovimentosManuaisConnection"
       connectionString="Data Source=YOUR_SERVER;Initial Catalog=YOUR_DATABASE;Integrated Security=True;TrustServerCertificate=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Run the project (F5)

The default page is MovimentosManuaisHome.aspx.

### Database
Tables

- PRODUTO
- PRODUTO_COSIF
- MOVIMENTO_MANUAL

Stored Procedure -> usp_ListarMovimentosManuais → Used to load the GridView.

### Features
- Load Products: Fills the Product DropDownList
- Load Cosif (cascading): Fills the Cosif DropDownList based on the selected Product
- Insert Movement: Inserts a new record into the MOVIMENTO_MANUAL table
- Automatic Launch Number: Calculates the next launch number for the same month/year
- Listing: Displays registered movements in a GridView
- Clear: Clears all form fields
- New: Re-enables the form fields after a successful insert
- Validations: Month (1-12), Year, monetary value, Description (max 50 characters), etc.

### Business Rules
- NUM_LANCAMENTO: Automatically generated (last launch of the same month/year + 1)
- COD_USUARIO: Always saved as SCOTT_TIGER
- DAT_MOVIMENTO: Date and time of the insert
- DES_DESCRICAO: Maximum of 50 characters
- DAT_MES: Must be between 1 and 12
- VAL_VALOR: Must be a valid monetary value greater than zero

### Security
- All database access uses parameterized queries (SqlParameter)
- Listing is performed through a Stored Procedure
- The application is protected against SQL Injection

### Author
Developed as a practical exercise applying Clean Architecture and MVP with ASP.NET WebForms.
