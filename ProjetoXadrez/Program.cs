using ProjetoXadrez.chess;
using ProjetoXadrez.chessBoard;
using ProjetoXadrez.chess;

namespace ProjetoXadrez
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.addPieces();
            

            while (true)
            {
                try
                {
                    Screen.print(game.board);

                    Position initial = Screen.ReadPosition();
                    Position destiny = Screen.ReadPosition();

                    game.ExecuteMoviment(initial, destiny);

                    Screen.print(game.board);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
            


            /*for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (r == 0 || r == 7)
                    {
                        if (c == 0 || c == 7) board.addPiece(rook, new Position(r, c));
                        else if (c == 1 || c == 6) board.addPiece(knight, new Position(r, c));
                        else if (c == 2 || c == 5) board.addPiece(bishop, new Position(r, c));
                        else if (c == 3) board.addPiece(king, new Position(r, c));
                        else board.addPiece(queen, new Position(r, c));
                    }
                    if (r ==1 || r == 6) board.addPiece(pawn, new Position(r, c));
                }
            }*/
        }
    }
}
