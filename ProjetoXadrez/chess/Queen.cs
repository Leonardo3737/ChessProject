using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Queen : ChessPiece
    {
        public Queen(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override string ToString()
        {
            return "r";
        }
    }
}
