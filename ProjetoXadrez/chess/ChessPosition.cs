using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class ChessPosition
    {
        public int row { get; set; }
        public char col { get; set; }

        public ChessPosition(char col, int row)
        {
            this.row = row;
            this.col = col;
        }

        public Position toPosition()
        {
            return new Position(8-row, col-'a');
        }

        public override string ToString()
        {
            return $"{col}{row}";
        }
    }
}
