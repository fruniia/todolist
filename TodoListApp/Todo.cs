using Microsoft.VisualBasic.FileIO;
using TodoApp.Core;
namespace TodoApp.Cli;

public class Todo
{
    private readonly ITodoListStorage _todoListstorage;
    private readonly IUserInteraction _userInteraction;
    private TodoList _todoList;
    public Todo(
        ITodoListStorage todoListStorage,
        IUserInteraction userInteraction,
        TodoList todoList)
    {
        _todoListstorage = todoListStorage;
        _userInteraction = userInteraction;
        _todoList = todoList;
    }
    public void Run()
    {
        var exit = false;
        while (exit is false)
        {
            _userInteraction.DisplayPlainMessage(
                $"Choose one option below\r\n" +
                $"[1] Add new todo\r\n" +
                $"[2] View todos\r\n" +
                $"[3] Show completed todos\r\n" +
                $"[4] Show not completed todos\r\n" +
                $"[5] Change status\r\n" +
                $"[6] Delete a task\r\n" +
                $"[0] Quit");

            if (_todoList.TodoItems.Count > 0)
            {
                PrintTodoItems();
            }
            var menuChoice = _userInteraction.Readkey();
            switch (menuChoice)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    CreateTodo();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ViewTodo();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    ShowCompletedTodos();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    ShowNotCompletedTodos();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    MarkTodoAsCompleted();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    break;
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    exit = true;
                    break;
                default:
                    _userInteraction.Clear();
                    _userInteraction.DisplayMessage("Wrong input");
                    break;
            }
        }
    }

    private void DeleteTodo()
    {
        throw new NotImplementedException();
    }

    private void MarkTodoAsCompleted()
    {
        _userInteraction.DisplayMessage("Choose which todo you want to change status on");
        var index = _userInteraction.GetInputAndParseToInt() - 1;
        var todo = _todoList.TodoItems.ElementAt(index);
        if (todo != null)
            _todoList.ChangeStatus(todo);
        UpdateTodoItems();
        _userInteraction.Clear();
    }

    private void ShowNotCompletedTodos()
    {
        var todos = _todoList.GetNotCompletedTodos();
        if (todos is not null)
        {
            foreach (var todo in todos)
            {
                _userInteraction.DisplayMessage($"{todo.Title} {todo.Description} {(todo.IsComplete ? "done" : "not done")}");
            }
            ClearScreen();
        }
    }

    private void ShowCompletedTodos()
    {
        var todos = _todoList.GetCompletedTodos();
        if (todos is not null)
        {
            foreach (var todo in todos)
            {
                _userInteraction.DisplayMessage($"{todo.Title} {todo.Description} {(todo.IsComplete ? "done" : "not done")}");
            }
        }
        ClearScreen();
    }

    private void ClearScreen()
    {
        _userInteraction.DisplayPlainMessage("Press any key to go back");
        _userInteraction.Readkey();
        _userInteraction.Clear();
    }

    public void CreateTodo()
    {
        var title = _userInteraction.GetInput("Add title");
        var description = _userInteraction.GetInput("Add description");
        _todoList.Create(title, description);
        UpdateTodoItems();
        _userInteraction.Clear();
    }

    private void ViewTodo()
    {
        _userInteraction.DisplayMessage("Choose todo you want to view");
        var index = _userInteraction.GetInputAndParseToInt() - 1;
        var todo = _todoList.TodoItems.ElementAt(index);
        if (todo != null)
            TodoDetails(todo);
    }

    private void UpdateTodoItems() => _todoList.TodoItems = _todoListstorage.Load();

    public void PrintTodoItems()
    {
        var todoTitles = new List<string>();
        for (int i = 1; i <= _todoList.TodoItems.Count; i++)
        {
            todoTitles.Add($"[{i}] - {_todoList.TodoItems[i - 1].Title} - {(_todoList.TodoItems[i - 1].IsComplete ? "Done" : "Not done")}");
        }
        _userInteraction.DisplayMessage(todoTitles);
    }

    public void TodoDetails(TodoItem todo)
    {
        var texts = new List<string>()
        {
            $"Title: {todo.Title}",
            $"Description: {todo.Description ?? string.Empty}",
            $"Completed: {(todo.IsComplete ? "Done" : "Not done")}"
        };
        _userInteraction.DisplayMessage(texts);
        ClearScreen();
    }
}
