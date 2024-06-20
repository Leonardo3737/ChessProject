namespace ProjetoXadrez.chessBoard
{
    internal class Position
    {
        public int row {  get; set; }
        public int col { get; set; }

        public Position() 
        { 
        }
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }

        public override string ToString()
        {
            return $"{row}{col}";
        }
    }
}

