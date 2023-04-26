namespace GameBoard;

public class BoardException : Exception
{
    //default constructor yang memanggil base class Exception dan akan diimplementasikan di ................
    public BoardException(string message) : base(message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
}