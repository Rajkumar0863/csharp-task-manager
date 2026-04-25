using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.UI;

/// <summary>
/// Console-based user interface for the Task Manager application.
/// Handles user input and display formatting.
/// </summary>
public class ConsoleUI
{
    private readonly TaskService _service;

    public ConsoleUI(TaskService service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    public void Run()
    {
        Console.WriteLine("=== Task Manager v1.0 ===\n");

        while (true)
        {
            ShowMenu();
            var choice = Console.ReadLine()?.Trim();

            try
            {
                switch (choice)
                {
                    case "1": ListAllTasks(); break;
                    case "2": AddTask(); break;
                    case "3": CompleteTask(); break;
                    case "4": DeleteTask(); break;
                    case "5": ListByPriority(); break;
                    case "6": ListPending(); break;
                    case "0":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("\n--- MENU ---");
        Console.WriteLine("1. List all tasks");
        Console.WriteLine("2. Add a new task");
        Console.WriteLine("3. Mark task as complete");
        Console.WriteLine("4. Delete a task");
        Console.WriteLine("5. Filter by priority");
        Console.WriteLine("6. Show pending tasks");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    private void ListAllTasks()
    {
        var tasks = _service.GetAllTasks().ToList();
        Console.WriteLine("\n--- All Tasks ---");

        if (!tasks.Any())
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (var task in tasks)
            Console.WriteLine(task);
    }

    private void AddTask()
    {
        Console.Write("Enter title: ");
        var title = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter description: ");
        var description = Console.ReadLine() ?? string.Empty;

        Console.Write("Enter due date (yyyy-MM-dd): ");
        var dateInput = Console.ReadLine();
        if (!DateTime.TryParse(dateInput, out var dueDate))
        {
            Console.WriteLine("Invalid date format.");
            return;
        }

        Console.Write("Enter priority (Low/Medium/High): ");
        var priorityInput = Console.ReadLine();
        if (!Enum.TryParse<TaskPriority>(priorityInput, true, out var priority))
        {
            Console.WriteLine("Invalid priority.");
            return;
        }

        var task = _service.CreateTask(title, description, dueDate, priority);
        Console.WriteLine($"✓ Task created: {task}");
    }

    private void CompleteTask()
    {
        Console.Write("Enter task ID to complete: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        if (_service.CompleteTask(id))
            Console.WriteLine("✓ Task marked as complete.");
        else
            Console.WriteLine("Task not found.");
    }

    private void DeleteTask()
    {
        Console.Write("Enter task ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        if (_service.DeleteTask(id))
            Console.WriteLine("✓ Task deleted.");
        else
            Console.WriteLine("Task not found.");
    }

    private void ListByPriority()
    {
        Console.Write("Enter priority (Low/Medium/High): ");
        var input = Console.ReadLine();
        if (!Enum.TryParse<TaskPriority>(input, true, out var priority))
        {
            Console.WriteLine("Invalid priority.");
            return;
        }

        var tasks = _service.GetTasksByPriority(priority).ToList();
        Console.WriteLine($"\n--- {priority} Priority Tasks ---");

        if (!tasks.Any())
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (var task in tasks)
            Console.WriteLine(task);
    }

    private void ListPending()
    {
        var tasks = _service.GetPendingTasks().ToList();
        Console.WriteLine("\n--- Pending Tasks ---");

        if (!tasks.Any())
        {
            Console.WriteLine("No pending tasks.");
            return;
        }

        foreach (var task in tasks)
            Console.WriteLine(task);
    }
}