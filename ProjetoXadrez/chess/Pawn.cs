using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Pawn : ChessPiece
    {

        public Pawn(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override bool movimentValidation(Position init, Position end)
        {
            int squaresAmout = movementsAmount == 0 ? 2 : 1;
            bool movementsValid = (init.col == end.col) && (Math.Abs(init.row - end.row) <= squaresAmout);
            return movementsValid && base.movimentValidation(init, end);
        }
        public override string ToString()
        {
            return "P";
        }
    }
}
