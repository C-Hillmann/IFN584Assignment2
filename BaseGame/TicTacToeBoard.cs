using BaseFramework;

namespace BaseGame
{
    public class TicTacToeBoard : IBoard
    {
        private string[,] grid;
        private int size;
        
        public int Size => size;

        public TicTacToeBoard(int boardSize = 3)
        {
            if (boardSize <= 0)
            {
                throw new ArgumentException("Board size must be greater than 0.");
            }
            
            size = boardSize;
            grid = new string[size, size];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            int pos = 1;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = "[" + pos.ToString() + "]";
                    pos++;
                }
            }
        }

        public string[,] boardGrid()
        {
            return grid;
        }

        public void SetCell(int row, int col, string value)
        {
            if (row < 0 || row >= size || col < 0 || col >= size)
            {
                throw new ArgumentOutOfRangeException("Row or column index is out of bounds.");
            }
            
            grid[row, col] = value;
        }

        public string GetCell(int row, int col)
        {
            if (row < 0 || row >= size || col < 0 || col >= size)
            {
                throw new ArgumentOutOfRangeException("Row or column index is out of bounds.");
            }
            
            return grid[row, col];
        }

        public void Display()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(grid[i, j].PadLeft(6) + "|");
                }
                Console.WriteLine();
            }
        }

        public IBoard CloneBoard()
        {
            var clonedBoard = new TicTacToeBoard(size);
            
            // Copy the grid state
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    clonedBoard.grid[i, j] = grid[i, j];
                }
            }
            
            return clonedBoard;
        }

        
        public bool CheckWin(int row, int col, string playerSymbol)
        {
            
            bool rowWin = true;
            for (int j = 0; j < size; j++)
            {
                if (grid[row, j] != playerSymbol)
                {
                    rowWin = false;
                    break;
                }
            }
            if (rowWin) return true;

            
            bool colWin = true;
            for (int i = 0; i < size; i++)
            {
                if (grid[i, col] != playerSymbol)
                {
                    colWin = false;
                    break;
                }
            }
            if (colWin) return true;

            
            if (row == col)
            {
                bool diagWin = true;
                for (int i = 0; i < size; i++)
                {
                    if (grid[i, i] != playerSymbol)
                    {
                        diagWin = false;
                        break;
                    }
                }
                if (diagWin) return true;
            }

            
            if (row + col == size - 1)
            {
                bool antiDiagWin = true;
                for (int i = 0; i < size; i++)
                {
                    if (grid[i, size - 1 - i] != playerSymbol)
                    {
                        antiDiagWin = false;
                        break;
                    }
                }
                if (antiDiagWin) return true;
            }

            return false;
        }

        public bool IsCellEmpty(int row, int col)
        {
            string cellValue = GetCell(row, col);
            return cellValue.StartsWith("[");
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (IsCellEmpty(i, j))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int GetPositionNumber(int row, int col)
        {
            return row * size + col + 1;
        }

        public (int row, int col) GetCoordinatesFromPosition(int position)
        {
            if (position < 1 || position > size * size)
            {
                throw new ArgumentOutOfRangeException("Position must be between 1 and " + (size * size));
            }
            
            position--; 
            int row = position / size;
            int col = position % size;
            return (row, col);
        }

        
        public int CalculateWinningSum()
        {
            
            return (size * size * size + size) / 2;
        }

        
        public bool CheckWinWithSum(int row, int col, int winningSum)
        {
            
            if (CheckRowSum(row, winningSum))
                return true;

            
            if (CheckColumnSum(col, winningSum))
                return true;

            
            if (row == col && CheckMainDiagonalSum(winningSum))
                return true;

            
            if (row + col == size - 1 && CheckAntiDiagonalSum(winningSum))
                return true;

            return false;
        }

        private bool CheckRowSum(int row, int winningSum)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int j = 0; j < size; j++)
            {
                string cellValue = grid[row, j];
                if (cellValue.StartsWith("["))
                {
                    hasAllNumbers = false;
                    break;
                }
                
                if (int.TryParse(cellValue, out int number))
                {
                    sum += number;
                }
                else
                {
                    hasAllNumbers = false;
                    break;
                }
            }

            return hasAllNumbers && sum == winningSum;
        }

        private bool CheckColumnSum(int col, int winningSum)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int i = 0; i < size; i++)
            {
                string cellValue = grid[i, col];
                if (cellValue.StartsWith("["))
                {
                    hasAllNumbers = false;
                    break;
                }
                
                if (int.TryParse(cellValue, out int number))
                {
                    sum += number;
                }
                else
                {
                    hasAllNumbers = false;
                    break;
                }
            }

            return hasAllNumbers && sum == winningSum;
        }

        private bool CheckMainDiagonalSum(int winningSum)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int i = 0; i < size; i++)
            {
                string cellValue = grid[i, i];
                if (cellValue.StartsWith("["))
                {
                    hasAllNumbers = false;
                    break;
                }
                
                if (int.TryParse(cellValue, out int number))
                {
                    sum += number;
                }
                else
                {
                    hasAllNumbers = false;
                    break;
                }
            }

            return hasAllNumbers && sum == winningSum;
        }

        private bool CheckAntiDiagonalSum(int winningSum)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int i = 0; i < size; i++)
            {
                string cellValue = grid[i, size - 1 - i];
                if (cellValue.StartsWith("["))
                {
                    hasAllNumbers = false;
                    break;
                }
                
                if (int.TryParse(cellValue, out int number))
                {
                    sum += number;
                }
                else
                {
                    hasAllNumbers = false;
                    break;
                }
            }

            return hasAllNumbers && sum == winningSum;
        }

        
        public bool CheckWin(int row, int col, int number, int winningSum)
        {
            return CheckWinWithSum(row, col, winningSum);
        }
    }
}