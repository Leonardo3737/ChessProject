using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoXadrez
{
    internal class ChessPiece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int movementsAmount { get; protected set; }
        public ChessBoard chessBoard { get; protected set; }


        public ChessPiece()
        {
        }

        public ChessPiece(Position position, Color color, ChessBoard chessBoard)
        {
            this.position = position;
            this.color = color;
            this.movementsAmount = 0;
            this.chessBoard = chessBoard;
        }
    }
}
