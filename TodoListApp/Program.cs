using TodoApp.Cli;
using TodoApp.Core;

internal class Program
{
    private static void Main(string[] args)
    {
        ITodoListStorage todoListStorage = new TodoListStorage();
        IConsoleWrapper consoleWrapper = new ConsoleWrapper();
        IUserInteraction userInteraction = new UserInteraction(consoleWrapper);
        var todoList = new TodoList(todoListStorage);
        var todo = new Todo(todoListStorage, userInteraction, todoList);
        todo.Run();
    }
}
