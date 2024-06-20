using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoXadrez.chessBoard.exceptions
{
    class ChessBoardException: ApplicationException
    {
        public ChessBoardException(string error) : base(error) { }
    }
}
