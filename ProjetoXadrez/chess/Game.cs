using ProjetoXadrez.chess.exceptions;
using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez.chess
{
    internal class Game
    {
        public ChessBoard board { get; private set; }
        public int move { get; private set; }
        public bool isFinished { get; private set; }
        public bool check { get; private set; }
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

        public void ExecuteMove(Position init, Position end)
        {
            ChessPiece piece = board.removePiece(init);
            piece.incrementMovement();
            ChessPiece destiny = board.removePiece(end);
            if (destiny != null)
            {
                destiny.isCaptured = true;
                capturedPieces.Add(destiny);
            }
            board.addPiece(piece, end);
            check = isCheck(adversary(player));
        }

        public void makeMove(Position init, Position end)
        {
            VerifyMove(init, end);
            ExecuteMove(init, end);
            move++;
            player = move % 2 == 0 ? Color.Red : Color.White;
        }

        public void VerifyMove(Position init, Position end)
        {
            ChessPiece piece = board.removePiece(init);
            if (piece == null) throw new GameExceptions("Posição Vazia!");

            ChessPiece capturedPiece = board.removePiece(end);
            board.addPiece(piece, end);

            if (isCheck(piece.color))
            {
                board.removePiece(end);
                board.addPiece(piece, init);
                if (capturedPiece != null) board.addPiece(capturedPiece, end);
                throw new GameExceptions("O rei ficará em cheque!");
            }
            board.removePiece(end);
            if(capturedPiece != null) board.addPiece(capturedPiece, end);
            board.addPiece(piece, init);
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

        /*public void undoMovement(Position init, Position end, ChessPiece piece)
        {
            ChessPiece auxPiece = board.removePiece(end);
            piece.decrementMovement();
            if(piece != null)
            {
                board.addPiece(piece, end);
                capturedPieces.Remove(piece);
            }
            board.addPiece(auxPiece, init);
        }*/

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
            ChessPiece kingB = new King(Color.Red, board);
            ChessPiece kingW = new King(Color.White, board);
            ChessPiece queen = new Queen(Color.White, board);
            ChessPiece rookW = new Rook(Color.White, board);
            ChessPiece rookB = new Rook(Color.Red, board);
            ChessPiece knight = new Knight(Color.Red, board);
            ChessPiece bishop = new Bishop(Color.Red, board);

            //board.addPiece(queen, ConvertPosition('a', 1));
            addNewPiece('a', 7, pawn1);
            addNewPiece('c', 7, pawn2);
            addNewPiece('a', 3, kingB);
            addNewPiece('a', 1, kingW);
            addNewPiece('b', 1, pawnW1);
            addNewPiece('f', 1, rookB);
            addNewPiece('d', 5, rookW);
            //board.addPiece(king, ConvertPosition('c', 1));
            //board.addPiece(bishop, ConvertPosition('d', 1));
        }

        public bool isCheck(Color color)
        {
            ChessPiece king = getKingByColor(color);
            HashSet<ChessPiece> piecesInGame = getPiecesInGameByColor(adversary(color));
            if (king == null) throw new GameExceptions("Não possui rei no tabuleiro");

            foreach(ChessPiece piece in piecesInGame) 
            {
                bool[,] validsPositions = piece.generateValidsPositions();
                if (validsPositions[king.position.row, king.position.col]) return true;
            }
            return false;
        }

        public ChessPiece getKingByColor(Color color)
        {
            foreach(ChessPiece piece in getPiecesInGameByColor(color))
            {
                if(piece is King) return piece;
            }
            return null;
        }

        public Color adversary(Color color)
        {
            if (color == Color.White) return Color.Red;
            return Color.White;
        }

        public Position ConvertPosition(char col, int row)
        {
            return new ChessPosition(col, row).toPosition();
        }

        public HashSet<ChessPiece> getCapturedPiecesByColor(Color color)
        {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            foreach (ChessPiece piece in capturedPieces)
            {
                if (piece.color == color) aux.Add(piece);
            }
            return aux;
        }

        public HashSet<ChessPiece> getPiecesInGameByColor(Color color)
        {
            HashSet<ChessPiece> aux = new HashSet<ChessPiece>();
            foreach (ChessPiece piece in pieces)
            {
                if (piece.color == color && !piece.isCaptured) aux.Add(piece);
            }
            return aux;
        }
    }
}
