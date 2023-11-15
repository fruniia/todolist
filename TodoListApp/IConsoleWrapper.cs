namespace TodoApp.Cli;

public interface IConsoleWrapper
{
    void WriteLine(string s);
    string ReadLine();
    ConsoleKey ReadKey();
    void Clear();

}
