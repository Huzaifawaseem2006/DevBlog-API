# DevBlog API

A blogging platform REST API built with ASP.NET Core 8 and Entity Framework Core.

## Features (completed)
- JWT authentication with refresh token rotation
- User registration and login
- Full CRUD for blog posts
- Pagination
- Layered architecture (Controllers, Services, Repositories)
- Repository pattern with generic and specific interfaces

## In Progress
- Comments system
- Role-based authorization (Admin)
- Likes system
- Image upload

## Tech Stack
- ASP.NET Core 8
- Entity Framework Core 8
- SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication

## How to Run
1. Clone the repository
2. Update connection string in `appsettings.json`
3. Run `Update-Database` in Package Manager Console
4. Run the project and open Swagger UI