using ProjetoXadrez.chessBoard.exceptions;

namespace ProjetoXadrez.chessBoard
{
    internal class ChessBoard
    {
        public int rows {  get; set; }
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

        public void addPiece(ChessPiece p, Position position)
        {
            if(isHiddenPosition(position)) throw new ChessBoardException("Ja existe uma peça nessa posição");
            _pieces[position.row, position.col] = p;
            p.position = position;
        }

        public ChessPiece removePiece(Position position)
        {
            if (Piece(position) == null) return null;
            ChessPiece aux = Piece(position);
            aux.position = null;
            _pieces[position.row, position.col] = null;
            return aux;
        }

        public bool isHiddenPosition(Position position)
        {
            validatePosition(position);
            return Piece(position) != null;
        }

        public void validatePosition(Position position)
        {
            if(position.col < 0 || position.col > cols || position.row < 0 || position.row > rows) throw new ChessBoardException("Posicao Invalida");
        }
    }
}
