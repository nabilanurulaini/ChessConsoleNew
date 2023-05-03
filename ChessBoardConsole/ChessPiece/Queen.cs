using PieceColors;
using GameBoard;
using Move;

namespace Chess
{
    class Queen : Piece, IMove
    {
        public Queen(PieceColor color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "Q";
        }

        public bool CanMoveToPosition(Position positionOfDestination)
        {
            Piece potentialPieceAtDestination = Board.Piece(positionOfDestination);
            return potentialPieceAtDestination == null || potentialPieceAtDestination.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementPossibilitiesMatrix = new bool[Board.Lines, Board.Columns];
            // Instantiate a new position with placeholder values
            Position position = new(0, 0);

            // NW of the piece
            position.DefineValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // Forced stop when the Bishop meets an adversary Piece
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line - 1, position.Column - 1);
            }
            // North of the piece
            position.DefineValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // Forced stop when the Tower meets an adversary Piece
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line--;
            }
            // NE of the piece
            position.DefineValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line - 1, position.Column + 1);
            }
            // East of the piece
            position.DefineValues(Position.Line, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column++;
            }
            // SE of the piece
            position.DefineValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line + 1, position.Column + 1);
            }
            // South of the piece
            position.DefineValues(Position.Line + 1, Position.Column);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line++;
            }
            // SW of the piece
            position.DefineValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line + 1, position.Column - 1);
            }
            // West of the piece
            position.DefineValues(Position.Line, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Column--;
            }

            return movementPossibilitiesMatrix;
        }
    }
}
