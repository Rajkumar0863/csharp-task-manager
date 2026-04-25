using TaskManager.Models;
using TaskManager.Repositories;
using Xunit;

namespace TaskManager.Tests;

/// <summary>
/// Unit tests for JsonTaskRepository data access operations.
/// Verifies persistence, retrieval, and CRUD correctness.
/// </summary>
public class JsonTaskRepositoryTests
{
    private string GetTempPath() =>
        Path.Combine(Path.GetTempPath(), $"repo_test_{Guid.NewGuid()}.json");

    [Fact]
    public void Add_NewTask_AssignsUniqueId()
    {
        // Arrange
        var path = GetTempPath();
        var repo = new JsonTaskRepository(path);
        var task = new TaskItem
        {
            Title = "Test",
            DueDate = DateTime.UtcNow.AddDays(1),
            Priority = TaskPriority.Medium
        };

        // Act
        var added = repo.Add(task);

        // Assert
        Assert.Equal(1, added.Id);
        File.Delete(path);
    }

    [Fact]
    public void Add_MultipleTasks_AssignsIncrementingIds()
    {
        // Arrange
        var path = GetTempPath();
        var repo = new JsonTaskRepository(path);

        // Act
        var t1 = repo.Add(new TaskItem { Title = "A", DueDate = DateTime.UtcNow.AddDays(1) });
        var t2 = repo.Add(new TaskItem { Title = "B", DueDate = DateTime.UtcNow.AddDays(1) });
        var t3 = repo.Add(new TaskItem { Title = "C", DueDate = DateTime.UtcNow.AddDays(1) });

        // Assert
        Assert.Equal(1, t1.Id);
        Assert.Equal(2, t2.Id);
        Assert.Equal(3, t3.Id);
        File.Delete(path);
    }

    [Fact]
    public void GetById_ExistingTask_ReturnsTask()
    {
        // Arrange
        var path = GetTempPath();
        var repo = new JsonTaskRepository(path);
        var added = repo.Add(new TaskItem { Title = "Find me", DueDate = DateTime.UtcNow.AddDays(1) });

        // Act
        var found = repo.GetById(added.Id);

        // Assert
        Assert.NotNull(found);
        Assert.Equal("Find me", found!.Title);
        File.Delete(path);
    }

    [Fact]
    public void GetById_NonExistingTask_ReturnsNull()
    {
        // Arrange
        var path = GetTempPath();
        var repo = new JsonTaskRepository(path);

        // Act
        var result = repo.GetById(999);

        // Assert
        Assert.Null(result);
        File.Delete(path);
    }

    [Fact]
    public void Delete_ExistingTask_ReturnsTrue()
    {
        // Arrange
        var path = GetTempPath();
        var repo = new JsonTaskRepository(path);
        var added = repo.Add(new TaskItem { Title = "Delete me", DueDate = DateTime.UtcNow.AddDays(1) });

        // Act
        var deleted = repo.Delete(added.Id);

        // Assert
        Assert.True(deleted);
        Assert.Null(repo.GetById(added.Id));
        File.Delete(path);
    }

    [Fact]
    public void Persistence_AfterSave_DataSurvivesReload()
    {
        // Arrange
        var path = GetTempPath();
        var repo1 = new JsonTaskRepository(path);
        repo1.Add(new TaskItem { Title = "Persistent", DueDate = DateTime.UtcNow.AddDays(1) });

        // Act — simulate app restart by creating a new repo instance
        var repo2 = new JsonTaskRepository(path);
        var tasks = repo2.GetAll().ToList();

        // Assert
        Assert.Single(tasks);
        Assert.Equal("Persistent", tasks[0].Title);
        File.Delete(path);
    }
}