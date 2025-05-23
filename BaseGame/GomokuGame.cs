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

        public GomokuGame(IBoard board, Player p1, Player p2, Player currentPlayer) : base(GameType.Gomoku, board, new GomokuGameLogic(), p1, p2, currentPlayer)
        {
        }

        protected override IMove GetInputPrompt()       // make a move
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
                // Derive the next move for AI player
                Move winMove = CheckRows(board);
                if (winMove == null)
                {
                    winMove = CheckCols(board);
                }
                if (winMove == null)
                {
                    winMove = CheckDiagRight(board);
                }
                if (winMove == null)
                { 
                    winMove = CheckDiagLeft(board);
                }


                if (winMove != null) 
                {
                    return winMove;
                }
                else
                {
                    return RandomMove(board);
                }
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

        private Move CheckRows(IBoard board)
        {
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 11; col++)
                {
                    List<(int, int)> positions = new List<(int, int)>();

                    int countAi = 0, emptyCount = 0;

                    for (int k = 0; k < 5; k++)
                    {
                        string cell = board.GetCell(row, col+ k);
                        positions.Add((row, col + k));

                        if (cell == deterimePlayerColour(currentPlayer))
                        {
                            countAi++;
                        }
                        else if (cell == " ")
                        { 
                            emptyCount++;
                        }
                    }

                    //if 4 W's and 1 empty in block of 5
                    if (countAi == 4 && emptyCount == 1)
                    {
                        foreach (var pos in positions)
                        {
                            if (board.GetCell(pos.Item1, pos.Item2) == " ")
                            {
                                return new Move(pos.Item1, pos.Item2, deterimePlayerColour(currentPlayer), currentPlayer);
                            }
                        }
                    
                    }
                
                }
            
            }
            return null;
        }

        private Move CheckCols(IBoard board)
        { 
            for(int col = 0; col < 15;col++)
            {
                for (int row = 0; row < 11; row++)
                {
                    List<(int, int)> positions = new List<(int, int)>();

                    int countAi = 0, emptyCount = 0;

                    for (int k = 0; k < 5; k++)
                    {
                        string cell = board.GetCell(row + k, col);
                        positions.Add((row + k, col));

                        if (cell == deterimePlayerColour(currentPlayer))
                        {
                            countAi++;
                        }
                        else if (cell == " ")
                        {
                            emptyCount++;
                        }
                    }

                    //if 4 W's and 1 empty in block of 5
                    if (countAi == 4 && emptyCount == 1)
                    {
                        foreach (var pos in positions)
                        {
                            if (board.GetCell(pos.Item1, pos.Item2) == " ")
                            {
                                return new Move(pos.Item1, pos.Item2, deterimePlayerColour(currentPlayer), currentPlayer);
                            }
                        }

                    }

                }
            }

            return null;
        }

        private Move CheckDiagRight(IBoard board)
        {
            for (int row = 0; row < 11; row++)
            {
                for (int col = 0; col < 11; col++)
                {
                    List<(int, int)> positions = new List<(int, int)>();

                    int countAi = 0, emptyCount = 0;
                    for (int k = 0; k < 5; k++)
                    {
                        string cell = board.GetCell(row + k, col +k);
                        positions.Add((row + k, col+k));

                        if (cell == deterimePlayerColour(currentPlayer))
                        {
                            countAi++;
                        }
                        else if (cell == "  ")
                        {
                            emptyCount++;
                        }
                    }
                    //if 4 W's and 1 empty in block of 5
                    if (countAi == 4 && emptyCount == 1)
                    {
                        foreach (var pos in positions)
                        {
                            if (board.GetCell(pos.Item1, pos.Item2) == "  ")
                            {
                                return new Move(pos.Item1, pos.Item2, deterimePlayerColour(currentPlayer), currentPlayer);
                            }
                        }

                    }
                }
            
            }
            return null;
        }

        private Move CheckDiagLeft(IBoard board)    //top right to bottom left
        {
            for (int row = 0; row < 11; row++)
            {
                for (int col = 14; col !< 4; col--)
                {
                    List<(int, int)> positions = new List<(int, int)>();

                    int countAi = 0, emptyCount = 0;

                    for (int k = 0; k < 5; k++)
                    {
                        string cell = board.GetCell(row - k, col - k);
                        positions.Add((row - k, col - k));

                        if (cell == deterimePlayerColour(currentPlayer))
                        {
                            countAi++;
                        }
                        else if (cell == "  ")
                        {
                            emptyCount++;
                        }
                    }
                    //if 4 W's and 1 empty in block of 5
                    if (countAi == 4 && emptyCount == 1)
                    {
                        foreach (var pos in positions)
                        {
                            if (board.GetCell(pos.Item1, pos.Item2) == "  ")
                            {
                                return new Move(pos.Item1, pos.Item2, deterimePlayerColour(currentPlayer), currentPlayer);
                            }
                        }

                    }
                }
            
            }
            return null;
        }
        private Move RandomMove(IBoard board)
        {
            Random randnumber = new Random();

            List<(int, int)> openGridspots = new List<(int, int)>();

            // puts all open grid spots in list
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 15; col++)
                {
                    string value = board.GetCell(row, col);
                    if (value == null || value == "")
                    {
                        openGridspots.Add((row, col));
                    }
                }
            }
            //randomly select a number in the open spots list
            int selectOpengrid = randnumber.Next(0, openGridspots.Count);
            return new Move(openGridspots[selectOpengrid].Item1, openGridspots[selectOpengrid].Item2, deterimePlayerColour(currentPlayer), currentPlayer);
        }

    }

    public class GomokuGameLogic : IGameLogic
    {
        private bool gameOver = false;
        public bool CheckWin(IMove lastMove, IBoard board)
        {
            bool returnValue = testHorizontal(board, lastMove)
                    || testVertical(board, lastMove)
                    || testDiagonal1(board, lastMove)
                    || testDiagonal2(board, lastMove);

            if (returnValue)
            {
                gameOver = true;
            }
            return returnValue;
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
            if (existingValue == null || existingValue == "")            //change from null to ""
            {
                return true;
            }

            return false;
        }

        public void MakeMove(IMove move, IBoard board)
        {
            board.SetCell(move.Row, move.Col, move.Value);
        }

        private bool testHorizontal(IBoard board, IMove move)
        {
            // Test to see if there are 5 of the same values horinzontally across the row
            var matchingCells = new List<(int, int)>();

            matchingCells.Add((move.Row, move.Col));
            // look to the right of the move
            int idx = move.Col + 1;
            while (idx < 15 && board.GetCell(move.Row, idx) == move.Value)
            {
                matchingCells.Add((move.Row, idx));
                idx++;
            }
            // look to the left of the move
            idx = move.Col - 1;
            while (idx >= 0 && board.GetCell(move.Row, idx) == move.Value)
            {
                matchingCells.Add((move.Row, idx));
                idx--;
            }

            return matchingCells.Count > 4;
        }

        private bool testVertical(IBoard board, IMove move)
        {
            // Test to see if there are 5 of the same values vertically across the column
            var matchingCells = new List<(int, int)>();

            matchingCells.Add((move.Row, move.Col));
            // look below the move
            int idx = move.Row + 1;
            while (idx < 15 && board.GetCell(idx, move.Col) == move.Value)
            {
                matchingCells.Add((idx, move.Col));
                idx++;
            }
            // look above the move
            idx = move.Row - 1;
            while (idx >= 0 && board.GetCell(idx, move.Col) == move.Value)
            {
                matchingCells.Add((idx, move.Col));
                idx--;
            }

            return matchingCells.Count > 4;
        }

        private bool testDiagonal1(IBoard board, IMove move)
        {
            // Test to see if there are 5 of the same values diagonally down/right
            var matchingCells = new List<(int, int)>();

            matchingCells.Add((move.Row, move.Col));
            // look below the move
            int rowIdx = move.Row + 1;
            int colIdx = move.Col + 1;
            while (rowIdx < 15 && colIdx < 15 && board.GetCell(rowIdx, colIdx) == move.Value)
            {
                matchingCells.Add((rowIdx, colIdx));
                rowIdx++;
                colIdx++;
            }
            // look in the other direction
            rowIdx = move.Row - 1;
            colIdx = move.Col - 1;
            while (rowIdx >= 0 && colIdx >= 0 && board.GetCell(rowIdx, colIdx) == move.Value)
            {
                matchingCells.Add((rowIdx, colIdx));
                rowIdx--;
                colIdx--;
            }

            return matchingCells.Count > 4;
        }

        private bool testDiagonal2(IBoard board, IMove move)
        {
            // Test to see if there are 5 of the same values diagonally up/right
            var matchingCells = new List<(int, int)>();

            matchingCells.Add((move.Row, move.Col));
            // look above the move
            int rowIdx = move.Row - 1;
            int colIdx = move.Col + 1;
            while (rowIdx >= 0 && colIdx < 15 && board.GetCell(rowIdx, colIdx) == move.Value)
            {
                matchingCells.Add((rowIdx, colIdx));
                rowIdx--;
                colIdx++;
            }
            // look in the other direction
            rowIdx = move.Row + 1;
            colIdx = move.Col - 1;
            while (rowIdx < 15 && colIdx >= 0 && board.GetCell(rowIdx, colIdx) == move.Value)
            {
                matchingCells.Add((rowIdx, colIdx));
                rowIdx++;
                colIdx--;
            }

            return matchingCells.Count > 4;
        }

    }

}
