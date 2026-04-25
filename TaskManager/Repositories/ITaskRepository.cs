using TaskManager.Models;

namespace TaskManager.Repositories;

/// <summary>
/// Repository interface for task data access operations.
/// Implements the Repository Design Pattern to separate 
/// data access from business logic.
/// </summary>
public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    TaskItem Add(TaskItem task);
    bool Update(TaskItem task);
    bool Delete(int id);
    void SaveChanges();
}