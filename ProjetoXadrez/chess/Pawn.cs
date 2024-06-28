using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Pawn : ChessPiece
    {
        private Game game;
        public Pawn(Color color, ChessBoard chessBoard, Game game) : base(color, chessBoard)
        {
            this.game = game;
        }

        public override bool[,] generateValidsPositions()
        {
            bool[,] auxValidsPositions = new bool[board.rows, board.cols];
            int rangeDefault = movementsAmount == 0 ? 3 : 2;
            int direction = color == Color.White ? -1 : 1;

            for (int i = 1; i < rangeDefault; i++)
            {
                if (board.Piece(position.row + i * direction, position.col) != null) break;
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

            // EnPassant

            if ((position.row == 3 && direction == -1) || (position.row == 4 && direction == 1))
            {
                Position left = new Position(position.row, position.col - 1);
                Position right = new Position(position.row, position.col + 1);

                if (board.isHeldPosition(left) && board.Piece(left).color != color && board.Piece(left) == game.vunerableEnPassant)
                {
                    auxValidsPositions[left.row + direction, left.col] = true;
                }

                if (board.isHeldPosition(right) && board.Piece(right).color != color && board.Piece(right) == game.vunerableEnPassant)
                {
                    auxValidsPositions[right.row + direction, right.col] = true;
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
