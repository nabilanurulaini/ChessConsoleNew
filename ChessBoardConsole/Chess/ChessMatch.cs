using GameBoard;
using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public bool MatchIsFinished { get; private set; }
        public bool IsInCheck { get; private set; }
        public int Turn { get; private set; }
        public PieceColor CurrentPlayer { get; private set; }
        public Piece VulnerableToEnPassant { get; private set; }
        private List<Piece> _piecesInGame;
        private List<Piece> _piecesCaptured;

        public ChessMatch()
        {
            Board = new Board(8, 8);
            MatchIsFinished = false;
            IsInCheck = false;
            Turn = 1;
            CurrentPlayer = PieceColor.White;
            _piecesInGame = new List<Piece>();
            _piecesCaptured = new List<Piece>();
            VulnerableToEnPassant = null;
            PlacePiecesOnBoard();
        }

        private Piece ExecuteMovement(Position origin, Position destination)
        {
            Piece pieceToMove = Board.RemovePieceFromBoard(origin);
            pieceToMove.IncreaseMovementQuantity();
            Piece pieceToCapture = Board.RemovePieceFromBoard(destination);
            if (pieceToCapture != null)
            {
                _piecesCaptured.Add(pieceToCapture);
            }
            Board.AddPieceToBoard(pieceToMove, destination);
            // Special Play - Castling Short
            if (pieceToMove is King && destination.Column == origin.Column + 2)
            {
                ExecuteCastlingShort(origin, pieceToMove);
            }
            // Special Play - Castling Long
            if (pieceToMove is King && destination.Column == origin.Column - 2)
            {
                ExecuteCastlingLong(origin, pieceToMove);
            }
            // Special Play - En Passant
            if (pieceToCapture == null && pieceToMove is Pawn && origin.Column != destination.Column)
            {
                pieceToCapture = ExecuteEnPassant(destination, pieceToMove, pieceToCapture);
            }
            return pieceToCapture;
        }

        private void ExecuteCastlingShort(Position origin, Piece pieceToMove)
        {
            Position rookOrigin = new Position(origin.Line, origin.Column + 3);
            Position rookDestination = new Position(origin.Line, origin.Column + 1);
            Piece rook = Board.RemovePieceFromBoard(rookOrigin);
            rook.IncreaseMovementQuantity();
            Board.AddPieceToBoard(rook, rookDestination);
        }

        private void ExecuteCastlingLong(Position origin, Piece pieceToMove)
        {
            Position rookOrigin = new Position(origin.Line, origin.Column - 4);
            Position rookDestination = new Position(origin.Line, origin.Column - 1);
            Piece rook = Board.RemovePieceFromBoard(rookOrigin);
            rook.IncreaseMovementQuantity();
            Board.AddPieceToBoard(rook, rookDestination);
        }

        private Piece ExecuteEnPassant(Position destination, Piece pieceToMove, Piece pieceToCapture)
        {
            Position enPassantPosition;
            if (pieceToMove.Color == PieceColor.White)
            {
                enPassantPosition = new Position(destination.Line + 1, destination.Column);
            }
            else
            {
                enPassantPosition = new Position(destination.Line - 1, destination.Column);
            }
            pieceToCapture = Board.RemovePieceFromBoard(enPassantPosition);
            _piecesCaptured.Add(pieceToCapture);
            return pieceToCapture;
        }

        private void UndoMovement(Position origin, Position destination, Piece capturedPiece)
        {
            Piece pieceToGoBack = Board.RemovePieceFromBoard(destination);
            pieceToGoBack.DecreaseMovementQuantity();
            if (capturedPiece != null)
            {
                Board.AddPieceToBoard(capturedPiece, destination);
                _piecesCaptured.Remove(capturedPiece);
            }
            Board.AddPieceToBoard(pieceToGoBack, origin);
            // Special Play - Castling Short
            UndoCastlingShort(pieceToGoBack, origin, destination);
            // Special Play - Castling Long
            UndoCastlingLong(pieceToGoBack, origin, destination);
            // Special Play - En Passant
            UndoEnPassant(pieceToGoBack, capturedPiece, origin, destination);
        }

        private void UndoCastlingShort(Piece pieceToGoBack, Position origin, Position destination)
        {
            if (pieceToGoBack is King && destination.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestination = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePieceFromBoard(rookDestination);
                rook.DecreaseMovementQuantity();
                Board.AddPieceToBoard(rook, rookOrigin);
            }
        }

        private void UndoCastlingLong(Piece pieceToGoBack, Position origin, Position destination)
        {
            if (pieceToGoBack is King && destination.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestination = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePieceFromBoard(rookDestination);
                rook.DecreaseMovementQuantity();
                Board.AddPieceToBoard(rook, rookOrigin);
            }
        }

        private void UndoEnPassant(Piece pieceToGoBack, Piece capturedPiece, Position origin, Position destination)
        {
            // The UndoMovement method returns the captured Pawn to the wrong position after a EnPessant play is undone
            // The method bellow will take this Pawn and move it from the wrong position to the right position
            if (pieceToGoBack is Pawn && origin.Column != destination.Column && capturedPiece == VulnerableToEnPassant)
            {
                Piece capturedPawn = Board.RemovePieceFromBoard(destination);
                Position capturedPawnOrigin;
                if (pieceToGoBack.Color == PieceColor.White)
                {
                    capturedPawnOrigin = new Position(3, destination.Column);
                }
                else
                {
                    capturedPawnOrigin = new Position(4, destination.Column);
                }
                Board.AddPieceToBoard(capturedPawn, capturedPawnOrigin);
            }
        }

        public void ExecutePlay(Position origin, Position destination)
        {
            Piece capturedPiece = ExecuteMovement(origin, destination);
            if (IsKingInCheck(CurrentPlayer))
            {
                UndoMovement(origin, destination, capturedPiece);
                throw new BoardException("You cannot place your own King in check. Your play will be undone.");
            }

            // Get a reference for the piece moved in this turn
            Piece pieceMovedInTheTurn = Board.Piece(destination);
            // Special Play - Promotion
            if (pieceMovedInTheTurn is Pawn)
            {
                if (pieceMovedInTheTurn.Color == PieceColor.White && destination.Line == 0 || pieceMovedInTheTurn.Color == PieceColor.Black && destination.Line == 7)
                {
                    pieceMovedInTheTurn = Board.RemovePieceFromBoard(destination);
                    _piecesInGame.Remove(pieceMovedInTheTurn);
                    Piece queen = new Queen(pieceMovedInTheTurn.Color, Board);
                    Board.AddPieceToBoard(queen, destination);
                    _piecesInGame.Add(queen);
                }
            }

            if (IsKingInCheck(FindAdversaryColor(CurrentPlayer)))
            {
                IsInCheck = true;
            }
            else
            {
                IsInCheck = false;
            }

            if (IsKingInCheckMate(FindAdversaryColor(CurrentPlayer)))
            {
                MatchIsFinished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }

            // Special Play - En Passant
            if ((pieceMovedInTheTurn is Pawn && destination.Line == origin.Line - 2) || destination.Line == origin.Line + 2)
            {
                VulnerableToEnPassant = pieceMovedInTheTurn;
            }
            else
            {
                VulnerableToEnPassant = null;
            }
        }

        public void ValidateOriginPosition(Position position)
        {
            if (Board.Piece(position) == null)
            {
                throw new BoardException("There are no Pieces at the selected position.");
            }
            if (CurrentPlayer != Board.Piece(position).Color)
            {
                throw new BoardException("A Player cannot move the adversary's Pieces.");
            }
            if (!Board.Piece(position).CanPieceMove())
            {
                throw new BoardException("The Piece selected cannot make any moves.");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.Piece(origin).CanMoveToDestination(destination))
            {
                throw new BoardException("Invalid destination. Press enter to try again.");
            }
        }

        private void ChangePlayer()
        {
            if (CurrentPlayer == PieceColor.White)
            {
                CurrentPlayer = PieceColor.Black;
            }
            else
            {
                CurrentPlayer = PieceColor.White;
            }
        }

        public List<Piece> CapturedPieces(PieceColor pieceColor)
        {
            List<Piece> auxList = new List<Piece>();
            foreach (Piece piece in _piecesCaptured)
            {
                if (piece.Color == pieceColor)
                {
                    auxList.Add(piece);
                }
            }
            return auxList;
        }

        public List<Piece> PiecesInGame(PieceColor pieceColor)
        {
            List<Piece> auxList = new List<Piece>();
            foreach (Piece piece in _piecesInGame)
            {
                if (piece.Color == pieceColor)
                {
                    auxList.Add(piece);
                }
            }
            // Remove the captured pieces from the list so they don't appear on the screen agaim when the game is updated
            auxList.RemoveAll(p => CapturedPieces(pieceColor).Contains(p));
            return auxList;
        }

        private void PlaceNewPiece(char column, int line, Piece piece)
        {
            //
            Board.AddPieceToBoard(piece, new ChessPosition(column, line).ToPosition());
            _piecesInGame.Add(piece);
        }

        private void PlacePiecesOnBoard()
        {
            // White Pieces, first line
            PlaceNewPiece('a', 1, new Rook(PieceColor.White, Board));
            PlaceNewPiece('b', 1, new Knight(PieceColor.White, Board));
            PlaceNewPiece('c', 1, new Bishop(PieceColor.White, Board));
            PlaceNewPiece('d', 1, new Queen(PieceColor.White, Board));
            PlaceNewPiece('e', 1, new King(PieceColor.White, Board, this));
            PlaceNewPiece('f', 1, new Bishop(PieceColor.White, Board));
            PlaceNewPiece('g', 1, new Knight(PieceColor.White, Board));
            PlaceNewPiece('h', 1, new Rook(PieceColor.White, Board));
            // White Pieces, second line
            PlaceNewPiece('a', 2, new Pawn(PieceColor.White, Board, this));
            PlaceNewPiece('b', 2, new Pawn(PieceColor.White, Board, this));
            PlaceNewPiece('c', 2, new Pawn(PieceColor.White, Board, this));
            PlaceNewPiece('d', 2, new Pawn(PieceColor.White, Board, this));
            PlaceNewPiece('e', 2, new Pawn(PieceColor.White, Board, this));
            PlaceNewPiece('f', 2, new Pawn(PieceColor.White, Board, this));
            PlaceNewPiece('g', 2, new Pawn(PieceColor.White, Board, this));
            PlaceNewPiece('h', 2, new Pawn(PieceColor.White, Board, this));
            // Black Pieces first line
            PlaceNewPiece('a', 8, new Rook(PieceColor.Black, Board));
            PlaceNewPiece('b', 8, new Knight(PieceColor.Black, Board));
            PlaceNewPiece('c', 8, new Bishop(PieceColor.Black, Board));
            PlaceNewPiece('d', 8, new Queen(PieceColor.Black, Board));
            PlaceNewPiece('e', 8, new King(PieceColor.Black, Board, this));
            PlaceNewPiece('f', 8, new Bishop(PieceColor.Black, Board));
            PlaceNewPiece('g', 8, new Knight(PieceColor.Black, Board));
            PlaceNewPiece('h', 8, new Rook(PieceColor.Black, Board));
            // Black Pieces, second line
            PlaceNewPiece('a', 7, new Pawn(PieceColor.Black, Board, this));
            PlaceNewPiece('b', 7, new Pawn(PieceColor.Black, Board, this));
            PlaceNewPiece('c', 7, new Pawn(PieceColor.Black, Board, this));
            PlaceNewPiece('d', 7, new Pawn(PieceColor.Black, Board, this));
            PlaceNewPiece('e', 7, new Pawn(PieceColor.Black, Board, this));
            PlaceNewPiece('f', 7, new Pawn(PieceColor.Black, Board, this));
            PlaceNewPiece('g', 7, new Pawn(PieceColor.Black, Board, this));
            PlaceNewPiece('h', 7, new Pawn(PieceColor.Black, Board, this));

        }

        private PieceColor FindAdversaryColor(PieceColor currentPlayerColor)
        {
            if (currentPlayerColor == PieceColor.White)
            {
                return PieceColor.Black;
            }
            else
            {
                return PieceColor.White;
            }
        }

        private Piece FindKingOnBoard(PieceColor color)
        {
            foreach (Piece piece in PiecesInGame(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }
        // Check if the King is in check (if the adversary can capture it)
        private bool IsKingInCheck(PieceColor color)
        {
            Piece adversaryKing = FindKingOnBoard(color);
            if (adversaryKing == null)
            {
                throw new BoardException("Could not find a " + color + " King on the board.");
            }

            foreach (Piece piece in PiecesInGame(FindAdversaryColor(color)))
            {
                bool[,] possibleMovements = piece.PossibleMovements();
                if (possibleMovements[adversaryKing.Position.Line, adversaryKing.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }
        // Check if the King is in checkmate (if the adversary can capture it and there is no way to escape)
        private bool IsKingInCheckMate(PieceColor color)
        {
            if (!IsKingInCheck(color))
            {
                return false;
            }
            foreach (Piece piece in PiecesInGame(color))
            {
                bool[,] possibleMovementsMatrix = piece.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (possibleMovementsMatrix[i, j])
                        {
                            Position origin = piece.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destination);
                            bool isKingInCheck = IsKingInCheck(color);
                            UndoMovement(origin, destination, capturedPiece);
                            if (!isKingInCheck)
                                return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
