using BaseFramework;

namespace BaseGame;

public class CompositeNotaktoBoard : IBoard
{
    public List<NotaktoBoard> Boards { get; } = new List<NotaktoBoard>(3);
    public int Size => 9;

    public CompositeNotaktoBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            Boards.Add(new NotaktoBoard());
        }
    }

    public string[,] boardGrid()
    {
        string[,] combinedGrid = new string[9, 9];
        for (int i = 0; i < 3; i++)
        {
            var individualGrid = Boards[i].boardGrid();
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int combinedRowIndex = i * 3 + j;
                    int combinedColumnIndex = k + i * 3;
                    combinedGrid[combinedRowIndex, combinedColumnIndex] = individualGrid[j, k];
                }
            }
        }
        return combinedGrid;
    }

    public IBoard CloneBoard()
    {
        throw new NotImplementedException();
    }

    public void Display()
    {
        Console.WriteLine("Three Boards Notakto : Last player to complete the final board will lose the game");
        for (int row = 0; row < 3; row++)
        {
            for (int boardIndex = 0; boardIndex < 3; boardIndex++)
            {
                for (int column = 0; column < 3; column++)
                {
                    var individualCell = Boards[boardIndex].GetCell(row, column);
                    if (string.IsNullOrWhiteSpace(individualCell))
                    {
                        Console.Write("0");
                    }
                    else
                    {
                        Console.Write(individualCell);
                    }
                }
                Console.Write("  "); //putting an empty space between each boards in the row.
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public string GetCell(int row, int col)
    {
        int boardIndex = col / 3;
        int localRow = row % 3;
        int localCol = col % 3;
        return Boards[boardIndex].GetCell(localRow, localCol);
    }

    public void SetCell(int row, int col, string value)
    {
        int boardIndex = col / 3;
        int localRow = row % 3;
        int localCol = col % 3;
        Boards[boardIndex].SetCell(localRow, localCol, value);
    }

    public bool isAllCompleted()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!Boards[i].IsCompleted()) return false;
        }
        return true;
    }
}