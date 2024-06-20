using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Rook : ChessPiece
    {
        public Rook(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
