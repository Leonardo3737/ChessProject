using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Rook : ChessPiece
    {
        public Rook(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override bool[,] generateValidsPositions()
        {
            bool[,] auxValidsPositions = new bool[board.rows, board.cols];
            Position auxPos = new Position(0, 0);

            auxPos.setPosition(position.row + 1, position.col);
            while (board.isValidPosition(auxPos) && canMove(auxPos))
            {
                auxValidsPositions[auxPos.row, auxPos.col] = true;
                if (board.Piece(auxPos) != null && board.Piece(auxPos).color != color) break;
                auxPos.row += 1;
            }

            auxPos.setPosition(position.row - 1, position.col);
            while (board.isValidPosition(auxPos) && canMove(auxPos))
            {
                auxValidsPositions[auxPos.row, auxPos.col] = true;
                if (board.Piece(auxPos) != null && board.Piece(auxPos).color != color) break;
                auxPos.row -= 1;
            }

            auxPos.setPosition(position.row, position.col+1);
            while (board.isValidPosition(auxPos) && canMove(auxPos))
            {
                auxValidsPositions[auxPos.row, auxPos.col] = true;
                if (board.Piece(auxPos) != null && board.Piece(auxPos).color != color) break;
                auxPos.col += 1;
            }

            auxPos.setPosition(position.row, position.col-1);
            while (board.isValidPosition(auxPos) && canMove(auxPos))
            {
                auxValidsPositions[auxPos.row, auxPos.col] = true;
                if (board.Piece(auxPos) != null && board.Piece(auxPos).color != color) break;
                auxPos.col -= 1;
            }
            return auxValidsPositions;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
