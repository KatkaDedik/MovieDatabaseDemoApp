# MovieApp – .NET 8 Backend

A backend project built as a technical assignment, focusing on clean architecture principles, layered design, and modern C# practices.

---

## Overview

This project is a **.NET 8 Web API** application that:
- Loads movie and actor data from **JSON** and **XML** files.
- Stores them in a **SQLite database** using **Entity Framework Core**.
- Exposes a **REST API** endpoint (`/api/statistics`) that returns computed statistics using **LINQ**.
- Demonstrates **Clean Architecture**, **Repository Pattern**, and **Dependency Injection**.

---

## Technologies Used

| Layer | Technologies |
|-------|---------------|
| API | ASP.NET Core 8, Swagger / OpenAPI |
| Application | DTOs, Interfaces, LINQ Logic |
| Infrastructure | EF Core, SQLite, File Readers, Data Seeder |
| Domain | Entity Models, Extension Methods |
| Testing | xUnit, Moq |

---
## Features

✅ Load data from JSON (`movies.json`) and XML (`actors.xml`)  
✅ Persist data into SQLite database  
✅ Automatic data seeding on startup  
✅ Strongly typed configuration with `IOptions<T>`  
✅ Repository pattern (no direct DbContext access)  
✅ LINQ-based statistics computation  
✅ Swagger API documentation  
✅ Unit tests for key components  

