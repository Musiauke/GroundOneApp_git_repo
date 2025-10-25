# GroundOne Project – Fire Department Equipment & Vehicle Management System

Full-stack web application for managing fire department equipment and vehicles. Built with **ASP.NET Core 9** backend and **React + Vite** frontend, this project demonstrates modern REST API architecture, CRUD operations, and professional deployment practices.

---

## 🚒 About The Project

GroundOne is a management system designed for fire departments to track and maintain their equipment and vehicles. The application provides a centralized platform for inventory management, maintenance scheduling, and resource allocation.

### Key Features

- **Equipment Management** – Track firefighting equipment, tools, and supplies
- **Vehicle Fleet Management** – Monitor fire trucks, ambulances, and support vehicles
- **Maintenance Records** – Schedule and log maintenance activities
- **RESTful API** – Clean, documented API with Swagger UI
- **Modern SPA Frontend** – Fast, responsive React interface
- **Unit Tested** – Comprehensive backend test coverage

---

## 🛠️ Tech Stack

**Backend:**
- ASP.NET Core 9 (Web API)
- Entity Framework Core
- SQLite Database
- xUnit (Testing)
- Swagger/OpenAPI

**Frontend:**
- React 18
- Vite
- Modern JavaScript (ES6+)

---

## 📁 Project Structure

```
GroundOneApi/
├── backend/                    # ASP.NET Core Web API
│   ├── Controllers/           # API endpoints
│   ├── Data/                  # Database context
│   ├── DTOs/                  # Data transfer objects
│   ├── Migrations/            # EF Core migrations
│   ├── Models/                # Domain models (Equipment, Vehicles)
│   ├── Repository/            # Data access layer
│   ├── Services/              # Business logic
│   ├── Program.cs
│   └── appsettings.json
├── frontend/                   # React SPA
│   ├── src/
│   │   ├── components/        # React components
│   │   ├── services/          # API integration
│   │   └── App.jsx
│   ├── index.html
│   └── vite.config.js
└── GroundOneApi.Tests/        # Unit tests
```


## 🎯 Use Cases

- Fire departments managing inventory
- Equipment lifecycle tracking
- Vehicle maintenance scheduling
- Resource allocation and reporting
- Training tool for learning full-stack development

---

## 🚢 Deployment

This project is ready for deployment to:
- **Azure App Service** (Backend + Frontend)
- **Render** or **Railway** (Quick deployments)
- **Docker** (Containerized deployment)
- **Vercel/Netlify** (Frontend only)

---

## 📝 Future Enhancements

- [ ] User authentication & authorization
- [ ] Real-time notifications
- [ ] Advanced reporting & analytics
- [ ] Mobile app version
- [ ] Integration with external systems

---

## ⭐ Show Your Support

Give a ⭐️ if this project helped you learn or build something cool!

---

**Built with passion for the fire service community and modern web development** 🚒🔥
