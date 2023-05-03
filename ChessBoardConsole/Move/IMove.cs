using GameBoard;
namespace Move;

public interface IMove
{
    bool CanMoveToPosition(Position positionOfDestination);
}
