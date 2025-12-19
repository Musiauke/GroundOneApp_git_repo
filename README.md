# 🚒 GroundOne - Fire Department Vehicle Management System

Modern web application for managing fire department vehicle fleet, compartments, and equipment inventory.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![React](https://img.shields.io/badge/React-18-61DAFB?logo=react)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Production-4169E1?logo=postgresql)
![License](https://img.shields.io/badge/License-MIT-green.svg)

## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [API Documentation](#api-documentation)
- [Testing](#testing)
- [Deployment](#deployment)
- [Contributing](#contributing)
- [License](#license)

## ✨ Features

### Vehicle Management
- ✅ Complete CRUD operations for fire department vehicles
- ✅ Support for various vehicle types (GBA, GCBA, SD, SRt, etc.)
- ✅ Vehicle status tracking (Available, On Action, Under Maintenance)
- ✅ Inspection date management
- ✅ Search and filter capabilities

### Compartment Organization
- ✅ Multiple compartments per vehicle
- ✅ Location-based organization (Front, Rear, Roof, etc.)
- ✅ Cascading delete protection

### Equipment Inventory
- ✅ Detailed item tracking
- ✅ Category-based organization (Tools, Safety, Medical, Communication)
- ✅ Quantity management
- ✅ Inspection scheduling
- ✅ Status monitoring (Available, In Use, Under Maintenance, Damaged)

## 🛠️ Tech Stack

### Backend
- **Framework**: ASP.NET Core 9.0
- **Database**: 
  - PostgreSQL (Production - Railway)
  - SQLite (Development)
- **ORM**: Entity Framework Core 9.0
- **Architecture**: Repository Pattern + Service Layer
- **Validation**: FluentValidation
- **API Documentation**: Swagger/OpenAPI
- **Logging**: Serilog (optional) / Built-in logging

### Frontend
- **Framework**: React 18
- **Build Tool**: Vite 5
- **UI Library**: Material-UI (MUI) v6
- **HTTP Client**: Axios
- **Routing**: React Router DOM
- **State Management**: React Context API

### DevOps & Hosting
- **Backend Hosting**: Railway
- **Frontend Hosting**: Vercel
- **Database**: Railway PostgreSQL
- **CI/CD**: GitHub Actions (optional)

## 🚀 Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [PostgreSQL](https://www.postgresql.org/) (for production testing, optional for development)
- Git

### Backend Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/groundone.git
   cd groundone
   ```

2. **Navigate to backend**
   ```bash
   cd backend
   ```

3. **Restore dependencies**
   ```bash
   dotnet restore
   ```

4. **Update database connection** (optional - SQLite is default)
   
   Create `appsettings.Development.json` if it doesn't exist:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Data Source=app.db"
     },
     "AllowedOrigins": [
       "http://localhost:5173"
     ]
   }
   ```

5. **Apply migrations**
   ```bash
   dotnet ef database update
   ```

6. **Run the API**
   ```bash
   dotnet run
   ```

   API will be available at:
   - HTTPS: `https://localhost:7000`
   - HTTP: `http://localhost:5000`
   - Swagger UI: `https://localhost:7000/swagger`

### Frontend Setup

1. **Navigate to frontend**
   ```bash
   cd frontend
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Create environment file**
   ```bash
   cp .env.example .env.local
   ```

   Edit `.env.local`:
   ```env
   VITE_API_URL=http://localhost:5000
   ```

4. **Run development server**
   ```bash
   npm run dev
   ```

   Frontend will be available at: `http://localhost:5173`

## 📁 Project Structure

```
GroundOne/
├── backend/
│   ├── Controllers/          # API endpoints
│   ├── Data/                 # DbContext & Seeder
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Middleware/           # Exception handling
│   ├── Models/               # Domain entities
│   │   └── Enums/           # Enumerations
│   ├── Repository/           # Data access layer
│   ├── Services/             # Business logic
│   ├── Validators/           # FluentValidation rules
│   ├── Migrations/           # EF Core migrations
│   ├── Program.cs            # Application entry point
│   └── backend.csproj        # Project file
│
├── frontend/
│   ├── src/
│   │   ├── components/      # React components
│   │   │   ├── admin/       # Admin panel
│   │   │   ├── dialogs/     # Modal dialogs
│   │   │   ├── layout/      # Layout components
│   │   │   └── vehicle/     # Vehicle-specific
│   │   ├── config/          # Configuration
│   │   ├── services/        # API services
│   │   ├── theme/           # MUI theme
│   │   ├── App.jsx          # Main component
│   │   └── main.jsx         # Entry point
│   ├── public/              # Static assets
│   ├── package.json         # Dependencies
│   └── vite.config.js       # Vite configuration
│
└── GroundOneApi.Tests/      # Unit & Integration tests
```

## 📚 API Documentation

### Base URL
- Development: `http://localhost:5000`
- Production: `https://your-api.railway.app`

### Main Endpoints

#### Vehicles
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/vehicle` | Get all vehicles |
| GET | `/api/vehicle/{id}` | Get vehicle by ID |
| GET | `/api/vehicle/search?query={term}` | Search vehicles |
| GET | `/api/vehicle/stats` | Get vehicle statistics |
| POST | `/api/vehicle` | Create new vehicle |
| PUT | `/api/vehicle/{id}` | Update vehicle |
| DELETE | `/api/vehicle/{id}` | Delete vehicle |

#### Compartments
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/compartment` | Get all compartments |
| GET | `/api/compartment/{id}` | Get compartment by ID |
| GET | `/api/compartment/vehicle/{vehicleId}` | Get compartments by vehicle |
| POST | `/api/compartment` | Create compartment |
| PUT | `/api/compartment/{id}` | Update compartment |
| DELETE | `/api/compartment/{id}` | Delete compartment |

#### Items
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/item` | Get all items |
| GET | `/api/item/{id}` | Get item by ID |
| GET | `/api/item/compartment/{compartmentId}` | Get items by compartment |
| POST | `/api/item` | Create item |
| PUT | `/api/item/{id}` | Update item |
| DELETE | `/api/item/{id}` | Delete item |

### Swagger Documentation

Interactive API documentation is available at `/swagger` when running the application.

**Example Request:**
```bash
curl -X POST "https://localhost:7000/api/vehicle" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "GBA 2/16 MAN",
    "type": "GBA",
    "cryptonym": "451-25",
    "registrationNumber": "WE 1234",
    "yearOfManufacture": 2020,
    "status": "Available"
  }'
```

## 🧪 Testing

### Run All Tests
```bash
cd GroundOneApi.Tests
dotnet test
```

### Run with Coverage
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Generate Coverage Report
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"./coverage/**/coverage.cobertura.xml" \
  -targetdir:"./coverage/report" \
  -reporttypes:Html
open ./coverage/report/index.html
```

### Test Structure
- **Unit Tests**: Controllers, Services, Validators
- **Integration Tests**: End-to-end API scenarios
- **Target Coverage**: > 70%

## 🌐 Deployment

### Backend - Railway

1. **Create Railway project**
   ```bash
   railway login
   railway init
   ```

2. **Add PostgreSQL**
   - Go to Railway dashboard
   - Click "+ New" → "Database" → "PostgreSQL"

3. **Configure environment variables**
   ```
   ConnectionStrings__DefaultConnection=${{Postgres.DATABASE_URL}}
   ASPNETCORE_ENVIRONMENT=Production
   AllowedOrigins__0=https://your-app.vercel.app
   ```

4. **Deploy**
   ```bash
   railway up
   ```

### Frontend - Vercel

1. **Install Vercel CLI**
   ```bash
   npm install -g vercel
   ```

2. **Deploy**
   ```bash
   cd frontend
   vercel
   ```

3. **Configure environment**
   - Add `VITE_API_URL` in Vercel dashboard
   - Set to your Railway backend URL

### Database Migrations (Production)

```bash
# Generate migration
dotnet ef migrations add MigrationName

# Update production database (Railway)
dotnet ef database update --connection "your-production-connection-string"
```

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Code Standards
- Follow C# coding conventions
- Use meaningful commit messages
- Write tests for new features
- Update documentation

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

- GitHub: [@musiauke](https://github.com/musiauke)

## 🙏 Acknowledgments

- Fire Department vehicle type codes based on Polish fire service standards
- Built for portfolio demonstration purposes
- Special thanks to the open-source community

## 📊 Project Status

- ✅ Core functionality complete
- ✅ API documentation
- ✅ Unit tests
- ✅ Ready for production deployment
- 🚧 Additional features in development

## 📈 Future Enhancements

- [ ] User authentication (JWT)
- [ ] Role-based access control
- [ ] Mobile app (React Native)
- [ ] Real-time notifications
- [ ] Advanced reporting
- [ ] Export to PDF/Excel
- [ ] Multi-language support

---

**Live Demo**: [https://your-app.vercel.app](https://your-app.vercel.app)  
**API Docs**: [https://your-api.railway.app/swagger](https://your-api.railway.app/swagger)

⭐ Star this repo if you find it helpful!