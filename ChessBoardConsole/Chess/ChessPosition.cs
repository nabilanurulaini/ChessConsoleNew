using GameBoard;

namespace Chess
{
    class ChessPosition
    {
        public char Column { get; set; }
        public int Line { get; set; }

        public ChessPosition(char column, int line)
        {
            Column = column;
            Line = line;
        }

        public Position ToPosition()
        {
            // Convert the Chess Position to an Matrix Position, mmengurangi line dengan 8 karena urutan pada board 1 dari bawah ke atas, 
            // dan dengan mengurangi nilai "Column" dari karakter 'a', maka huruf yang mewakili kolom pada papan catur dapat dikonversi ke dalam angka yang mewakili kolom pada matriks.
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            // By placing two double quote signs before the variables one can automatically convert the variables to String
            return "" + Column + Line;
        }
    }
}
