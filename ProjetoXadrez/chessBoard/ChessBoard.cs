using ProjetoXadrez.chessBoard.exceptions;

namespace ProjetoXadrez.chessBoard
{
    internal class ChessBoard
    {
        public int rows { get; set; }
        public int cols { get; set; }
        private ChessPiece[,] _pieces;

        public ChessBoard(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            _pieces = new ChessPiece[rows, cols];
        }

        public ChessPiece Piece(Position position)
        {
            return _pieces[position.row, position.col];
        }

        public ChessPiece Piece(int row, int col)
        {
            return _pieces[row, col];
        }

        public void addPiece(ChessPiece p, Position position)
        {
            if (isHeldPosition(position)) throw new ChessBoardException("Ja existe uma peça nessa posição");
            _pieces[position.row, position.col] = p;
            p.position = position;
            p.generateValidsPositions();
        }

        public ChessPiece removePiece(Position position)
        {
            if (Piece(position) == null) return null;
            ChessPiece aux = Piece(position);
            aux.position = null;
            _pieces[position.row, position.col] = null;
            return aux;
        }

        public bool isHeldPosition(Position position)
        {
            validatePosition(position);
            return Piece(position) != null;
        }

        public void validatePosition(Position position)
        {
            if (!isValidPosition(position))
                throw new ChessBoardException("Posicao Invalida");
        }

        public bool isValidPosition(Position position)
        {
            return !(position.row < 0 || position.row >= rows || position.col < 0 || position.col >= cols);
        }
    }
}
