using TaskManager.Repositories;
using TaskManager.Services;
using TaskManager.UI;

// Entry point for the Task Manager application
// Demonstrates manual Dependency Injection:
// Program -> UI -> Service -> Repository

var dataFilePath = Path.Combine(
    Directory.GetCurrentDirectory(), 
    "tasks.json"
);

ITaskRepository repository = new JsonTaskRepository(dataFilePath);
var service = new TaskService(repository);
var ui = new ConsoleUI(service);

ui.Run();