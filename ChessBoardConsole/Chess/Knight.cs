using GameBoard;

namespace Chess
{
    class Knight : Piece
    {
        public Knight(PieceColor color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "N";
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

            // NW of the piece. Movement one
            position.DefineValues(Position.Line - 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // NW of the piece. Movement two
            position.DefineValues(Position.Line - 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // NE of the piece. Movement one
            position.DefineValues(Position.Line - 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // NE of the piece. Movement two
            position.DefineValues(Position.Line - 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // SE of the piece. Movement one
            position.DefineValues(Position.Line + 1, Position.Column + 2);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // SE of the piece. Movement two
            position.DefineValues(Position.Line + 2, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // SW of the piece. Movement one
            position.DefineValues(Position.Line + 1, Position.Column - 2);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // SW of the piece. Movement two
            position.DefineValues(Position.Line + 2, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }

            return movementPossibilitiesMatrix;
        }
    }
}
