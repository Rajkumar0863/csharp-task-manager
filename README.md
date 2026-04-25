# Task Manager — C# / .NET 8 Console Application

A clean, modular task management console application built with C# and .NET 8, demonstrating Object-Oriented Programming principles, the Repository design pattern, and unit testing with xUnit.

## 🎯 Project Highlights

- **Object-Oriented Programming**: Classes, interfaces, encapsulation, and polymorphism
- **Repository Pattern**: Clean separation of data access from business logic
- **Dependency Injection**: Manual constructor injection for testability
- **JSON Persistence**: File-based storage using `System.Text.Json`
- **Unit Testing**: 14 xUnit tests — 100% pass rate
- **Clean Code**: XML documentation, input validation, and exception handling

## 🏗️ Architecture

```
ConsoleUI          ← User interaction layer
    ↓
TaskService        ← Business logic layer
    ↓
ITaskRepository    ← Abstraction (interface)
    ↓
JsonTaskRepository ← Concrete implementation (JSON persistence)
```

## 🛠️ Tech Stack

- **Language**: C# 12
- **Runtime**: .NET 8
- **Testing Framework**: xUnit
- **Serialization**: System.Text.Json
- **IDE**: Visual Studio Code

## 📁 Project Structure

```
csharp-task-manager/
├── TaskManager/
│   ├── Models/
│   │   ├── TaskItem.cs
│   │   └── TaskPriority.cs
│   ├── Repositories/
│   │   ├── ITaskRepository.cs
│   │   └── JsonTaskRepository.cs
│   ├── Services/
│   │   └── TaskService.cs
│   ├── UI/
│   │   └── ConsoleUI.cs
│   └── Program.cs
└── TaskManager.Tests/
    ├── TaskServiceTests.cs
    └── JsonTaskRepositoryTests.cs
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Build

```bash
dotnet build
```

### Run

```bash
dotnet run --project TaskManager
```

### Test

```bash
dotnet test
```

## ✨ Features

- Add tasks with title, description, due date, and priority (Low / Medium / High)
- List all tasks, pending tasks, or filter by priority
- Mark tasks as complete
- Delete tasks
- Persistent storage via JSON — data survives between sessions

## 🧪 Test Coverage

14 unit tests validating:

- Task creation with valid and invalid input
- Business rule enforcement (empty titles, past due dates)
- Task completion logic
- Priority-based filtering
- Repository CRUD operations
- Data persistence across sessions

## 💡 Design Decisions

- **Repository Pattern** enables swapping JSON storage for a database (e.g., SQL Server, PostgreSQL) without changing business logic.
- **Interface-based design** (`ITaskRepository`) makes unit testing and future extension straightforward.
- **Service layer** centralizes validation and business rules, keeping the UI thin.
- **Value validation at construction** ensures invalid tasks can never exist in the system.

## 🔮 Future Enhancements

- Migrate persistence to **Entity Framework Core** with a real database
- Build an **ASP.NET Core Web API** on top of the service layer
- Add **integration tests** alongside unit tests
- Introduce a **DI container** (`Microsoft.Extensions.DependencyInjection`)
- Add **task categories, tags, and reminders**

## 👤 Author

**Rajkumar Vijayan**  
MSc Software Development — University of Limerick  
[LinkedIn](https://linkedin.com/in/rajkumar-vijayan-0135a8338) | [GitHub](https://github.com/Rajkumar0863)
