namespace TodoApp.Core;

public interface ITodoListStorage
{
    public void Save(TodoItem todoitem);
    public List<TodoItem> Load();
}
