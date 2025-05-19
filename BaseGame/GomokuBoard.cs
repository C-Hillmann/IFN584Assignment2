using BaseFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGame
{
    public class GomokuBoard : IBoard
    {
        public GomokuGame Game { get; set; }

        private string[,] grid = new string[15,15];

        public int Size => 15;

        public string[,] boardGrid()
        {
            return grid;
        }

        public IBoard CloneBoard()
        {
            throw new NotImplementedException();
        }

        public void Display()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    var cellValue = grid[row, col];
                    if ((cellValue == null) || cellValue == "")
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(grid[row, col]);
                    }

                    if (col < Size - 1)
                    {
                        Console.Write("|");
                    }

                }
                Console.WriteLine();
            }
        }

        public string GetCell(int row, int col)
        {
            return grid[row, col];
        }

        public void SetCell(int row, int col, string value)
        {
            grid[row, col] = value;
        }
    }
}
