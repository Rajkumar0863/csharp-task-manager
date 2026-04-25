namespace TaskManager.Models;

/// <summary>
/// Represents a single task in the task management system.
/// Demonstrates encapsulation through properties and validation.
/// </summary>
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public bool IsComplete { get; set; }
    public DateTime CreatedAt { get; set; }

    public TaskItem()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public TaskItem(int id, string title, string description,
                    DateTime dueDate, TaskPriority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.", nameof(title));

        Id = id;
        Title = title;
        Description = description ?? string.Empty;
        DueDate = dueDate;
        Priority = priority;
        IsComplete = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkComplete()
    {
        IsComplete = true;
    }

    public override string ToString()
    {
        var status = IsComplete ? "✓" : " ";
        return $"[{status}] #{Id} [{Priority}] {Title} (Due: {DueDate:yyyy-MM-dd})";
    }
}