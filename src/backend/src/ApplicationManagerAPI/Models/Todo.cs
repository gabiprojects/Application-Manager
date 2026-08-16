namespace ApplicationManagerAPI.Models;

public class Todo
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public DateOnly? DueBy { get; set; }
    public bool IsComplete { get; set; }

    public Todo() { }

    public Todo(int id, string? title, DateOnly? dueBy = null, bool isComplete = false)
    {
        Id = id;
        Title = title;
        DueBy = dueBy;
        IsComplete = isComplete;
    }
}