namespace BaseFramework
{
    public interface IBoard
    {
        int Size { get; }
        string[,] boardGrid();
        void SetCell(int row, int col, string value);
        string GetCell(int row, int col);
        void Display(); 
        IBoard CloneBoard(); // undo/redo
    }
}
