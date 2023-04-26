using GameBoard;

namespace Chess
{
    class King : Piece
    {
        private ChessMatch _chessMatch;

        public King(PieceColor color, Board board, ChessMatch chessMatch) : base(color, board)
        {
            _chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMoveToPosition(Position positionOfDestination)
        {
            Piece potentialPieceAtDestination = Board.Piece(positionOfDestination);
            return potentialPieceAtDestination == null || potentialPieceAtDestination.Color != Color;
        }

        private bool CanRookParticipateInCastling(Position positionOfDestination)
        {
            if (!Board.ValidPosition(positionOfDestination))
            {
                return false;
            }
            else
            {
                Piece piece = Board.Piece(positionOfDestination);
                return piece != null && piece is Rook && piece.Color == Color && piece.NumberOfMoves == 0;
            }
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementPossibilitiesMatrix = new bool[Board.Lines, Board.Columns];
            // Instantiate a new position with placeholder values
            Position position = new(0, 0);

            // North of the piece
            position.DefineValues(Position.Line - 1, Position.Column);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Northeast of the piece
            position.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // East of the piece
            position.DefineValues(Position.Line, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Southeast of the piece
            position.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // South of the piece
            position.DefineValues(Position.Line + 1, Position.Column);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Southwest of the piece
            position.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // West of the piece
            position.DefineValues(Position.Line, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // Northwest of the piece
            position.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.ValidPosition(position) && CanMoveToPosition(position))
            {
                movementPossibilitiesMatrix[position.Line, position.Column] = true;
            }
            // # Special Play - Castling
            if (NumberOfMoves == 0 && !_chessMatch.IsInCheck)
            {
                // Castling Short
                Position rookPosition1 = new(Position.Line, Position.Column + 3);
                if (CanRookParticipateInCastling(rookPosition1))
                {
                    Position position1 = new(Position.Line, Position.Column + 1);
                    Position position2 = new(Position.Line, Position.Column + 2);
                    if (Board.Piece(position1) == null && Board.Piece(position2) == null)
                    {
                        movementPossibilitiesMatrix[Position.Line, Position.Column + 2] = true;
                    }
                }
                // Castling Long
                Position rookPosition2 = new(Position.Line, Position.Column - 4);
                if (CanRookParticipateInCastling(rookPosition2))
                {
                    Position position1 = new(Position.Line, Position.Column - 1);
                    Position position2 = new(Position.Line, Position.Column - 2);
                    Position position3 = new(Position.Line, Position.Column - 3);
                    if (Board.Piece(position1) == null && Board.Piece(position2) == null && Board.Piece(position3) == null)
                    {
                        movementPossibilitiesMatrix[Position.Line, Position.Column - 2] = true;
                    }
                }
            }

            return movementPossibilitiesMatrix;
        }
    }
}
