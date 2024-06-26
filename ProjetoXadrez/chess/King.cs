using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class King : ChessPiece
    {
        public King(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override bool[,] generateValidsPositions()
        {
            bool[,] auxValidsPositions = new bool[board.rows, board.cols];
            for (int row = 0; row < board.rows; row++)
            {
                for (int col = 0; col < board.cols; col++)
                {
                    bool colRange = Math.Abs(position.col - col) <= 1;
                    bool rowRange = Math.Abs(position.row - row) <= 1;
                    auxValidsPositions[row, col] = colRange && rowRange;
                }
            }
            return auxValidsPositions;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
