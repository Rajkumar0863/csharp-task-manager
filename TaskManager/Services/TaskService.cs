using TaskManager.Models;
using TaskManager.Repositories;

namespace TaskManager.Services;

/// <summary>
/// Service layer containing business logic for task management.
/// Coordinates between UI and repository while enforcing business rules.
/// </summary>
public class TaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IEnumerable<TaskItem> GetAllTasks()
    {
        return _repository.GetAll();
    }

    public IEnumerable<TaskItem> GetPendingTasks()
    {
        return _repository.GetAll().Where(t => !t.IsComplete);
    }

    public IEnumerable<TaskItem> GetCompletedTasks()
    {
        return _repository.GetAll().Where(t => t.IsComplete);
    }

    public IEnumerable<TaskItem> GetTasksByPriority(TaskPriority priority)
    {
        return _repository.GetAll().Where(t => t.Priority == priority);
    }

    public TaskItem CreateTask(string title, string description,
                               DateTime dueDate, TaskPriority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Task title cannot be empty.", nameof(title));

        if (dueDate < DateTime.UtcNow.Date)
            throw new ArgumentException("Due date cannot be in the past.", nameof(dueDate));

        var task = new TaskItem
        {
            Title = title,
            Description = description ?? string.Empty,
            DueDate = dueDate,
            Priority = priority,
            IsComplete = false
        };

        return _repository.Add(task);
    }

    public bool CompleteTask(int id)
    {
        var task = _repository.GetById(id);
        if (task == null) return false;

        task.MarkComplete();
        return _repository.Update(task);
    }

    public bool DeleteTask(int id)
    {
        return _repository.Delete(id);
    }

    public TaskItem? GetTask(int id)
    {
        return _repository.GetById(id);
    }
}