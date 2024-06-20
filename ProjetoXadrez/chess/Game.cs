
using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Game
    {
        public ChessBoard board { get; private set; }
        private int move;
        private Color player;

        public Game()
        {
            board = new ChessBoard(8, 8);
            move = 1;
            player = Color.White;
        }

        public void ExecuteMoviment(Position initial, Position end)
        {
            ChessPiece piece = board.removePiece(initial);
            piece.incrementMovement();
            board.removePiece(end);
            board.addPiece(piece, end);
        }

        public void addPieces()
        {
            ChessPiece pawn = new Pawn(Color.Red, board);
            ChessPiece king = new King(Color.Red, board);
            ChessPiece queen = new Queen(Color.White, board);
            ChessPiece rook = new Rook(Color.Red, board);
            ChessPiece knight = new Knight(Color.Red, board);
            ChessPiece bishop = new Bishop(Color.Red, board);

            board.addPiece(queen, ConvertPosition('a', 2));
            board.addPiece(pawn, ConvertPosition('a', 1));
            board.addPiece(king, ConvertPosition('a', 4));
            board.addPiece(bishop, ConvertPosition('a', 3));
        }

        public Position ConvertPosition(char col, int row)
        {
            return new ChessPosition(col, row).toPosition();
        }
    }
}
