namespace TodoApp.Cli;
public interface IUserInteraction
{
    void DisplayMessage(string message);
    void DisplayMessage(List<string> messages);
    void DisplayPlainMessage(string message);
    int GetInputAndParseToInt();
    string GetInput(string prompt);
    void Clear();
    ConsoleKey Readkey();

}
