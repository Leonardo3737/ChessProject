namespace ProjetoXadrez
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard(8, 8);

            Console.WriteLine(board.ToString());
        }
    }
}
 