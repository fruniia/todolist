namespace TodoApp.Core;

public class TodoList
{
    private readonly ITodoListStorage _todoListStorage;
    public List<TodoItem> TodoItems { get; set; } = new();
    public TodoList(ITodoListStorage todoListStorage)
    {
        _todoListStorage = todoListStorage;
    }

    public TodoItem Create(string title, string? description)
    {
        var todoItem = new TodoItem
        {
            Title = title,
            Description = description
        };

        _todoListStorage.Save(todoItem);
        return todoItem;
    }

    public void ChangeStatus(TodoItem todo)
    {
        todo.IsComplete = !todo.IsComplete;
    }

    public bool Delete(TodoItem todoItem)
    {
        if (TodoItems is null)
            return false;
        return TodoItems.Remove(todoItem);
    }

    public List<TodoItem> GetAllTodos() => TodoItems = _todoListStorage.Load();

    public List<TodoItem> GetCompletedTodos() => TodoItems = _todoListStorage.Load().Where(x => x.IsComplete).ToList();

    public List<TodoItem> GetNotCompletedTodos() => TodoItems = _todoListStorage.Load().Where(x => x.IsComplete is false).ToList();

}
