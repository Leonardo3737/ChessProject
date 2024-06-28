using ProjetoXadrez.chess;
using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez
{
    class Screen
    {
        public static void printGame(Game game)
        {
            if (!game.isFinished && game.check) Console.WriteLine("XEQUE!!");
            else if (!game.isFinished) Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"Turno: {game.move}");
            printCapturedsPiece(game);
            Console.WriteLine();
            Console.WriteLine();
            printBoard(game.board);
            Console.WriteLine();
        }

        public static void printGame(Game game, bool[,] validsPositions)
        {
            if (!game.isFinished && game.check) Console.WriteLine("XEQUE!!");
            else Console.WriteLine();
            Console.WriteLine($"Turno: {game.move}");
            printCapturedsPiece(game);
            Console.WriteLine();
            Console.WriteLine();
            printBoard(game.board, validsPositions);
            Console.WriteLine();
        }

        public static void printBoard(ChessBoard board)
        {
            ConsoleColor auxBackground = Console.BackgroundColor;
            for (int r = 0; r < board.rows; r++)
            {
                Console.Write($"{8 - r} ");
                Console.BackgroundColor = ConsoleColor.Gray;
                for (int c = 0; c < board.cols; c++)
                {
                    if (c % 2 == r % 2) Console.BackgroundColor = ConsoleColor.Red;

                    ChessPiece piece = board.Piece(new Position(r, c));
                    printPiece(piece);
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
                Console.BackgroundColor = auxBackground;
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(ChessBoard board, bool[,] validsPositions)
        {
            ConsoleColor auxBackground = Console.BackgroundColor;

            for (int r = 0; r < board.rows; r++)
            {
                Console.Write($"{8 - r} ");
                Console.BackgroundColor = ConsoleColor.Gray;
                for (int c = 0; c < board.cols; c++)
                {
                    if (validsPositions[r, c])
                    {
                        if (c % 2 == r % 2) Console.BackgroundColor = ConsoleColor.DarkRed;
                        else Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    else if (c % 2 == r % 2) Console.BackgroundColor = ConsoleColor.Red;

                    ChessPiece piece = board.Piece(new Position(r, c));
                    printPiece(piece);
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                Console.WriteLine();
                Console.BackgroundColor = auxBackground;
            }

            Console.WriteLine("  a b c d e f g h");
        }
        
        public static void printCapturedsPiece(Game game)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.WriteLine("Peças Capturadas");
            Console.Write("Brancas: ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            printList(game.getCapturedPiecesByColor(Color.White));
            Console.ForegroundColor = aux;
            Console.WriteLine();
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Black;
            printList(game.getCapturedPiecesByColor(Color.Black));
            Console.ForegroundColor = aux;
        }

        public static void printList(HashSet<ChessPiece> pieces)
        {
            if (pieces.Count == 0) return;
            ConsoleColor aux = Console.BackgroundColor;
            if (pieces.First<ChessPiece>().color == Color.Black) Console.BackgroundColor = ConsoleColor.White;
            Console.Write("[ ");
            foreach (ChessPiece piece in pieces)
            {
                Console.Write($"{piece} ");
            }
            Console.Write(" ]");
            Console.BackgroundColor = aux;
        }
 
        public static void printPiece(ChessPiece piece)
        {
            if (piece == null)
            {
                Console.Write("  ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                if (piece.color == Color.White) Console.Write(piece);
                else changeColor(piece.ToString());
                Console.Write(" ");
                Console.ForegroundColor = aux;
            }
        }

        public static void changeColor(string p)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(p);
            Console.ForegroundColor = aux;
        }

        public static Position ReadPosition()
        {
            string pos = Console.ReadLine();
            char row = pos[0];
            int col = int.Parse(pos[1] + "");
            return new ChessPosition(row, col).toPosition();
        }
    }
}
