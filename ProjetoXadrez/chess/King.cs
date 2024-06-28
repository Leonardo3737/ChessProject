using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class King : ChessPiece
    {
        public Game game {  get; set; }
        public King(Color color, ChessBoard chessBoard, Game game) : base(color, chessBoard)
        {
            this.game = game;
        }

        public bool canCastling(Position position)
        {
            ChessPiece piece = board.Piece(position);
            return piece != null && piece is Rook && piece.color == color && piece.movementsAmount == 0;
        }

        public override bool[,] generateValidsPositions()
        {
            bool[,] auxValidsPositions = new bool[board.rows, board.cols];
            for (int row = 0; row < board.rows; row++)
            {
                for (int col = 0; col < board.cols; col++)
                {
                    bool colRange = Math.Abs(position.col - col) <= 1;
                    bool rowRange = Math.Abs(position.row - row) <= 1;
                    ChessPiece piece = board.Piece(row, col);
                    auxValidsPositions[row, col] = colRange && rowRange;
                    if (piece != null && piece.color == color) auxValidsPositions[row, col] = false;
                }
            }

            if (movementsAmount > 0 || game.check || position.col != 4)
                return auxValidsPositions;

            // kingside castling

            Position posRookKingSide = new Position(position.row, position.col+3);
            if(canCastling(posRookKingSide)){
                bool aux = true;
                for (int col = position.col+1;  col < position.col+2; col++)
                {
                    Position positionToTest = new Position(position.row, col);
                    if (board.Piece(positionToTest) != null)
                        aux = false;
                }
                auxValidsPositions[position.row, position.col+2] = aux;
            }

            // kingside castling

            Position posRookQueenSide = new Position(position.row, position.col - 4);
            if (canCastling(posRookQueenSide))
            {
                bool aux = true;
                for (int col = position.col - 1; col > position.col -3; col--)
                {
                    Position positionToTest = new Position(position.row, col);
                    if (board.Piece(positionToTest) != null)
                        aux = false;
                }
                auxValidsPositions[position.row, position.col - 2] = aux;
            }


            return auxValidsPositions;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
