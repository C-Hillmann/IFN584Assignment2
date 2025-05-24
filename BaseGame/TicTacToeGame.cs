using BaseFramework;

namespace BaseGame
{
    public class TicTacToeGame:Game
    {
        public TicTacToeGame(Player p1, Player p2) 
            : this(new TicTacToeBoard(GetBoardSize()), p1, p2)
        {
        }

        public TicTacToeGame(IBoard board, Player p1, Player p2) 
            : base(GameType.TicTacToe, board, new TicTacToeGameLogic(), p1, p2)
        {
            SetupPlayers();
        }

        public TicTacToeGame(IBoard board, Player p1, Player p2, Player currentPlayer) 
            : base(GameType.TicTacToe, board, new TicTacToeGameLogic(), p1, p2, currentPlayer)
        {
        }

        public static TicTacToeGame CreateNewGame(Player p1, Player p2)
        {
            Console.WriteLine("Enter board size: ");
            int boardSize;
            while (!int.TryParse(Console.ReadLine(), out boardSize) || boardSize < 3)
            {
                Console.WriteLine("Please enter a board size equal or greater than 3:");
            }

            var board=new TicTacToeBoard(boardSize);
            return new TicTacToeGame(board, p1, p2);
        }

        private static int GetBoardSize()
        {
            Console.WriteLine("Enter board size: ");
            int boardSize;
            while (!int.TryParse(Console.ReadLine(), out boardSize) || boardSize < 3)
            {
                Console.WriteLine("Please enter a board size equal or greater than 3:");
            }
            return boardSize;
        }

        private void SetupPlayers()
        {
            int maxNum=board.Size * board.Size;
            player1.AssignNumbers(maxNum, board.Size);
            player2.AssignNumbers(maxNum, board.Size);

            Console.WriteLine("\nPlayer numbers assigned:");
            ShowPlayerNumbers();
        }

        protected override IMove GetInputPrompt()
        {
            if (currentPlayer is Human)
            {
                return GetHumanMove();
            }
            else if (currentPlayer is TicTacToeAi)
            {
                return GetAiMove();
            }
            else if (currentPlayer is Computer)
            {
                return GetComputerMove();
            }
            else
            {
                return null;
            }
        }

        public new void GamePlay()
        {
            Console.Clear();
            Console.WriteLine($"{gameType} Game Start:");
            board.Display();

            while (!gameLogic.IsGameOver())
            {
                Console.WriteLine("Press m to make next move, type save to save current game, or type help to view help menu");
                string input=Console.ReadLine();

                if (input=="m")
                {
                    Console.WriteLine($"\n{currentPlayer.Name}'s turn:");
                    IMove move = GetInputPrompt();

                    if (move==null)
                    {
                        Console.WriteLine("Invalid input or command.");
                        continue;
                    }

                    if (!gameLogic.IsValidMove(move, board))
                    {
                        Console.WriteLine("Invalid move. Try again.");
                        continue;
                    }

                    ICommand command=new TicTacToePlaceMoveCommand(move);
                    undoRedoManager.SetCommand(command);
                    undoRedoManager.Execute(board);

                    board.Display();

                    if (gameLogic.CheckWin(move, board))
                    {
                        Console.WriteLine($"\n {currentPlayer.Name} wins!");
                        break;
                    }

                    PlayerTurn();
                }
                else if (input=="undo")
                {
                    undoRedoManager.Undo(board);
                    board.Display();
                    Console.WriteLine("Current available numbers:");
                    ShowPlayerNumbers();
                }
                else if (input=="redo")
                {
                    undoRedoManager.Redo(board);
                    board.Display();
                    Console.WriteLine("Current available numbers:");
                    ShowPlayerNumbers();
                }
                else if (input=="save")
                {
                    Save();
                }
                else if (input=="help")
                {
                    HelpMenu();
                }
                else if (input=="exit")
                {
                    Console.WriteLine("Returning to main menu...");
                    return;
                }
            }

            Console.WriteLine("\n Game Over");
        }

        private IMove GetHumanMove()
        {
            while (true)
            {
                Console.Write("Enter any key to make your move or enter help/save/exit: ");
                string input=Console.ReadLine();

                if (input == "help")
                {
                    HelpMenu();
                    continue;
                }
                else if (input=="save")
                {
                    Save();
                    continue;
                }
                else if (input=="exit")
                {
                    Console.WriteLine("Game exited.");
                    Environment.Exit(0);
                }

                try
                {
                    Console.Write("Enter row: ");
                    int row=Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter column: ");
                    int col=Convert.ToInt32(Console.ReadLine());

                    Console.Write("Enter number: ");
                    int number= Convert.ToInt32(Console.ReadLine());

                    row--;
                    col--;

                    if (!CheckPlayerNumber(number))
                    {
                        Console.WriteLine("That number is not available to you.");
                        continue;
                    }

                    return new TicTacToeMove(row, col, number.ToString(), currentPlayer);
                }
                catch
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }
            }
        }

        private IMove GetAiMove()
        {
            var ticTacToeAi=currentPlayer as TicTacToeAi;
            string input=ticTacToeAi.GetInput(board);
            string[] parts= input.Split(' ');
            
            if (parts.Length==3)
            {
                int row= Convert.ToInt32(parts[0]);
                int col= Convert.ToInt32(parts[1]);
                int number = Convert.ToInt32(parts[2]);
                return new TicTacToeMove(row, col, number.ToString(), currentPlayer);
            }
            
            return null;
        }

        private IMove GetComputerMove()
        {
            var ticTacToeBoard=board as TicTacToeBoard;
            int[] numberList=currentPlayer.IsOdd ? currentPlayer.oddNum : currentPlayer.evenNum;

            for (int i= 0; i < board.Size; i++)
            {
                for (int j= 0; j < board.Size; j++)
                {
                    if (ticTacToeBoard.IsCellEmpty(i, j))
                    {
                        for (int n= 0; n < numberList.Length; n++)
                        {
                            if (numberList[n] != -1)
                            {
                                Console.WriteLine($"{currentPlayer.Name}: {numberList[n]}");
                                return new TicTacToeMove(i, j, numberList[n].ToString(), currentPlayer);
                            }
                        }
                    }
                }
            }
            
            return null;
        }

        private bool CheckPlayerNumber(int number)
        {
            int[] playerNumbers=currentPlayer.IsOdd ? currentPlayer.oddNum : currentPlayer.evenNum;
            
            for (int i= 0; i < playerNumbers.Length; i++)
            {
                if (playerNumbers[i]==number)
                {
                    return true;
                }
            }
            return false;
        }

        private void ShowPlayerNumbers()
        {
            Console.WriteLine($"{player1.Name} numbers (odd):");
            for (int i = 0; i < player1.oddNum.Length; i++)
            {
                if (player1.oddNum[i] != -1)
                {
                    Console.Write(player1.oddNum[i] + " ");
                }
            }
            Console.WriteLine();

            Console.WriteLine($"{player2.Name} numbers (even):");
            for (int i = 0; i < player2.evenNum.Length; i++)
            {
                if (player2.evenNum[i] != -1)
                {
                    Console.Write(player2.evenNum[i] + " ");
                }
            }
            Console.WriteLine();
        }

        protected override void HelpMenu()
        {
            Console.WriteLine("\n=== TicTacToe Help Menu ===");
            Console.WriteLine("Game Rules:");
            Console.WriteLine("- Players take turns placing numbers on the board");
            Console.WriteLine("- Player 1 uses odd numbers, Player 2 uses even numbers");
            Console.WriteLine("- Win by getting a row, column, or diagonal that sums to the winning sum");
            Console.WriteLine($"- Winning sum for this board: {((TicTacToeGameLogic)gameLogic).GetWinningSum(board.Size)}");
            Console.WriteLine("\nCommands:");
            Console.WriteLine("- To make a move: enter row, column, and number when prompted");
            Console.WriteLine("- Enter 'undo' to undo the last move");
            Console.WriteLine("- Enter 'redo' to redo a move");
            Console.WriteLine("- Enter 'save' to save the game");
            Console.WriteLine("- Enter 'help' to view this menu");
            Console.WriteLine("- Enter 'exit' to quit the game");
            ShowPlayerNumbers();
            Console.WriteLine();
        }
    }

    public class TicTacToeMove : IMove
    {
        public int Row { get; }
        public int Col { get; }
        public string Value { get; }
        public Player Player { get; }

        public TicTacToeMove(int row, int col, string value, Player player)
        {
            Row=row;
            Col=col;
            Value=value;
            Player=player;
        }
    }
}