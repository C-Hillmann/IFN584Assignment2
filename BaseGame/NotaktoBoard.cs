using BaseFramework;


namespace BaseGame
{
    public class NotaktoBoard : IBoard
    {
        public Game Game { get; set; }

        private string[,] board = new string[3, 3];

        public int Size => 3;

        public string[,] boardGrid()
        {
            return board;
        }

        public IBoard CloneBoard()
        {
            var newBoard = new NotaktoBoard();
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    newBoard.board[row, column] = board[row, column];
                }
            }
            newBoard.Game = Game;
            return newBoard;
        }

        public void Display()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 3; column++)
                {
                    string? currentValue;
                    if (string.IsNullOrWhiteSpace(board[row, column]))
                    {
                        currentValue = "0";
                    }
                    else
                    {
                        currentValue = board[row, column];
                    }
                    Console.Write(currentValue);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public string GetCell(int row, int col)
        {
            return board[row, col];
        }

        public void SetCell(int row, int col, string value)
        {
            board[row, col] = value;
        }

        public bool IsCompleted()
        {
            // check each rows, columns, two diagonals. It can be done using 1 for loop. 
            if (!string.IsNullOrWhiteSpace(board[0, 0]) && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) return true; //checking primary diagonal
            if (!string.IsNullOrWhiteSpace(board[0, 2]) && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) return true; // checking secondary diagonal

            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrWhiteSpace(board[i, 0]) && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) return true; //checking each rows
                if (!string.IsNullOrWhiteSpace(board[0, i]) && board[0, i] == board[1, i] && board[1, i] == board[2, i]) return true; //checking each columns
            }
            return false;
        }
    }
}