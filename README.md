# 🔐 Konecta N-Tier Architecture API

Konecta N-Tier is a secure, scalable, and modular **ASP.NET Core Web API** project structured using a clean **N-Tier architecture** (API, Service, Data, Core).  
It demonstrates best practices for building enterprise-level APIs with a strong focus on separation of concerns, security, and maintainability.

### ✨ Key Features

- 🔐 **JWT Authentication & Role-based Authorization**: Admins can create, update, and delete products, while users can only view them.
- 📦 **CRUD APIs**: Full Create, Read, Update, Delete support for managing products.
- 🔁 **Hosted Background Service**: Periodically fetches external API data and writes it to a local JSON file.
- 🧪 **Swagger UI**: Fully documented API with built-in token-based authorization for easy testing.
- 🧱 **Layered Architecture**: Clearly separated Core (models/interfaces), Data (repositories), Service (business logic), and API layers.

---

## 📦 Tech Stack

- **.NET 8.0**  
  The latest version of .NET for modern, high-performance APIs.

- **ASP.NET Core Web API**  
  Framework used for building RESTful APIs.

- **JWT Bearer Authentication**  
  Secure token-based login system with support for Admin/User roles.

- **Swagger**  
  For documenting and testing API endpoints directly in the browser.

- **CORS Configuration**  
  Allows API access from frontend apps running on different domains (like React, Angular, etc.).

- **HttpClientFactory**  
  Used in the background service to call external HTTP endpoints efficiently.

- **BackgroundService**  
  A hosted worker that runs on a timer in the background to fetch and log data from a remote API.

- **Dependency Injection**  
  Built-in .NET DI used throughout the project for services, repositories, and HttpClients.

- **Clean Architecture Principles**  
  With separate layers for Models, Interfaces, Repositories, Services, and Controllers.
