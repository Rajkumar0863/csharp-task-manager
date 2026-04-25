using TaskManager.Models;
using TaskManager.Repositories;
using TaskManager.Services;
using Xunit;

namespace TaskManager.Tests;

/// <summary>
/// Unit tests for TaskService business logic.
/// Uses an in-memory repository to isolate service behavior.
/// </summary>
public class TaskServiceTests
{
    private readonly string _testFilePath;
    private readonly ITaskRepository _repository;
    private readonly TaskService _service;

    public TaskServiceTests()
    {
        // Use a unique temp file per test run to avoid collisions
        _testFilePath = Path.Combine(Path.GetTempPath(), $"test_tasks_{Guid.NewGuid()}.json");
        _repository = new JsonTaskRepository(_testFilePath);
        _service = new TaskService(_repository);
    }

    [Fact]
    public void CreateTask_WithValidData_ReturnsTaskWithId()
    {
        // Arrange
        var title = "Write unit tests";
        var description = "Cover service layer";
        var dueDate = DateTime.UtcNow.AddDays(3);
        var priority = TaskPriority.High;

        // Act
        var result = _service.CreateTask(title, description, dueDate, priority);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal(title, result.Title);
        Assert.Equal(priority, result.Priority);
        Assert.False(result.IsComplete);

        Cleanup();
    }

    [Fact]
    public void CreateTask_WithEmptyTitle_ThrowsArgumentException()
    {
        // Arrange
        var dueDate = DateTime.UtcNow.AddDays(1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _service.CreateTask("", "desc", dueDate, TaskPriority.Low));

        Cleanup();
    }

    [Fact]
    public void CreateTask_WithPastDueDate_ThrowsArgumentException()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _service.CreateTask("Title", "desc", pastDate, TaskPriority.Low));

        Cleanup();
    }

    [Fact]
    public void CompleteTask_WithValidId_MarksTaskComplete()
    {
        // Arrange
        var task = _service.CreateTask("Test", "desc", 
            DateTime.UtcNow.AddDays(1), TaskPriority.Medium);

        // Act
        var result = _service.CompleteTask(task.Id);

        // Assert
        Assert.True(result);
        var updated = _service.GetTask(task.Id);
        Assert.NotNull(updated);
        Assert.True(updated!.IsComplete);

        Cleanup();
    }

    [Fact]
    public void CompleteTask_WithInvalidId_ReturnsFalse()
    {
        // Act
        var result = _service.CompleteTask(9999);

        // Assert
        Assert.False(result);

        Cleanup();
    }

    [Fact]
    public void GetPendingTasks_ReturnsOnlyIncompleteTasks()
    {
        // Arrange
        var t1 = _service.CreateTask("Task 1", "", DateTime.UtcNow.AddDays(1), TaskPriority.Low);
        var t2 = _service.CreateTask("Task 2", "", DateTime.UtcNow.AddDays(2), TaskPriority.High);
        _service.CompleteTask(t1.Id);

        // Act
        var pending = _service.GetPendingTasks().ToList();

        // Assert
        Assert.Single(pending);
        Assert.Equal(t2.Id, pending[0].Id);

        Cleanup();
    }

    [Fact]
    public void GetTasksByPriority_ReturnsOnlyMatchingPriority()
    {
        // Arrange
        _service.CreateTask("High task", "", DateTime.UtcNow.AddDays(1), TaskPriority.High);
        _service.CreateTask("Low task", "", DateTime.UtcNow.AddDays(1), TaskPriority.Low);
        _service.CreateTask("Another high", "", DateTime.UtcNow.AddDays(2), TaskPriority.High);

        // Act
        var highTasks = _service.GetTasksByPriority(TaskPriority.High).ToList();

        // Assert
        Assert.Equal(2, highTasks.Count);
        Assert.All(highTasks, t => Assert.Equal(TaskPriority.High, t.Priority));

        Cleanup();
    }

    [Fact]
    public void DeleteTask_WithValidId_RemovesTask()
    {
        // Arrange
        var task = _service.CreateTask("To delete", "", 
            DateTime.UtcNow.AddDays(1), TaskPriority.Low);

        // Act
        var result = _service.DeleteTask(task.Id);

        // Assert
        Assert.True(result);
        Assert.Null(_service.GetTask(task.Id));

        Cleanup();
    }

    private void Cleanup()
    {
        if (File.Exists(_testFilePath))
            File.Delete(_testFilePath);
    }
}