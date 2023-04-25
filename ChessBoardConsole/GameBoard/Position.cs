namespace GameBoard;

public class Position
{
    //untuk mendefinisikan posisi row sebagai line dengan bentuk a, b, c, d, e, f, g, h
    public int Line { get; set; }
    //untuk mendefinisikan posisi column dengan bentuk 1, 2, 3, 4, 5, 6, 7, 8
    public int Column { get; set; }
    //default constructor
    public Position()
    {
    }
    //overloaded constructor
    public Position(int line, int column)
    {
        Line = line;
        Column = column;
    }
    //method to define values
    public void DefineValues(int line, int column)
    {
        Line = line;
        Column = column;
    }
    //overriding ToString() method
    public override string ToString()
    {
        //untuk mengembalikan nilai dari Line dan Column tetapi dalam bentuk string Line, Column
        return Line + ", " + Column;
    }
}
