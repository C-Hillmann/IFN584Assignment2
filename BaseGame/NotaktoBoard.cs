using BaseFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGame
{
    public class NotaktoBoard : IBoard
    {
        public NotaktoGame Game { get; set; }

        private string[,] grid = new string[3,3];

        public int Size => 3;

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
            //TODO : Display function
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
