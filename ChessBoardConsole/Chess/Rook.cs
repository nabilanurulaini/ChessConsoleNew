using GameBoard;

namespace Chess
{
    class Rook : Piece
    {
        public Rook(PieceColor color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "R";
        }

        private bool CanMoveToPosition(Position positionOfDestination)
        {
            Piece potentialPieceAtDestination = Board.Piece(positionOfDestination);
            return potentialPieceAtDestination == null || potentialPieceAtDestination.Color != Color;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementPossibilitiesMatrix = new bool[Board.Lines, Board.Columns];
            // Instantiate a new position with placeholder values
            Position position = new Position(0, 0);

            // North of the piece
            position.DefineValues(Position.Line - 1, Position.Column);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // Forced stop when the Rook meets an adversary Piece
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.Line--;
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
