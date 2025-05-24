using BaseFramework;
using System;
using System.Collections.Generic;

namespace BaseGame
{
    public class TicTacToeGameLogic : IGameLogic
    {
        private bool gameOver = false;

        public bool IsValidMove(IMove move, IBoard board)
        {
            if (move.Row < 0 || move.Row >= board.Size || 
                move.Col < 0 || move.Col >= board.Size)
            {
                return false;
            }

            var ticTacToeBoard = board as TicTacToeBoard;
            if (ticTacToeBoard == null)
            {
                return false;
            }

            if (!ticTacToeBoard.IsCellEmpty(move.Row, move.Col))
            {
                return false;
            }

            if (!IsValidNumberForPlayer(move.Value, move.Player))
            {
                return false;
            }

            return true;
        }

        public void MakeMove(IMove move, IBoard board)
        {
            board.SetCell(move.Row, move.Col, move.Value);
        }

        public bool CheckWin(IMove lastMove, IBoard board)
        {
            var ticTacToeBoard = board as TicTacToeBoard;
            if (ticTacToeBoard == null)
            {
                return false;
            }

            int winningSum = GetWinningSum(board.Size);
            return CheckWinWithSum(lastMove.Row, lastMove.Col, winningSum, board);
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

        public List<IMove> GetAvailableMoves(IBoard board)
        {
            var moves = new List<IMove>();
            var ticTacToeBoard = board as TicTacToeBoard;
            
            if (ticTacToeBoard == null)
            {
                return moves;
            }

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (ticTacToeBoard.IsCellEmpty(i, j))
                    {
                        moves.Add(new TicTacToeMove(i, j, "1", null));
                    }
                }
            }

            return moves;
        }

        public int GetWinningSum(int boardSize)
        {
            return (boardSize * boardSize * boardSize + boardSize) / 2;
        }

        private bool CheckWinWithSum(int row, int col, int winningSum, IBoard board)
        {
            if (CheckRowSum(row, winningSum, board))
            {
                gameOver = true;
                return true;
            }

            if (CheckColumnSum(col, winningSum, board))
            {
                gameOver = true;
                return true;
            }

            if (row == col && CheckMainDiagonalSum(winningSum, board))
            {
                gameOver = true;
                return true;
            }

            if (row + col == board.Size - 1 && CheckAntiDiagonalSum(winningSum, board))
            {
                gameOver = true;
                return true;
            }

            var ticTacToeBoard = board as TicTacToeBoard;
            if (ticTacToeBoard != null && ticTacToeBoard.IsBoardFull())
            {
                gameOver = true;
                return false;
            }

            return false;
        }

        private bool CheckRowSum(int row, int winningSum, IBoard board)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int j = 0; j < board.Size; j++)
            {
                string cellValue = board.GetCell(row, j);
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

        private bool CheckColumnSum(int col, int winningSum, IBoard board)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int i = 0; i < board.Size; i++)
            {
                string cellValue = board.GetCell(i, col);
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

        private bool CheckMainDiagonalSum(int winningSum, IBoard board)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int i = 0; i < board.Size; i++)
            {
                string cellValue = board.GetCell(i, i);
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

        private bool CheckAntiDiagonalSum(int winningSum, IBoard board)
        {
            int sum = 0;
            bool hasAllNumbers = true;

            for (int i = 0; i < board.Size; i++)
            {
                string cellValue = board.GetCell(i, board.Size - 1 - i);
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

        private bool IsValidNumberForPlayer(string value, Player player)
        {
            if (!int.TryParse(value, out int number))
            {
                return false;
            }

            int[] playerNumbers = player.IsOdd ? player.oddNum : player.evenNum;
            
            if (playerNumbers == null)
            {
                return false;
            }

            for (int i = 0; i < playerNumbers.Length; i++)
            {
                if (playerNumbers[i] == number)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}