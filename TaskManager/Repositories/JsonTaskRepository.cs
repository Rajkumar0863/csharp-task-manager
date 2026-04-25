using System.Text.Json;
using TaskManager.Models;

namespace TaskManager.Repositories;

/// <summary>
/// JSON file-based implementation of ITaskRepository.
/// Persists tasks to a local JSON file.
/// </summary>
public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;
    private List<TaskItem> _tasks;
    private int _nextId;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        _tasks = LoadFromFile();
        _nextId = _tasks.Count > 0 ? _tasks.Max(t => t.Id) + 1 : 1;
    }

    public IEnumerable<TaskItem> GetAll()
    {
        return _tasks.ToList();
    }

    public TaskItem? GetById(int id)
    {
        return _tasks.FirstOrDefault(t => t.Id == id);
    }

    public TaskItem Add(TaskItem task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        task.Id = _nextId++;
        _tasks.Add(task);
        SaveChanges();
        return task;
    }

    public bool Update(TaskItem task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        var existing = GetById(task.Id);
        if (existing == null) return false;

        existing.Title = task.Title;
        existing.Description = task.Description;
        existing.DueDate = task.DueDate;
        existing.Priority = task.Priority;
        existing.IsComplete = task.IsComplete;
        SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var task = GetById(id);
        if (task == null) return false;

        _tasks.Remove(task);
        SaveChanges();
        return true;
    }

    public void SaveChanges()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(_tasks, options);
        File.WriteAllText(_filePath, json);
    }

    private List<TaskItem> LoadFromFile()
    {
        if (!File.Exists(_filePath))
            return new List<TaskItem>();

        try
        {
            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<TaskItem>();

            return JsonSerializer.Deserialize<List<TaskItem>>(json)
                   ?? new List<TaskItem>();
        }
        catch (JsonException)
        {
            return new List<TaskItem>();
        }
    }
}