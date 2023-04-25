namespace GameBoard;

public class BoardException : Exception
{
    //default constructor yang memanggil base class constructor sehingga akan menginherit semua exception class
    public BoardException(string message) : base(message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
    }
}

