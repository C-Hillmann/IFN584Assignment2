using BaseFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGame
{
    public class GomokuGame : Game
    {
        public GomokuGame(Player p1, Player p2) : this(new GomokuBoard(), p1, p2)
        {
            ((GomokuBoard) this.board).Game = this;
        }

        public GomokuGame(IBoard board, Player p1, Player p2) : base(GameType.Gomoku, board, new GomokuGameLogic(), p1, p2)
        {
        }

        protected override IMove GetInputPrompt()
        {
            if (currentPlayer is Human)
            {
                // Ask for the row / col... colour will be from the Player
                Console.WriteLine("Enter the row and column to place your piece:");
                string inputValue = Console.ReadLine().Trim();
                if (!(inputValue.Contains(' ')))
                {
                    return null;
                }

                String[] rowAndColumns = inputValue.Split(' ');
                int row = Int32.Parse(rowAndColumns[0]) - 1;
                int column = Int32.Parse(rowAndColumns[1]) - 1;
                return new Move(row, column, deterimePlayerColour(currentPlayer), currentPlayer);
            }
            else
            {
                // Derive the next move
                return null;
            }
        }

        private string deterimePlayerColour(Player p)
        {
            if (p == this.player1)
            {
                return "B";
            } else
            {
                return "W";
            }
        }
    }

    public class GomokuGameLogic : IGameLogic
    {
        private bool gameOver = false;
        public bool CheckWin(IMove lastMove, IBoard board)
        {
            for (int row = 0; row < 11; row++)
            {
                for (int col = 0; col < 11; col++)
                {
                    if (testHorizontal(board, lastMove, row, col)
                        || testVeritcal(board, lastMove, row, col)
                        || testDiagonalDownRight(board, lastMove, row, col)
                        || testDiagonalDownLeft(board, lastMove, row, col))
                    {
                        return true;
                    }
                }

            }
            return false;
        }

        public List<IMove> GetAvailableMoves(IBoard board)
        {
            throw new NotImplementedException();
        }

        public bool IsGameOver()
        {
            return gameOver;
        }

        public bool IsValidMove(IMove move, IBoard board)
        {
            string existingValue = board.GetCell(move.Row, move.Col);
            if (existingValue != null)
            {
                return false;
            }

            return true;
        }

        public void MakeMove(IMove move, IBoard board)
        {
            board.SetCell(move.Row, move.Col, move.Value);
        }

        private bool testHorizontal(IBoard board, IMove move, int row, int col)
        {
            // Test to see if there are 5 of the same values in the move to the right of the row/col
            return 
                board.GetCell(row, col) == move.Value
                && board.GetCell(row, col + 1) == move.Value
                && board.GetCell(row, col + 2) == move.Value
                && board.GetCell(row, col + 3) == move.Value
                && board.GetCell(row, col + 4) == move.Value
            ;
        }

        private bool testVeritcal(IBoard board, IMove move, int row, int col)
        {
            // Test to see if there are 5 of the same values in the move to the bottom of the row/col
            return
                board.GetCell(row, col) == move.Value
                && board.GetCell(row + 1, col) == move.Value
                && board.GetCell(row + 2, col) == move.Value
                && board.GetCell(row + 3, col) == move.Value
                && board.GetCell(row + 4, col) == move.Value
            ;
        }

        private bool testDiagonalDownRight(IBoard board, IMove move, int row, int col)
        {
            return
                board.GetCell(row, col) == move.Value
                && board.GetCell(row + 1, col + 1) == move.Value
                && board.GetCell(row + 2, col + 2) == move.Value
                && board.GetCell(row + 3, col + 3) == move.Value
                && board.GetCell(row + 4, col + 4) == move.Value
            ;
        }

        private bool testDiagonalDownLeft(IBoard board, IMove move, int row, int col)
        {
            if (col < 4)
            {
                return false;
            }
            return
                board.GetCell(row, col) == move.Value
                && board.GetCell(row + 1, col - 1) == move.Value
                && board.GetCell(row + 2, col - 2) == move.Value
                && board.GetCell(row + 3, col - 3) == move.Value
                && board.GetCell(row + 4, col - 4) == move.Value
            ;
        }

    }

}
