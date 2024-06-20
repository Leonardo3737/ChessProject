using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class King : ChessPiece
    {
        public King(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
