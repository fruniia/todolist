namespace TodoApp.Core;

public class TodoListStorage : ITodoListStorage
{
    public List<TodoItem> Todos { get; set; }
    public List<TodoItem> Load() => Todos;

    public void Save(TodoItem todo)
    {
        Todos ??= new();
        Todos.Add(todo);
    }
}
