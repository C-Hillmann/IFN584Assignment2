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
    }
}
