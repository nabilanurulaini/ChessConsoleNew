using GameBoard;
using Chess;
using NLog;
using static Helper.ConsoleHelper;
namespace ChessConsole;
class Program
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    static void Main()
    {
        LogManager.LoadConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config"));
        logger.Info("Chess Application Started Now");
        try
        {
            ChessMatch chessMatch = new();
            while (!chessMatch.MatchIsFinished)
            {
                 
                try
                {
                    Clear();
                    GameController.PrintMatchOriginPlay(chessMatch);

                    Position origin = GameController.ReadChessPosition().ToPosition();
                    logger.Info("Origin Position: " + origin);
                    chessMatch.ValidateOriginPosition(origin);
                    

                    bool[,] possiblePositions = chessMatch.Board.Piece(origin).PossibleMovements();

                    Clear();
                    GameController.PrintMatchDestinationPlay(chessMatch, possiblePositions);

                    Position destination = GameController.ReadChessPosition().ToPosition();
                    logger.Info("Destination Position: " + destination);
                    chessMatch.ValidateDestinationPosition(origin, destination);

                    chessMatch.ExecutePlay(origin, destination);
                }
                catch (BoardException e)
                {
                    WriteLine(e.Message);
                    logger.Error(e.Message);
                    ReadLine();
                }
            }

            Clear();
            GameController.PrintMatchOriginPlay(chessMatch);
        }
        catch (BoardException e)
        {
            WriteLine(e.Message);
            ReadLine();
        }
        ReadKey();
        logger.Info("Chess Console Application Finished");
    }
}
