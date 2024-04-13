# 🏨 Hotel Management
This project involves the development of a web API application in C# dedicated to the management of a hotel. The application covers various essential aspects such as room booking, management of users and hotel services. The main objective is to apply object-oriented programming concepts, architecture and design patterns, while ensuring the functionality, security and quality of the code.


## 💻 Technologies

### General
- **ASP.NET Core 8.0** : Framework used for minimal web API development.
- **Entity Framework Core 8.0** : Used for data persistence management (ORM).
- **MediatR** : Used for command and query mediation.
- **AutoMapper** :  Used for object-object mapping.
- **FluentValidation** : Used for input validation.
- **Quartz.NET** : Used for asynchronous task scheduling.
- **Ardalis.GuardClauses** : Used for checking for invalid inputs up front and immediately failing if any are found.

### Testing
- **NUnit** : Unit testing framework.
- **FluentAssertions** : Fluent assertion library for unit tests.
- **Moq** : Mocking library for unit tests.
- **Respawn** : Used for resetting the database state between tests.

## 🎨 Design principles

### CQRS
CQRS is used to separate read (queries) from write (commands) operations, allowing better data management and optimized performance.

### DDD
DDD is used to model the application's business domain, using concepts such as entities, value objects, aggregates, etc, to ensure a software design aligned with business needs.

### SOLID
SOLID principles are applied to ensure code quality, focusing on code simplicity, maintainability, and extensibility.

### Fail Fast
The "Fail Fast" strategy is adopted to detect and report errors as soon as they occur, ensuring a quick and appropriate response to any issues.

### Domain Events
Domain events are used to communicate changes in the domain model, allowing the application to react to these changes and trigger appropriate actions.

### Mediator
The Mediator pattern is used to decouple the components of the application, allowing them to communicate without being directly dependent on each other.

### Repository
The Repository pattern is used to abstract the data access layer, providing a clean and consistent way to access data from the database.

### Guard Clauses
Guard clauses are used to validate input parameters and ensure that the application's business rules are enforced (Failing fast).

## 🗄️Database
The application utilizes a relational database, specifically Microsoft SQL Server LocalDB (for dev env), for storing data related to users, roles, bookings, rooms, etc. Entity Framework Core is utilized for data persistence management, providing an effective database abstraction.

### Database Schema
Here is a UML diagram of the business-side database schema:

// Put the UML diagram here

### Database Seeding
The database is seeded with initial data to facilitate testing and development. The seeding process is performed when running the application using Entity Framework Core's, which populates the database with predefined data. You can find the seeding data in the `src/Infrastructure/Data/ApplicationDbContextInitialiser.cs` file.


## 📚 Structure
The application is structured following the principles of Clean Architecture, which consists of four layers: Presentation (Web API), Application, Domain, and Infrastructure.

### Web
The Web layer is responsible for handling HTTP requests and responses, as well as input validation and serialization. It contains the controllers and other components related to the web API.

### Application
The Application layer contains the application's business logic, including commands, queries, and handlers. It is responsible for orchestrating the flow of data between the Presentation and Domain layers.

### Domain
The Domain layer contains the core business entities, value objects, and domain services. It represents the business domain and encapsulates the business rules and logic.

### Infrastructure
The Infrastructure layer contains the implementation details, such as data access, external services such as email service and payment, and other infrastructure-related concerns. It is responsible for interacting with external systems and providing the necessary infrastructure for the application to run.

## 🚦 API Endpoints

### Users (provided by Microsoft Identity)
Routes for **all** roles :
- **POST /api/users/login** : Authenticate a user and generate a JWT token.

### Rooms

Routes for **Customer** role :
- **GET /api/rooms/available/customer/?StartDate={StartDate}&EndDate={EndDate}** : Get available rooms between two dates.
  
Routes for **Cleaner** role :
- **GET /api/rooms/to-clean** : Get all rooms to clean.
- **PUT /api/rooms/{roomId}/clean** : Mark a room as cleaned.

Routes for **Receptionist** role :
- **GET /api/rooms/available/receptionist/?StartDate={StartDate}&EndDate={EndDate}** : Get available rooms between two dates but with the status of the room (New, Renovated, NeedRenovation...). 

### Bookings

Routes for **Customer** role :
- **POST /api/bookings** : Create a new booking with possibility to pay directly.
- **PUT /api/bookings/{bookingId}/cancel** : Cancel a booking and refund the customer if it's possible.

Routes for **Receptionist** role :
- **PUT /api/bookings/{bookingId}/check-in** : Check-in a booking so the room is not available anymore.
- **PUT /api/bookings/{bookingId}/check-out** : Check-out a booking so the room need to be cleaned.
- **PUT /api/bookings/{bookingId}/cancel** : Cancel a booking and refund the customer if he wants to, no rules apply for the receptionist.

## 🔒 Security
The application uses JWT (JSON Web Token) for authentication and authorization. The JWT token is generated when a user logs in and is used to authenticate subsequent requests. The token contains the user's claims and is validated on each request to ensure that the user has the necessary permissions to access the requested resource.

### Roles
The application uses role-based authorization to control access to different parts of the application. There are three roles defined in the application: Customer, Cleaner, and Receptionist. Each role has specific permissions and access rights to different parts of the application.

## ⚙️ Installation


## 🚀 Usage


## 📝 License


## Authors



