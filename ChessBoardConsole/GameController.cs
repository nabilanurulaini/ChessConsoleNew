using Chess;
using GameBoard;
using static Helper.ConsoleHelper;
namespace ChessConsole;
class GameController
{
    public static void Header()
    {
        Clear();
        Clear();
        ChangeBackgroundColor(ConsoleColor.DarkBlue);
        WriteLine("+-----------------------------------------+");
        WriteLine("|                                         |");
        WriteLine("|         Chess Board Game Console        |");
        WriteLine("|                                         |");
        WriteLine("+-----------------------------------------+");
        WriteLine("");
        ResetColor();
    }
    public static void PrintMatchOriginPlay(ChessMatch chessMatch)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;
        Clear();

        Header();

        PrintBoard(chessMatch.Board);

        WriteLine("");
        PrintCapturedPieces(chessMatch);

        WriteLine("");
        WriteLine($"Turn {chessMatch.Turn}");

        if (!chessMatch.MatchIsFinished)
        {
            Write("Current Player: ");
            PrintInPlayerColor(chessMatch, defaultColor);
            WriteLine(chessMatch.CurrentPlayer);
            ReturnToDefaultColor(defaultColor);
        }
        else
        {
            WriteLine("Check Mate!!!");
            WriteLine("Winner: " + chessMatch.CurrentPlayer);
            WriteLine("Congratulations!!!");
        }
        WriteLine("");
        PrintInPlayerColor(chessMatch, defaultColor);
        Write("Origin: ");
        ReturnToDefaultColor(defaultColor);
    }
    public static void PrintMatchDestinationPlay(ChessMatch chessMatch, bool[,] possiblePositions)
    {
        Header();

        ConsoleColor defaultColor = Console.ForegroundColor;
        PrintBoard(chessMatch.Board, possiblePositions);

        WriteLine("");
        PrintCapturedPieces(chessMatch);


        WriteLine("");
        WriteLine("Turn " + chessMatch.Turn);

        Write("Current Player: ");
        PrintInPlayerColor(chessMatch, defaultColor);
        //ReturnToDefaultColor(defaultColor);
        // PrintInPlayerColor(chessMatch, defaultColor);
        WriteLine(chessMatch.CurrentPlayer);
        ReturnToDefaultColor(defaultColor);

        WriteLine("");
        PrintInPlayerColor(chessMatch, defaultColor);
        Write("Destination: ");
        ReturnToDefaultColor(defaultColor);
    }
    private static void PrintInPlayerColor(ChessMatch chessMatch, ConsoleColor defaultColor)
    {
        if (chessMatch.CurrentPlayer == PieceColor.Black)
        {
            ChangeForegroundColor(ConsoleColor.DarkYellow);
        }
        else
        {
            ChangeForegroundColor(defaultColor);
        }
    }

    private static void ReturnToDefaultColor(ConsoleColor defaultColor)
    {
        ChangeForegroundColor(defaultColor);
    }

    private static void PrintCapturedPieces(ChessMatch chessMatch)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Write("White ");
        PrintListOfPieces(chessMatch.CapturedPieces(PieceColor.White));

        ChangeForegroundColor(ConsoleColor.DarkYellow);
        Write(" Black ");
        PrintListOfPieces(chessMatch.CapturedPieces(PieceColor.Black));
        ChangeForegroundColor(defaultColor);
        WriteLine("");
    }

    private static void PrintListOfPieces(List<Piece> capturedPieces)
    {
        Write("[");
        foreach (Piece piece in capturedPieces)
        {
            //if (piece.Color == Color.Black) 
            Write(piece + " ");
        }
        Write("]");
    }

    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Lines; i++)
        {
            Write(" ");
            PrintEdge(board);
            Write(8 - i);
            for (int j = 0; j < board.Columns; j++)
            {
                Write("| ");
                PrintPiece(board.Piece(i, j));
            }
            Write("|");
            WriteLine("");
        }
        Write(" ");
        PrintEdge(board);
        WriteLine("   a    b    c    d    e    f    g    h");
    }
    public static void PrintEdge(Board board)
    {
        for (int x = 0; x < board.Lines; x++)
        {
            ChangeBackgroundColor(ConsoleColor.Black);
            Write("+----");
        }
        WriteLine("+");
    }
    //board kedua untuk possible positions
    public static void PrintBoard(Board board, bool[,] possiblePositions)
    {
        ConsoleColor originalBackgroundColor = Console.BackgroundColor;
        ConsoleColor activePositionBackgroundColor = ConsoleColor.DarkGray;

        for (int i = 0; i < board.Lines; i++)
        {
            Write(" ");
            PrintEdge(board);
            Write(8 - i);
            for (int j = 0; j < board.Columns; j++)
            {
                Write("| ");
                if (possiblePositions[i, j])
                {
                    ChangeBackgroundColor(activePositionBackgroundColor);
                }
                else
                {
                    ChangeBackgroundColor(originalBackgroundColor);
                }
                PrintPiece(board.Piece(i, j));

                ChangeBackgroundColor(originalBackgroundColor);
            }
            Write("|");
            WriteLine("");
        }
        Write(" ");
        PrintEdge(board);
        WriteLine("   a    b    c    d    e    f    g    h");
        ChangeBackgroundColor(originalBackgroundColor);
    }

    public static ChessPosition ReadChessPosition()
    {
        string playerInput = Console.ReadLine();
        char columnInput = playerInput[0];
        int lineInput = int.Parse(playerInput[1].ToString());
        return new ChessPosition(columnInput, lineInput);
    }

    public static void PrintPiece(Piece piece)
    {
        if (piece == null)
        {
            Write("-  ");
        }
        else
        {
            if (piece.Color == PieceColor.White)
            {
                Write(piece);
            }
            else
            {
                ConsoleColor defaultColor = Console.ForegroundColor;
                ChangeForegroundColor(ConsoleColor.Yellow);
                Write(piece);
                ChangeForegroundColor(defaultColor);
            }
            Write("  ");
        }
    }
}