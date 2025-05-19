namespace BaseFramework
{
    public class Move : IMove
    {
        public int Row { get; }
        public int Col { get; }
        public string Value { get; }
        public Player Player { get; }

        public Move(int row, int col, string value, Player player)
        {
            Row = row;
            Col = col;
            Value = value;
            Player = player;
        }

        // undo/redo
        public class PlaceMoveCommand : ICommand
        {
            private IMove move;
            private string previousValue;

            public PlaceMoveCommand(IMove move)
            {
                this.move = move;
            }

            public void Execute(IBoard board)
            {
                previousValue = board.GetCell(move.Row, move.Col);
                board.SetCell(move.Row, move.Col, move.Value);
            }

            public void Undo(IBoard board)
            {
                board.SetCell(move.Row, move.Col, previousValue);
            }
        }
    }
}
