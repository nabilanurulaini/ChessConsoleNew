namespace GameBoard
{
    class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;
        // define a constructor with no arguments
        public Board() { }
        // overload the constructor with arguments
        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }
        // method to return a piece from the matrix
        public Piece Piece(int line, int column)
        {
            return Pieces[line, column];
        }
        // method untuk mengembalikan nilai dari Piece dengan parameter Position
        public Piece Piece(Position position)
        {
            return Pieces[position.Line, position.Column];
        }

        public bool PositionOccupied(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void AddPieceToBoard(Piece pieceToAdd, Position position)
        {
            if (PositionOccupied(position))
            {
                throw new BoardException("Error! Cannot place two Pieces at the same Position. Please try again.");
            }
            // Insert the piece at the defined position on the Board Class
            Pieces[position.Line, position.Column] = pieceToAdd;
            // Register the piece's position on the Piece Class
            pieceToAdd.Position = position;
        }

        public Piece RemovePieceFromBoard(Position position)
        {
            if (Piece(position) == null)
            {
                return null;
            }
            Piece pieceToRemove = Piece(position);
            pieceToRemove.Position = null;
            Pieces[position.Line, position.Column] = null;
            return pieceToRemove;
        }
        // Checks if the position is valid not out of bounds
        public bool ValidPosition(Position position)
        {
            if (position.Line < 0 || position.Line >= Lines || position.Column < 0 || position.Column >= Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Error! Invalid Position detected. Please try again.");
            }
        }
    }
}
