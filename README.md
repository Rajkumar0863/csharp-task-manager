# Task Manager — C# / .NET 8 Console Application

A clean, modular task management console application built with C# and .NET 8, demonstrating Object-Oriented Programming principles, the Repository design pattern, and unit testing with xUnit.

## 🎯 Project Highlights

- **Object-Oriented Programming**: Classes, interfaces, encapsulation, and polymorphism
- **Repository Pattern**: Clean separation of data access from business logic
- **Dependency Injection**: Manual constructor injection for testability
- **JSON Persistence**: File-based storage using `System.Text.Json`
- **Unit Testing**: 14 xUnit tests covering service and repository layers
- **Clean Code**: XML documentation, input validation, and exception handling

## 🏗️ Architecture

┌─────────────────┐ │ ConsoleUI │ ← User interaction layer └────────┬────────┘ │ ┌────────▼────────┐ │ TaskService │ ← Business logic layer └────────┬────────┘ │ ┌────────▼────────┐ │ ITaskRepository │ ← Abstraction (interface) └────────┬────────┘ │ ┌────────▼────────┐ │JsonTaskRepository│ ← Concrete implementation (JSON persistence) └─────────────────┘


## 🛠️ Tech Stack

- **Language**: C# 12
- **Runtime**: .NET 8
- **Testing Framework**: xUnit
- **Serialization**: System.Text.Json
- **IDE**: Visual Studio Code

## 📁 Project Structure

csharp-task-manager/ ├── TaskManager/ │ ├── Models/ │ │ ├── TaskItem.cs │ │ └── TaskPriority.cs │ ├── Repositories/ │ │ ├── ITaskRepository.cs │ │ └── JsonTaskRepository.cs │ ├── Services/ │ │ └── TaskService.cs │ ├── UI/ │ │ └── ConsoleUI.cs │ └── Program.cs └── TaskManager.Tests/ ├── TaskServiceTests.cs └── JsonTaskRepositoryTests.cs


## 🚀 Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Build
```bash
dotnet build