using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Bishop : ChessPiece
    {
        public Bishop(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override string ToString()
        {
            return "B";
        }
    }
}
