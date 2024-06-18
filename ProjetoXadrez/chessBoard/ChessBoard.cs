using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoXadrez
{
    internal class ChessBoard
    {
        public int rows {  get; set; }
        public int cols { get; set; }
        public ChessPiece[,] pieces;

        public ChessBoard(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            pieces = new ChessPiece[rows, cols];
        }
    }
}
