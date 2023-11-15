namespace TodoApp.Cli;

public class UserInteraction : IUserInteraction
{
    private readonly IConsoleWrapper _consoleWrapper;
    private const int Padleft = 40;
    private const int Padright = 80;
    private readonly string _rowDeliminator = "".PadRight(Padright, '-');
    public UserInteraction(IConsoleWrapper consoleWrapper)
    {
        _consoleWrapper = consoleWrapper;
    }
    public void DisplayPlainMessage(string message)
    {
        _consoleWrapper.WriteLine(message);
    }
    public void DisplayMessage(string message)
    {
        _consoleWrapper.WriteLine(
            $"{_rowDeliminator}\r\n" +
            $"{message.PadLeft(Padleft + message.Length / 2).PadRight(Padright)}\r\n" +
            $"{_rowDeliminator}");
    }
    public void DisplayMessage(List<string> messages)
    {
        var output = _rowDeliminator;
        foreach (var text in messages)
        {
            output += $"\r\n{text.PadLeft(Padleft + text.Length / 2).PadRight(Padright)}\r\n";
        }
        output += _rowDeliminator;

        _consoleWrapper.WriteLine(output);
    }
    public string GetInput(string prompt)
    {
        DisplayMessage(prompt);
        return _consoleWrapper.ReadLine() ?? string.Empty;
    }
    public int GetInputAndParseToInt()
    {
        int number;
        while (int.TryParse(_consoleWrapper.ReadLine(), out number) is false)
        {
           _consoleWrapper.WriteLine("You must enter digits. Please try again");
        }
        return number;
    }
    public void Clear() => _consoleWrapper.Clear();
    public ConsoleKey Readkey() => _consoleWrapper.ReadKey();
}
