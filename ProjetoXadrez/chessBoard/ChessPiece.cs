using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoXadrez.chessBoard
{
    internal class ChessPiece
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

        public void incrementMovement()
        {
            movementsAmount++;
        }

        public virtual bool movimentValidation(Position end)
        {
            return board.isHiddenPosition(end);
        }
    }
}
