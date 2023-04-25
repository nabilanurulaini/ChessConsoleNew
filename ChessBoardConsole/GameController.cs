using System;
using System.Collections.Generic;
using System.Text;
using Chess;
using GameBoard;
namespace ChessConsole;
class GameController
{
    public static void PrintMatchOriginPlay(ChessMatch chessMatch)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("|                                        |");
        Console.WriteLine("|        Chess Board Game Console        |");
        Console.WriteLine("|                                        |");
        Console.WriteLine("+----------------------------------------+");

        Console.WriteLine();
        PrintBoard(chessMatch.Board);

        Console.WriteLine();
        PrintCapturedPieces(chessMatch);

        Console.WriteLine();
        Console.WriteLine($"Turn: {chessMatch.Turn}");

        if (!chessMatch.MatchIsFinished)
        {
            Console.Write("Current Player: ");
            PrintInPlayerColor(chessMatch, defaultColor);
            Console.WriteLine(chessMatch.CurrentPlayer);
            ReturnToDefaultColor(defaultColor);
        }
        else
        {
            Console.WriteLine("Check Mate!!!");
            Console.WriteLine("Winner: " + chessMatch.CurrentPlayer);
            Console.WriteLine("Congratulations!");
        }
        Console.WriteLine();
        PrintInPlayerColor(chessMatch, defaultColor);
        Console.Write("Origin: ");
        ReturnToDefaultColor(defaultColor);
    }
    public static void PrintMatchDestinationPlay(ChessMatch chessMatch, bool[,] possiblePositions)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;
        Console.Clear();
        Console.WriteLine("+----------------------------------------+");
        Console.WriteLine("|                                        |");
        Console.WriteLine("|        Chess Board Game Console        |");
        Console.WriteLine("|                                        |");
        Console.WriteLine("+----------------------------------------+");

        Console.WriteLine();
        PrintBoard(chessMatch.Board, possiblePositions);

        Console.WriteLine();
        PrintCapturedPieces(chessMatch);


        Console.WriteLine();
        Console.WriteLine("Turn 1" + chessMatch.Turn);

        Console.Write("Current Player: ");
        PrintInPlayerColor(chessMatch, defaultColor);
        Console.WriteLine(chessMatch.CurrentPlayer);
        ReturnToDefaultColor(defaultColor);

        Console.WriteLine();
        PrintInPlayerColor(chessMatch, defaultColor);
        Console.Write("Destination: ");
        ReturnToDefaultColor(defaultColor);
    }
    private static void PrintInPlayerColor(ChessMatch chessMatch, ConsoleColor defaultColor)
    {
        if (chessMatch.CurrentPlayer == PieceColor.Black)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else
        {
            Console.ForegroundColor = defaultColor;
        }
    }

    private static void ReturnToDefaultColor(ConsoleColor defaultColor)
    {
        Console.ForegroundColor = defaultColor;
    }

    private static void PrintCapturedPieces(ChessMatch chessMatch)
    {
        ConsoleColor defaultColor = Console.ForegroundColor;

        Console.Write("White: ");
        PrintListOfPieces(chessMatch.CapturedPieces(PieceColor.White));

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(" Black: ");
        PrintListOfPieces(chessMatch.CapturedPieces(PieceColor.Black));
        Console.ForegroundColor = defaultColor;
        Console.WriteLine();
    }

    private static void PrintListOfPieces(List<Piece> capturedPieces)
    {
        Console.Write("[");
        foreach (Piece piece in capturedPieces)
        {
            //if (piece.Color == Color.Black) 
            Console.Write(piece + " ");
        }
        Console.Write("]");
    }

    public static void PrintBoard(Board board)
    {
        for (int i = 0; i < board.Lines; i++)
        {
            Console.Write(" ");
            PrintEdge(board);
            Console.Write(8 - i);
            for (int j = 0; j < board.Columns; j++)
            {
                Console.Write("| ");
                PrintPiece(board.Piece(i, j));
            }
            Console.Write("|");
            Console.WriteLine();
        }
        Console.Write(" ");
        PrintEdge(board);
        Console.WriteLine("   a    b    c    d    e    f    g    h");
    }
    public static void PrintEdge(Board board)
    {
        for (int x = 0; x < board.Lines; x++)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write("+----");
        }
        Console.WriteLine("+");
    }
    //board kedua untuk possible positions
    public static void PrintBoard(Board board, bool[,] possiblePositions)
    {
        ConsoleColor originalBackgroundColor = Console.BackgroundColor;
        ConsoleColor alternativeBaclgroundColor = ConsoleColor.DarkGray;

        for (int i = 0; i < board.Lines; i++)
        {
            Console.Write(" ");
            PrintEdge(board);
            Console.Write(8 - i);
            for (int j = 0; j < board.Columns; j++)
            {
                Console.Write("| ");
                if (possiblePositions[i, j])
                {
                    Console.BackgroundColor = alternativeBaclgroundColor;
                }
                else
                {
                    Console.BackgroundColor = originalBackgroundColor;
                }
                PrintPiece(board.Piece(i, j));

                Console.BackgroundColor = originalBackgroundColor;
            }
            Console.Write("|");
            Console.WriteLine();
        }
        Console.Write(" ");
        PrintEdge(board);
        Console.WriteLine("   a    b    c    d    e    f    g    h");
        Console.BackgroundColor = originalBackgroundColor;
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
            Console.Write("-  ");
        }
        else
        {
            if (piece.Color == PieceColor.White)
            {
                Console.Write(piece);
            }
            else
            {
                ConsoleColor defaultColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = defaultColor;
            }
            Console.Write("  ");
        }
    }
}