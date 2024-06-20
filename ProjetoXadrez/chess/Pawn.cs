using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Pawn : ChessPiece
    {
        public Pawn(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
