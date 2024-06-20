using ProjetoXadrez.chess;
using ProjetoXadrez.chessBoard;

namespace ProjetoXadrez
{
    class Screen
    {
        public static void print(ChessBoard board)
        {
            for (int r = 0; r<board.rows; r++)
            {
                Console.Write($"{8- r} ");
                for (int c = 0; c < board.cols; c++)
                {
                    if(board.Piece(new Position(r, c)) == null)
                    {
                        if (c % 2 != r % 2) changeColor("-");
                        else Console.Write("-");
                        Console.Write(" ");
                    }
                    else
                    {
                        if(board.Piece(new Position(r, c)).color == Color.White) Console.Write(board.Piece(new Position(r, c)));
                        else changeColor(board.Piece(new Position(r, c)).ToString());
                        Console.Write(" ");

                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void changeColor(string p)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(p);
            Console.ForegroundColor = aux;
        }

        public static Position ReadPosition()
        {
            string pos = Console.ReadLine();
            char row = pos[0];
            int col = int.Parse(pos[1]+"");
            return new ChessPosition(row, col).toPosition();
        }
    }
}
