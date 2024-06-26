using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Pawn : ChessPiece
    {

        public Pawn(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override bool[,] generateValidsPositions()
        {
            bool[,] auxValidsPositions = new bool[board.rows, board.cols];
            int rangeDefault = movementsAmount == 0 ? 3 : 2;
            int direction = color == Color.White ? -1 : 1;

            for (int i = 1; i < rangeDefault; i++)
            {
                if(board.Piece(position.row + i * direction, position.col) != null) break;
                auxValidsPositions[position.row + i * direction, position.col] = true;
            }

            if (position.col != 0)
            {
                ChessPiece existPieceOnLeft = board.Piece(position.row + direction, position.col - 1);
                if (existPieceOnLeft != null)
                {
                    auxValidsPositions[existPieceOnLeft.position.row, existPieceOnLeft.position.col] = existPieceOnLeft.color != color;
                }
            }
            if (position.col != 7)
            {
                ChessPiece existPieceOnRigth = board.Piece(position.row + direction, position.col + 1);
                if (existPieceOnRigth != null)
                {
                    auxValidsPositions[existPieceOnRigth.position.row, existPieceOnRigth.position.col] = existPieceOnRigth.color != color;
                }
            }

            return auxValidsPositions;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
