# 👁️ EyeCareHub

A comprehensive ASP.NET Core Web API platform for managing and tracking all operations within the eye-care ecosystem — products, doctors, patients, pharmacies, and medical articles.

🔗 **Live API Documentation (Swagger):** [http://eyecarehub.runasp.net/swagger/index.html](http://eyecarehub.runasp.net/swagger/index.html)

---

## 📌 Overview

EyeCareHub gives administrators the tools to manage products, doctors and their schedules, and to publish medical articles. Users can book appointments with doctors based on available time slots, access their personal medical records, and interact with content through likes, comments, saved posts, and doctor ratings.

## ✨ Features

**Admin**
- Add, update, and manage products
- Manage doctors and their schedules
- Manage pharmacies
- Publish and manage medical articles

**Users / Patients**
- Book appointments with doctors based on their available time slots
- Access personal medical records
- Like, comment on, and save articles/posts
- Rate doctors

**Platform**
- RESTful endpoints documented with Swagger
- Redis caching for faster data retrieval and scalability
- Optimized queries for real-time performance
- Image hosting for product and doctor images via a configurable `BaseURL`
- Database migrations managed with EF Core

## 🛠️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API |
| Language | C# |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Caching | Redis |
| API Docs | Swagger (OpenAPI) |
| Data Format | JSON |
| Hosting | RunASP.NET |

## 🏗️ Project Structure

The solution follows a layered architecture:

```
EyeCareHub.API.sln
├── EyeCareHub.API   # Presentation layer – controllers, configuration, Swagger
├── EyeCareHub.BLL   # Business logic layer – services and business rules
└── EyeCareHub.DAL   # Data access layer – entities, DbContext, repositories, migrations
```

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (the version targeted by the solution)
- SQL Server
- Redis

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/El-Mos/EyeCareHub.git
   cd EyeCareHub
   ```

2. **Configure `appsettings.json`** in `EyeCareHub.API`

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=EyeCareHub;Trusted_Connection=True;TrustServerCertificate=True;",
       "Redis": "localhost:6379"
     },
     "BaseURL": "https://localhost:5001"
   }
   ```
   > Key names may differ slightly — check the `appsettings.json` in the repo and adjust accordingly.

3. **Apply database migrations**
   ```bash
   dotnet ef database update --project EyeCareHub.DAL --startup-project EyeCareHub.API
   ```

4. **Run the API**
   ```bash
   dotnet run --project EyeCareHub.API
   ```

5. **Open Swagger UI** at `https://localhost:<port>/swagger`

## 📖 API Documentation

Full, interactive documentation for every endpoint is available on Swagger:

👉 [http://eyecarehub.runasp.net/swagger/index.html](http://eyecarehub.runasp.net/swagger/index.html)

The API is organized around these resources:

- **Products** – catalog management and tracking
- **Doctors** – profiles, schedules, and ratings
- **Patients** – accounts and medical records
- **Appointments** – booking based on doctor availability
- **Pharmacies** – pharmacy management
- **Articles** – medical content with likes, comments, and saved posts

## 🌐 Deployment

The API is deployed on **RunASP.NET** cloud hosting, with EF Core migrations applied to the hosted SQL Server database.

## 👤 Author

**Mostafa Abdel-Hakem**

- GitHub: [@El-Mos](https://github.com/El-Mos)
- Email: mkemomostafa452@gmail.com

---

⭐ If you find this project useful, consider giving it a star!
