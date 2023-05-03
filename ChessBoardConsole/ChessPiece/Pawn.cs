using PieceColors;
using GameBoard;

namespace Chess
{
    class Pawn : Piece
    {
        private ChessMatch _chessMatch;

        public Pawn(PieceColor color, Board board, ChessMatch chessMatch) : base(color, board)
        {
            _chessMatch = chessMatch;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool IsEnemyAtPosition(Position positionOfDestination)
        {
            Piece piece = Board.Piece(positionOfDestination);
            return piece != null && piece.Color != Color;
        }

        private bool IsPositionFree(Position positionAtDestination)
        {
            return Board.Piece(positionAtDestination) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] movementPossibilitiesMatrix = new bool[Board.Lines, Board.Columns];
            // Instantiate a new position with placeholder values
            Position position = new(0, 0);

            if (Color == PieceColor.White)
            {
                // North of the piece. Standard movement of a Pawn
                position.DefineValues(Position.Line - 1, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position))
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // North of the piece. Initial movement for a Pawn (moves two places instead of one)
                position.DefineValues(Position.Line - 2, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position) && IsPositionFree(new Position(Position.Line - 1, Position.Column)) && NumberOfMoves == 0)
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // NW of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line - 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // NE of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line - 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // Special Play - En Passant
                // Move the piece to the NW direction and capture the piece at its left or move the piece to the NE direction and capture the piece at its right
                if (Position.Line == 3)
                {
                    Position positionToTheLeft = new(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(positionToTheLeft) && IsEnemyAtPosition(positionToTheLeft) && Board.Piece(positionToTheLeft) == _chessMatch.VulnerableToEnPassant)
                    {
                        movementPossibilitiesMatrix[positionToTheLeft.Line - 1, positionToTheLeft.Column] = true;
                    }
                    Position positionToTheRight = new(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(positionToTheRight) && IsEnemyAtPosition(positionToTheRight) && Board.Piece(positionToTheRight) == _chessMatch.VulnerableToEnPassant)
                        movementPossibilitiesMatrix[positionToTheRight.Line - 1, positionToTheRight.Column] = true;
                }
            }
            else
            {
                // South of the piece. Standard movement of a Pawn
                position.DefineValues(Position.Line + 1, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position))
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // South of the piece. Initial movement for a Pawn (moves two places instead of one)
                position.DefineValues(Position.Line + 2, Position.Column);
                if (Board.ValidPosition(position) && IsPositionFree(position) && IsPositionFree(new Position(Position.Line + 1, Position.Column)) && NumberOfMoves == 0)
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // SW of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // SE of the piece. Movement for capturing other pieces
                position.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.ValidPosition(position) && IsEnemyAtPosition(position))
                {
                    movementPossibilitiesMatrix[position.Line, position.Column] = true;
                }
                // Special Play - En Passant
                // Move the piece to the SW direction and capture the piece at its left or move the piece to the SE direction and capture the piece at its right
                if (Position.Line == 4)
                {
                    Position positionToTheLeft = new(Position.Line, Position.Column - 1);
                    if (Board.ValidPosition(positionToTheLeft) && IsEnemyAtPosition(positionToTheLeft) && Board.Piece(positionToTheLeft) == _chessMatch.VulnerableToEnPassant)
                    {
                        movementPossibilitiesMatrix[positionToTheLeft.Line + 1, positionToTheLeft.Column] = true;
                    }
                    Position positionToTheRight = new(Position.Line, Position.Column + 1);
                    if (Board.ValidPosition(positionToTheRight) && IsEnemyAtPosition(positionToTheRight) && Board.Piece(positionToTheRight) == _chessMatch.VulnerableToEnPassant)
                    {
                        movementPossibilitiesMatrix[positionToTheRight.Line + 1, positionToTheRight.Column] = true;
                    }
                }
            }

            return movementPossibilitiesMatrix;
        }
    }
}
