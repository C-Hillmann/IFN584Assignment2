using BaseFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGame
{
    public class CompositeNotaktoBoard : IBoard
    {
        public NotaktoGame Game { get; set; }

        private List<IBoard> notaktoBoards = new List<>();

        public CompositeNotaktoBoard() {
            for (int i = 0; i <= 2; i++) {
                notaktoBoards.Add(new NotaktoBoard()); // adding three individual notakto boards
            }
        }


        public int Size => 3;

        public string[,] boardGrid()
        {
            throw new NotImplementedException();
        }

        public IBoard CloneBoard()
        {
            throw new NotImplementedException();
        }

        public void Display()
        {
            for (int i = 0; i <= 2; i++) {
                Console.WriteLine($"Board {i + 1} : ")
                notaktoBoards[i].Display() + "\n";
            }
        }

        public string GetCell(int row, int col)
        {
            throw new NotImplementedException();
        }

        public void SetCell(int row, int col, string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IBoard> GetNotaktoBoards() {
            return notaktoBoards;
        }

        public IBoard GetNotaktoBoard(int index) {
            return notaktoBoards[index];
        }
    }
}
