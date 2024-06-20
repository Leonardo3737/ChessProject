using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Knight : ChessPiece
    {
        public Knight(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
