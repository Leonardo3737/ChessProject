using ProjetoXadrez.chess.exceptions;
using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Game
    {
        public ChessBoard board { get; private set; }
        private int move;
        public bool isFinished {  get; private set; }
        private Color player;
        private HashSet<ChessPiece> pieces;
        private HashSet<ChessPiece> capturedPieces;

        public Game()
        {
            isFinished = false;
            board = new ChessBoard(8, 8);
            move = 1;
            player = Color.White;
            pieces = new HashSet<ChessPiece>();
            capturedPieces = new HashSet<ChessPiece>();
        }

        public void ExecuteMoviment(Position init, Position end)
        {
            ChessPiece piece = board.removePiece(init);
            piece.incrementMovement();
            ChessPiece destiny = board.removePiece(end);
            if (destiny != null) capturedPieces.Add(destiny);
            board.addPiece(piece, end);
        }

        public void makeMove(Position init, Position end)
        {
            VerifyMove(init, end);
            ExecuteMoviment(init, end);
            move++;
            player = move%2 == 0 ? Color.Red : Color.White;
        }

        public void VerifyMove(Position init, Position end)
        {
            ChessPiece piece = board.Piece(init);
            if (piece == null) throw new GameExceptions("Posição Vazia!");
            if (!piece.generateValidsPositions()[end.row, end.col]) throw new GameExceptions("Movimento Invalido");
            if (piece.isblocked()) throw new GameExceptions("A peça não possui movimentos validos!");
            if (piece.color != player) throw new GameExceptions("Não é sua vez!");
        }

        public void VerifyMove(Position init)
        {
            ChessPiece piece = board.Piece(init);
            if (piece == null) throw new GameExceptions("Posição Vazia!");
            if (piece.isblocked()) throw new GameExceptions("A peça não possui movimentos validos!");
            if (piece.color != player) throw new GameExceptions("Não é sua vez!");
        }

        public void addNewPiece(char col, int row, ChessPiece piece)
        {
            board.addPiece(piece, ConvertPosition(col, row));
            pieces.Add(piece);
        }

        public void addPieces()
        {
            ChessPiece pawn1 = new Pawn(Color.Red, board);
            ChessPiece pawn2 = new Pawn(Color.Red, board);
            ChessPiece pawnW = new Pawn(Color.White, board);
            ChessPiece pawnW1 = new Pawn(Color.White, board);
            ChessPiece king = new King(Color.Red, board);
            ChessPiece queen = new Queen(Color.White, board);
            ChessPiece rook = new Rook(Color.Red, board);
            ChessPiece knight = new Knight(Color.Red, board);
            ChessPiece bishop = new Bishop(Color.Red, board);

            //board.addPiece(queen, ConvertPosition('a', 1));
            addNewPiece('a', 7, pawn1);
            addNewPiece('c', 7, pawn2);
            addNewPiece('a', 6, pawnW);
            addNewPiece('b', 4, pawnW1);
            addNewPiece('f', 4, rook);
            //board.addPiece(king, ConvertPosition('c', 1));
            //board.addPiece(bishop, ConvertPosition('d', 1));
        }

        public Position ConvertPosition(char col, int row)
        {
            return new ChessPosition(col, row).toPosition();
        }

        public HashSet<ChessPiece> getCapturedPiecesByColor(Color col)
        {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            foreach (ChessPiece piece in capturedPieces) 
            {
                if (piece.color == col) aux.Add(piece);
            }
            return aux;
        }
    }
}
