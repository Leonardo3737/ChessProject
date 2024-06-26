using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Queen : ChessPiece
    {
        public Queen(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override bool[,] generateValidsPositions()
        {
            bool[,] auxValidsPositions = new bool[board.rows, board.cols];
            for (int row = 0; row < board.rows; row++)
            {
                for (int col = 0; col < board.cols; col++)
                {

                }
            }
            return auxValidsPositions;
        }

        public override string ToString()
        {
            return "r";
        }
    }
}
