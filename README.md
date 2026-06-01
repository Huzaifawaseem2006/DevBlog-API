```markdown
# DevBlog API

A production-style blogging platform REST API built with ASP.NET Core 8, following clean layered architecture principles. Built as a learning project to solidify backend engineering fundamentals.

---

## Features

### Authentication & Authorization
- JWT authentication with refresh token rotation
- Role-based authorization (Admin / User)
- Automatic role seeding on startup
- Admin account seeded on first run

### Blog Posts
- Full CRUD with ownership enforcement
- Slug generation from title
- Server-side pagination
- Full-text search by title and content
- Tag assignment (many-to-many)
- Like / unlike system

### Comments
- Full CRUD with ownership enforcement
- Admin override for deletion

### Tags
- User-created tags
- Admin-only update and delete
- Tag filtering by post

### Architecture & Patterns
- Layered architecture (Controllers → Services → Repositories)
- Generic `IRepository<T>` with entity-specific extensions
- `Result<T>` pattern replacing exception-driven flow
- DTOs with manual mapping via static helper classes
- Global action filter for model validation
- `CancellationToken` support throughout async pipeline
- ASP.NET Core Identity with custom `ApplicationUser`

---

## Tech Stack

| Technology | Version |
|---|---|
| ASP.NET Core | 8.0 |
| Entity Framework Core | 8.0 |
| SQL Server | - |
| ASP.NET Core Identity | 8.0 |
| JWT Bearer Authentication | - |

---

## Project Structure

```
DevBlog/
  Controllers/        ← HTTP layer
  Filters/            ← Action filters
  Middleware/         ← Global exception handling
  Core/
    Entities/         ← Domain models
    Interfaces/       ← Service and repository contracts
    DTOs/             ← Data transfer objects
    Helpers/          ← Mapping classes, Result<T>, PagedResult<T>
  Infrastructure/
    Data/             ← DbContext and migrations
    Repositories/     ← EF Core implementations
    Services/         ← Business logic
```

---

## How to Run

1. Clone the repository
2. Update the connection string in `appsettings.json`
3. Run `Update-Database` in Package Manager Console
4. Run the project and open Swagger UI at `https://localhost:{port}/swagger`

> **Default Admin Account**
> - Email: `admin@devblog.com`
> - Password: `Admin@1234`

---

## API Endpoints

| Group | Endpoints |
|---|---|
| **Auth** | `POST /api/account/register` `POST /api/account/login` `POST /api/account/refreshtoken` |
| **Posts** | `GET /api/post` `GET /api/post/{id}` `GET /api/post/search` `GET /api/post/author/{authorId}` `POST /api/post` `PUT /api/post/{id}` `DELETE /api/post/{id}` |
| **Comments** | `GET /api/comment/{id}` `GET /api/comment/post/{postId}` `POST /api/comment/post/{postId}` `PUT /api/comment/{id}` `DELETE /api/comment/{id}` |
| **Tags** | `GET /api/tag` `GET /api/tag/{id}` `GET /api/tag/post/{postId}` `POST /api/tag` `PUT /api/tag/{id}` `DELETE /api/tag/{id}` |
| **Likes** | `POST /api/like/{postId}` `DELETE /api/like/{postId}` `GET /api/like/{postId}/count` |
```
