using ProjetoXadrez.chess.exceptions;
using ProjetoXadrez.chessBoard;
using System.Runtime.ConstrainedExecution;

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

            if(piece is King && end.col == init.col+2)
            {
                Position posRookKingSide = new Position(init.row, init.col + 3);
                Position rookDestiny = new Position(init.row, init.col + 1);
                ChessPiece Rook = board.removePiece(posRookKingSide);
                Rook.incrementMovement();
                board.addPiece(Rook, rookDestiny);
            }

            if (piece is King && end.col == init.col - 2)
            {
                Position posRookKingSide = new Position(init.row, init.col - 4);
                Position rookDestiny = new Position(init.row, init.col - 1);
                ChessPiece Rook = board.removePiece(posRookKingSide);
                Rook.incrementMovement();
                board.addPiece(Rook, rookDestiny);
            }

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
            addNewPiece('a', 1, new Rook(Color.White, board));
            addNewPiece('b', 1, new Knight(Color.White, board));
            addNewPiece('c', 1, new Bishop(Color.White, board));
            addNewPiece('d', 1, new Queen(Color.White, board));
            addNewPiece('e', 1, new King(Color.White, board, this));
            addNewPiece('f', 1, new Bishop(Color.White, board));
            addNewPiece('g', 1, new Knight(Color.White, board));
            addNewPiece('h', 1, new Rook(Color.White, board));
            addNewPiece('a', 2, new Pawn(Color.White, board));
            addNewPiece('b', 2, new Pawn(Color.White, board));
            addNewPiece('c', 2, new Pawn(Color.White, board));
            addNewPiece('d', 2, new Pawn(Color.White, board));
            addNewPiece('e', 2, new Pawn(Color.White, board));
            addNewPiece('f', 2, new Pawn(Color.White, board));
            addNewPiece('g', 2, new Pawn(Color.White, board));
            addNewPiece('h', 2, new Pawn(Color.White, board));

            addNewPiece('a', 8, new Rook(Color.Black, board));
            addNewPiece('b', 8, new Knight(Color.Black, board));
            addNewPiece('c', 8, new Bishop(Color.Black, board));
            addNewPiece('d', 8, new Queen(Color.Black, board));
            addNewPiece('e', 8, new King(Color.Black, board, this));
            addNewPiece('f', 8, new Bishop(Color.Black, board));
            addNewPiece('g', 8, new Knight(Color.Black, board));
            addNewPiece('h', 8, new Rook(Color.Black, board));
            addNewPiece('a', 7, new Pawn(Color.Black, board));
            addNewPiece('b', 7, new Pawn(Color.Black, board));
            addNewPiece('c', 7, new Pawn(Color.Black, board));
            addNewPiece('d', 7, new Pawn(Color.Black, board));
            addNewPiece('e', 7, new Pawn(Color.Black, board));
            addNewPiece('f', 7, new Pawn(Color.Black, board));
            addNewPiece('g', 7, new Pawn(Color.Black, board));
            addNewPiece('h', 7, new Pawn(Color.Black, board));
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
