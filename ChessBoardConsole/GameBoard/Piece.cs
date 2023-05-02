namespace GameBoard;

abstract class Piece
{
    public Position Position { get; set; }
    public PieceColor Color { get; protected set; }
    public int NumberOfMoves { get; protected set; }
    public Board Board { get; protected set; }
    // default constructor
    public Piece()
    {
    }
    public Piece(PieceColor color, Board board)
    {
        Position = null;
        Color = color;
        NumberOfMoves = 0;
        Board = board;
    }
    // method to increment movement quantity
    public void IncreaseMovementQuantity()
    {
        //jika NumberOfMoves lebih besar dari 0 maka NumberOfMoves akan ditambah 1
        NumberOfMoves++;
    }
    // method to decrement movement quantity
    public void DecreaseMovementQuantity()
    {
        //jika NumberOfMoves lebih besar dari 0 maka NumberOfMoves akan dikurangi 1
        NumberOfMoves--;
    }
    // method to check if there is a possible movement by checking the matrix of possible movements
    public bool CanPieceMove()
    {
        // call the PossibleMovements() method to check the possible movements dengan bentuk matrix bool 2D
        bool[,] possibleMovementsMatrix = PossibleMovements();
        //checking each Lines/row and columns
        for (int i = 0; i < Board.Lines; i++)
        {
            for (int j = 0; j < Board.Columns; j++)
            {
                // check if the value of current position is true
                if (possibleMovementsMatrix[i, j])
                {
                    return true;
                }
            }
        }
        // if there is no possible movement, return false
        return false;
    }
    // method to check if the next move is legal so the piece bisa move atau tidak if true then the piece will move
    public bool CanMoveToDestination(Position position)
    {
        return PossibleMovements()[position.Line, position.Column];
    }
    // abstract method to check the possible movements and will be override in each piece class
    public abstract bool[,] PossibleMovements();
}
