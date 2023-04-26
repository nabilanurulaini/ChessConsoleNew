using GameBoard;
using Chess;
namespace ChessConsole;
class Program
{
    static void Main()
    {
        try
        {
            ChessMatch chessMatch = new ChessMatch();
            while (!chessMatch.MatchIsFinished)
            {
                try
                {
                    Console.Clear();
                    GameController.PrintMatchOriginPlay(chessMatch);

                    Position origin = GameController.ReadChessPosition().ToPosition();
                    chessMatch.ValidateOriginPosition(origin);

                    bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMovements();

                    Console.Clear();
                    GameController.PrintMatchDestinationPlay(chessMatch, possiblePositions);

                    Position destination = GameController.ReadChessPosition().ToPosition();
                    chessMatch.ValidateDestinationPosition(origin, destination);

                    chessMatch.ExecutePlay(origin, destination);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }

            Console.Clear();
            GameController.PrintMatchOriginPlay(chessMatch);
        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
            Console.ReadLine();
        }
        Console.ReadKey();
    }
}
