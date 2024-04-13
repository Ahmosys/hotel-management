﻿
<div align="center">
<img src="https://cdn.discordapp.com/attachments/1016352658350682133/1228734146332070069/banner.png?ex=662d1ed9&is=661aa9d9&hm=2637b6742ab6b692ff07c7ae3bf4078d3bd2453621f6b2f08769baceaf4bb961&" />
</div>

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
- **NSwag** : Used for API documentation (OpenAPI).

### Testing
- **NUnit** : Unit testing framework.
- **FluentAssertions** : Fluent assertion library for unit tests.
- **Moq** : Mocking library for unit tests.
- **Respawn** : Used for resetting the database state between tests.


## 🎨 Design Principles and Patterns used
### CQRS
CQRS pattern is used to separate read (queries) from write (commands) operations, allowing better data management and optimized performance.

### Domain Events
Domain events pattern are used to communicate changes in the domain model, allowing the application to react to these changes and trigger appropriate actions.

### Repository
The Repository pattern is used to abstract the data access layer, providing a clean and consistent way to access data from the database.

### Guard Clauses
Guard clauses are used to validate input parameters and ensure that the application's business rules are enforced (Failing fast).

### DDD
DDD is used to model the application's business domain, using concepts such as entities, value objects, aggregates, etc, to ensure a software design aligned with business needs.

### SOLID
SOLID principles are applied to ensure code quality, focusing on code simplicity, maintainability, and extensibility.

### Fail Fast
The "Fail Fast" strategy is adopted to detect and report errors as soon as they occur, ensuring a quick and appropriate response to any issues.

### Mediator
The Mediator pattern is used to decouple the components of the application, allowing them to communicate without being directly dependent on each other.

### Static Factory Method
The static factory method pattern is employed to create instances of objects, furnishing a neat and uniform approach for object creation. You can observe the implementation of the static factory method pattern in the `src/Domain/Entities/Room.cs` file for instance. This file encapsulates all properties of a room object, and the static factory method `Create()` is utilized to instantiate room objects with consistent behavior and configuration. This approach not only simplifies the process of creating objects but also ensures adherence to design principles such as encapsulation and separation of concerns.

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

## 🗄️Database
The application utilizes a relational database, specifically Microsoft SQL Server LocalDB (for dev env), for storing data related to users, roles, bookings, rooms, etc. Entity Framework Core is utilized for data persistence management, providing an effective database abstraction.

### Database Schema
Here is a **UML** diagram of the business-side database schema:

<img src="https://cdn.discordapp.com/attachments/1016352658350682133/1228759264110317578/uml-diagram-v2.png?ex=662d363d&is=661ac13d&hm=387d8e0628538602c8ae57135707ff4843e54f3b8f37077df65cf5476a4ab073&" />

Here is a **ERD** diagram of the database schema:

<img src="https://cdn.discordapp.com/attachments/1016352658350682133/1228733944451829842/erd-diagram-v1.png?ex=662d1ea9&is=661aa9a9&hm=02c07d055244b62a9547c7589ab32940518ce8b06f47d4d13fa4de2fe105e438&" />

### Database Migrations
The database schema is managed using Entity Framework Core's migration feature, which allows us to create, update, and rollback database schema changes. The migrations are stored in the `src/Infrastructure/Data/Migrations` directory. At the start of the application, the database is automatically migrated to the latest version.

### Database Seeding
The database is seeded with initial data to facilitate testing and development. The seeding process is performed when running the application using Entity Framework Core's, which populates the database with predefined data. You can find the seeding data in the `src/Infrastructure/Data/ApplicationDbContextInitialiser.cs` file.

## 🚦 API Endpoints
### Users (Identity - ASP.NET Core)
Routes for **all** roles :
- **POST /api/users/login** : Authenticate a user and generate a JWT token.
- **POST /api/users/register** : Register a new user.

👀 See more at [Identity API](https://learn.microsoft.com/fr-fr/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio).

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

## 📖 API Documentation
The API documentation is generated using NSwag and can be accessed at `https://localhost:5001/api/index.html`. It provides detailed information about the available endpoints, request/response models, and authentication requirements.

In addition, we have also implement Postman collection for testing the API endpoints. You can find the collection in the `.docs/postman` folder and import it into Postman to test the API endpoints.

## 🔒 Security
The application uses JWT (JSON Web Token) for authentication and authorization. The JWT token is generated when a user logs in and is used to authenticate subsequent requests. The token contains the user's claims and is validated on each request to ensure that the user has the necessary permissions to access the requested resource.

The application uses **role-based** authorization to control access to different parts of the application. There are three roles defined in the application: Customer, Cleaner, and Receptionist. Each role has specific permissions and access rights to different parts of the application.

## 🌐 External Services
### Email Service (IEmailService)

- **Send a e-mail when customer check-out** : The application uses an external email service to send an email to the user when they check-out. You can find the usage of the email service in the `src/Application/Bookings/EventHandlers/BookingCheckedOutEventHandler.cs` file. In the Infrastructure layer, we have implemented a simple email service that logs the email content to the console just for demonstration purposes.

### Payment Gateway (IPaymentGateway)

- **Process payment when customer book a room or check-in** : The application uses an external payment gateway to process payments when a customer books a room or checks in. You can find the usage of the payment gateway in the `src/Application/Bookings/Commands/CreateBookingCommandHandler.cs` and `src/Application/Bookings/Commands/CheckInBookingCommandHandler.cs` files. In the Infrastructure layer, we have implemented a simple Stripe payment gateway that logs the payment details to the console just for demonstration purposes.

## 🕒 Task Scheduling (Quartz.NET)
The application uses Quartz.NET to schedule asynchronous tasks such as sending reminder emails to users.

- **Pre-stay notification to customers to inform them of the start of their stay (1 day before the start):** : The application uses Quartz.NET to schedule a task (called "Job") that sends reminder emails to customers who have upcoming bookings. You can find the Quartz.NET configuration in the `src/Infrastructure/Jobs/PreStayNotificationJobSetup.cs` file and the job implementation in the `src/Infrastructure/Jobs/PreStayNotificationJob.cs` file. The job is scheduled to run every day at 8:00 AM.

## ⚙️ Installation
### Prerequisites
- **.NET 8.0 SDK** : You need to have .NET 8.0 SDK installed on your machine to run the application.
- **SQL Server LocalDB** : You need to have SQL Server LocalDB installed on your machine to run the application (Not mandatory, you can use another database if you want but by default the application is configured to use SQL Server LocalDB).

### Steps
1. Clone the repository to your local machine:

```bash
git clone https://github.com/Ahmosys/hotel-management.git
```

2. Navigate to the `src/Web` directory:
```bash
cd hotel-management/src/Web
```

3. Run the application using the following command:

```bash
dotnet run
```

4. Open your browser and navigate to `https://localhost:5001/api/index.html` to access the API documentation.

5. You can also import the Postman collection from the `.docs/postman` folder to test the API endpoints.

⚠️ NB : If you want to use another database, you can change the connection string in the `appsettings.json` file in the `src/Web` directory.


## 📝 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ✍️ Authors
- **HR.** - [Ahmosys](https://github.com/Ahmosys)
- **LA.** - [louisalr](https://github.com/louisalr)



