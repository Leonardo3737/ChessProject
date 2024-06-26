using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoXadrez.chessBoard
{
    abstract class ChessPiece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movementsAmount { get; protected set; }
        public ChessBoard board { get; protected set; }
        public ChessPiece(Color color, ChessBoard board)
        {
            this.color = color;
            this.movementsAmount = 0;
            this.board = board;
        }

        protected bool canMove(Position pos)
        {
            ChessPiece p = board.Piece(pos);
            return p == null || p.color!= color;
        }

        public void incrementMovement()
        {
            movementsAmount++;
        }
        public bool isblocked()
        {
            bool[,] validsPositions = generateValidsPositions();
            for(int row = 0; row<board.cols;  row++)
            {
                for (int col = 0; col < board.cols; col++)
                {
                    if (validsPositions[row, col]) return false;
                }
            }
            return true;
        }
        public abstract bool[,] generateValidsPositions();
    }
}
