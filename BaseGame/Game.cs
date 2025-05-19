using BaseGame;

namespace BaseFramework
{
    public abstract class Game
    {
        protected IBoard board;
        protected IGameLogic gameLogic;
        protected Player player1;
        protected Player player2;
        protected Player currentPlayer;
        //protected UndoRedoManager undoRedoManager;

        protected GameType gameType;

        public Game(GameType gameType, IBoard board, IGameLogic logic, Player p1, Player p2)
        {
            this.gameType = gameType;
            this.board = board;
            this.gameLogic = logic;
            this.player1 = p1;
            this.player2 = p2;
            this.currentPlayer = p1;
            
        }

        public Game(GameType gameType, IBoard board, IGameLogic logic, Player p1, Player p2, Player currentPlayer)  //new
        {
            this.gameType = gameType;
            this.board = board;
            this.gameLogic = logic;
            this.player1 = p1;
            this.player2 = p2;

            if (currentPlayer != p1 && currentPlayer != p2)
                throw new ArgumentException("Current player must be either player1 or player2.");

            this.currentPlayer = currentPlayer;
        }

        public void GamePlay()
        {
            Console.Clear();
            Console.WriteLine($"{gameType} Game Start:");
            board.Display();

            while (!gameLogic.IsGameOver())
            {
                Console.WriteLine("Press m to make next move, type save to save current game, or type help to view help menu");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "m":
                        Console.WriteLine($"\n{currentPlayer.Name}'s turn:");
                        IMove move = GetInputPrompt();

                        if (move == null)
                        {
                            Console.WriteLine("Invalid input or command.");
                            continue;
                        }

                        if (!gameLogic.IsValidMove(move, board))
                        {
                            Console.WriteLine("Invalid move. Try again.");
                            continue;
                        }

                        gameLogic.MakeMove(move, board);

                        board.Display();

                        if (gameLogic.CheckWin(move, board))
                        {
                            Console.WriteLine($"\n {currentPlayer.Name} wins!");
                            break;
                        }

                        PlayerTurn();
                        break;

                    case "save":
                        Save();

                        break;

                    case "help":
                        HelpMenu(); break;

                }
 
            }

            Console.WriteLine("\n Game Over");
        }

        protected void PlayerTurn()
        {
            if (currentPlayer == player1)
            {
                currentPlayer = player2;
            }
            else
            {
                currentPlayer = player1;
            }

        }
        

        protected void Save()
        {
            SaveManager.SaveGame(gameType, board, currentPlayer, player1, player2);
            Console.WriteLine("Game saved successfully.");
        }

        protected virtual void HelpMenu()
        {
            Console.WriteLine(" Help Menu:");
            Console.WriteLine("- Guide for game: ");
            Console.WriteLine("  To make move input: row col number");
            Console.WriteLine("  Example: For row 1 and column 2 want to put 7");
            Console.WriteLine("  Put moves- 1 2 7");
            Console.WriteLine("- Enter 'undo' to undo the last move");
            Console.WriteLine("- Enter 'redo' to redo a move");
            Console.WriteLine("- Enter 'save' to save the game");
            Console.WriteLine("- Enter 'exit' to quit");
            Console.WriteLine("- Enter 'help' to view this help menu again\n");
        }

        
        protected abstract IMove GetInputPrompt();
    }
}
