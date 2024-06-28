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

        public ChessPiece ExecuteMove(Position init, Position end)
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
            if (check && isCheckMate(adversary(player)))
            {
                isFinished = true;
            }
            return destiny;
        }

        public void makeMove(Position init, Position end)
        {
            VerifyMove(init, end);
            ExecuteMove(init, end);
            move++;
            player = move % 2 == 0 ? Color.Black : Color.White;
        }

        public void VerifyMove(Position init, Position end)
        {
            ChessPiece piece = board.removePiece(init);
            if (piece == null)
                throw new GameExceptions("Posição Vazia!");

            ChessPiece capturedPiece = board.removePiece(end);
            board.addPiece(piece, end);

            if (isCheck(piece.color))
            {
                board.removePiece(end);
                board.addPiece(piece, init);
                if (capturedPiece != null)
                    board.addPiece(capturedPiece, end);
                throw new GameExceptions("O rei ficará em cheque!");
            }
            board.removePiece(end);
            if (capturedPiece != null)
                board.addPiece(capturedPiece, end);
            board.addPiece(piece, init);

            if (!piece.generateValidsPositions()[end.row, end.col])
                throw new GameExceptions("Movimento Invalido");
            if (piece.isblocked())
                throw new GameExceptions("A peça não possui movimentos validos!");
            if (piece.color != player)
                throw new GameExceptions("Não é sua vez!");
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
            ChessPiece pawn1 = new Pawn(Color.Black, board);
            ChessPiece pawn2 = new Pawn(Color.Black, board);
            ChessPiece pawnW = new Pawn(Color.White, board);
            ChessPiece pawnW1 = new Pawn(Color.White, board);
            ChessPiece kingB = new King(Color.Black, board);
            ChessPiece kingW = new King(Color.White, board);
            ChessPiece queen = new Queen(Color.White, board);
            ChessPiece rookW1 = new Rook(Color.White, board);
            ChessPiece rookW2 = new Rook(Color.White, board);
            ChessPiece rookB = new Rook(Color.Black, board);
            ChessPiece knight = new Knight(Color.Black, board);
            ChessPiece bishop = new Bishop(Color.Black, board);

            //board.addPiece(queen, ConvertPosition('a', 1));
            addNewPiece('a', 7, kingB);
            addNewPiece('h', 1, kingW);
            addNewPiece('b', 1, pawnW1);
            addNewPiece('b', 2, rookW1);
            addNewPiece('b', 8, pawn1);
            addNewPiece('c', 3, rookW2);
            //board.addPiece(king, ConvertPosition('c', 1));
            //board.addPiece(bishop, ConvertPosition('d', 1));
        }

        public bool isCheck(Color color)
        {
            ChessPiece king = getKingByColor(color);
            HashSet<ChessPiece> piecesInGame = getPiecesInGameByColor(adversary(color));
            if (king == null) throw new GameExceptions("Não possui rei no tabuleiro");

            foreach (ChessPiece piece in piecesInGame)
            {
                if(piece.position == null) 
                    continue;
                bool[,] validsPositions = piece.generateValidsPositions();
                if (validsPositions[king.position.row, king.position.col]) return true;
            }
            return false;
        }

        public bool isCheckMate(Color color)
        {
            if (!check)
                return false;
            foreach (ChessPiece piece in getPiecesInGameByColor(color))
            {
                bool[,] moviments = piece.generateValidsPositions();
                for (int row = 0; row < board.rows; row++)
                {
                    for (int col = 0; col < board.cols; col++)
                    {
                        if (!moviments[row, col]) 
                            continue;

                        Position init = piece.position;
                        Position destiny = new Position(row, col);
                        ChessPiece auxPiece = board.removePiece(init);
                        ChessPiece capturedPiece = board.removePiece(destiny);

                        board.addPiece(auxPiece, destiny);
                        bool testCheckMate = isCheck(color);
                        board.removePiece(destiny);
                        if (capturedPiece != null)
                            board.addPiece(capturedPiece, destiny);
                        board.addPiece(piece, init);
                        if (!testCheckMate)
                            return false;
                    }
                }
            }
            return true;
        }

        public ChessPiece getKingByColor(Color color)
        {
            foreach (ChessPiece piece in getPiecesInGameByColor(color))
            {
                if (piece is King) return piece;
            }
            return null;
        }

        public Color adversary(Color color)
        {
            if (color == Color.White) return Color.Black;
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
