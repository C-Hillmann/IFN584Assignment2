using BaseFramework;

namespace BaseGame
{
    public class TicTacToeAi : Player
    {
        public TicTacToeAi(string name, bool isOdd) : base(name, isOdd) { }

        public override string GetInput(IBoard board)
        {
            var ticTacToeBoard = board as TicTacToeBoard;
            if (ticTacToeBoard == null)
            {
                throw new InvalidOperationException("Computer AI only works with TicTacToeBoard");
            }

            int boardSize = board.Size;
            int[] numberList = isOdd ? oddNum : evenNum;
            int winningSum = ticTacToeBoard.CalculateWinningSum(); //Winning sum

            //Check for winning move
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (ticTacToeBoard.IsCellEmpty(i, j))
                    {
                        for (int x = 0; x < numberList.Length; x++)
                        {
                            int inputWinNum = numberList[x];
                            if (inputWinNum == -1) 
                                continue; 

                            string originalValue = board.GetCell(i, j);
                            board.SetCell(i, j, inputWinNum.ToString());
                            
                            //change win-check to sum from symbol
                            bool isWin = ticTacToeBoard.CheckWinWithSum(i, j, winningSum);
                            
                            board.SetCell(i, j, originalValue);
                            
                            if (isWin)
                            {
                                Console.WriteLine(name + ": " + inputWinNum + " (Winning move!)");
                                return i + " " + j + " " + inputWinNum;
                            }
                        }
                    }
                }
            }

            //block opponent
            int[] opponentNumbers = isOdd ? evenNum : oddNum;
            if (opponentNumbers != null)
            {
                for (int i = 0; i < boardSize; i++)
                {
                    for (int j = 0; j < boardSize; j++)
                    {
                        if (ticTacToeBoard.IsCellEmpty(i, j))
                        {
                            for (int x = 0; x < opponentNumbers.Length; x++)
                            {
                                int opponentNum = opponentNumbers[x];
                                if (opponentNum == -1) 
                                    continue;

                                string originalValue = board.GetCell(i, j);
                                board.SetCell(i, j, opponentNum.ToString());
                   
                                bool opponentWins = ticTacToeBoard.CheckWinWithSum(i, j, winningSum);
                                
                                board.SetCell(i, j, originalValue);
                                
                                if (opponentWins)
                                {
                                    //use available number
                                    for (int n = 0; n < numberList.Length; n++)
                                    {
                                        int blockingNum = numberList[n];
                                        if (blockingNum != -1)
                                        {
                                            Console.WriteLine(name + ": " + blockingNum + " (Blocking move!)");
                                            return i + " " + j + " " + blockingNum;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //check for moves
            int[] strategicOrder = GetStrategicMoveOrder(boardSize);
            
            for (int moveIndex = 0; moveIndex < strategicOrder.Length; moveIndex++)
            {
                int position = strategicOrder[moveIndex];
                var (row, col) = ticTacToeBoard.GetCoordinatesFromPosition(position);
                
                if (ticTacToeBoard.IsCellEmpty(row, col))
                {
                    for (int n = 0; n < numberList.Length; n++)
                    {
                        int compMove = numberList[n];
                        
                        if (compMove != -1)
                        {
                            Console.WriteLine(name + ": " + compMove + " (Strategic move)");
                            return row + " " + col + " " + compMove;
                        }
                    }
                }
            }

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (ticTacToeBoard.IsCellEmpty(i, j))
                    {
                        for (int n = 0; n < numberList.Length; n++)
                        {
                            int compMove = numberList[n];
                            
                            if (compMove != -1)
                            {
                                Console.WriteLine(name + ": " + compMove);
                                return i + " " + j + " " + compMove;
                            }
                        }
                    }
                }
            }
            
            return "0 0 0";
        }

        private int[] GetStrategicMoveOrder(int boardSize)
        {
            if (boardSize == 3)
            {
                return new int[] { 5, 1, 3, 7, 9, 2, 4, 6, 8 };
            }
            else
            {
                //start from center
                int[] order = new int[boardSize * boardSize];
                int center = (boardSize * boardSize + 1) / 2;
                order[0] = center;

                int index = 1;
                for (int i = 1; i <= boardSize * boardSize; i++)
                {
                    if (i != center)
                    {
                        order[index++] = i;
                    }
                }
                return order;
            }
        }
    }
}