using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Knight : ChessPiece
    {
        public Knight(Color color, ChessBoard chessBoard) : base(color, chessBoard)
        {
        }

        public override bool[,] generateValidsPositions()
        {
            bool[,] auxValidsPositions = new bool[board.rows, board.cols];

            Position pos = new Position(0, 0);

            pos.setPosition(position.row- 1, position.col- 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }
            pos.setPosition(position.row- 2, position.col- 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }
            pos.setPosition(position.row- 2, position.col+ 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }
            pos.setPosition(position.row- 1, position.col+ 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }
            pos.setPosition(position.row+ 1, position.col+ 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }
            pos.setPosition(position.row+ 2, position.col+ 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }
            pos.setPosition(position.row+ 2, position.col- 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }
            pos.setPosition(position.row+ 1, position.col- 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                auxValidsPositions[pos.row, pos.col] = true;
            }

            return auxValidsPositions;
        }

        public override string ToString()
        {
            return "C";
        }
    }
}
