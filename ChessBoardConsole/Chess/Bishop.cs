
using GameBoard;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(PieceColor color, Board board) : base(color, board) { }

        public override string ToString()
        {
            return "B";
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
            Position position = new(0, 0);

            // NW 
            position.DefineValues(Position.Line - 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                //set the position as a possible movement in maovementPossibilitiesMatrix
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                // Forced stop when the Bishop meets an adversary Piece
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line - 1, position.Column - 1);
            }
            // NE 
            position.DefineValues(Position.Line - 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                //set the position as a possible movement in maovementPossibilitiesMatrix
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line - 1, position.Column + 1);
            }
            // SE 
            position.DefineValues(Position.Line + 1, Position.Column + 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                //set the position as a possible movement in maovementPossibilitiesMatrix
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line + 1, position.Column + 1);
            }
            // SW 
            position.DefineValues(Position.Line + 1, Position.Column - 1);
            while (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                //set the position as a possible movement in maovementPossibilitiesMatrix
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
                if (Board.Piece(position) != null && Board.Piece(position).Color != Color)
                {
                    break;
                }
                position.DefineValues(position.Line + 1, position.Column - 1);
            }

            return movementPossibilitiesMatrix;
        }
    }
}
