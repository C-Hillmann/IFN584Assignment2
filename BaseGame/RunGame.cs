using System;
using BaseGame;

namespace BaseFramework
{
    class RunGame
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("*******************************");
            Console.WriteLine("* Welcome to the Board Games! *");
            Console.WriteLine("*******************************");
            Console.WriteLine("Do you want to load a saved game or start a new one?");
            Console.WriteLine("Type 'y' to load a saved game, or 'n' to start a new game.");

            string statusChoice = Console.ReadLine().ToLower();
            Game selectedGame = null;

            if (statusChoice == "y")
            {
                // Load saved game
                SaveManager.LoadGame();

                if (SaveManager.gameType == BaseGame.GameType.TicTacToe)
                {
                    selectedGame = new BaseGame.TicTacToeGame(SaveManager.board, SaveManager.currentPlayer, SaveManager.player1, SaveManager.player2);
                }
                else if (SaveManager.gameType == BaseGame.GameType.Notakto)
                {
                    selectedGame = new BaseGame.NotaktoGame(SaveManager.board, SaveManager.currentPlayer, SaveManager.player1, SaveManager.player2);
                }
                else if (SaveManager.gameType == BaseGame.GameType.Gomoku)
                {
                    selectedGame = new BaseGame.GomokuGame(SaveManager.board, SaveManager.player1, SaveManager.player2, SaveManager.currentPlayer);
                }
                else
                {
                    Console.WriteLine("Unsupported game type found in save file.");
                    return;
                }
            }
            else
            {
                // Start new game
                Console.WriteLine("Choose a game to play:");
                Console.WriteLine("1. TicTacToe");
                Console.WriteLine("2. Notakto");
                Console.WriteLine("3. Gomoku");
                string gameChoice = Console.ReadLine();

                Console.WriteLine("Select Game Mode:");
                Console.WriteLine("1. Human vs Human");
                Console.WriteLine("2. Human vs Computer");

                int gameMode;
                while (!int.TryParse(Console.ReadLine(), out gameMode) ||
                       (gameMode != GameMode.HumanVsHuman && gameMode != GameMode.HumanVsComputer))
                {
                    Console.WriteLine("Invalid input. Please enter 1 or 2.");
                }

                Player player1;
                Player player2;



                // Game choice
                if (gameChoice == "1")
                {

                    if (gameMode == GameMode.HumanVsHuman)
                    {
                        player1 = new Human("Player 1", true);
                        player2 = new Human("Player 2", false);
                    }
                    else
                    {
                        player1 = new Human("Player 1", true);
                        player2 = new TicTacToeAi("Computer", false); // always computer
                    }
                    selectedGame = BaseGame.TicTacToeGame.CreateNewGame(player1, player2);
                }
                else if (gameChoice == "2")
                {

                    if (gameMode == GameMode.HumanVsHuman)
                    {
                        player1 = new Human("Player 1", true);
                        player2 = new Human("Player 2", false);
                    }
                    else
                    {
                        player1 = new Human("Player 1", true);
                        player2 = new Computer("Computer", false); // always computer
                    }
                    selectedGame = new BaseGame.NotaktoGame(player1, player2);
                }
                else if (gameChoice == "3")
                {

                    if (gameMode == GameMode.HumanVsHuman)
                    {
                        player1 = new Human("Player 1", true);
                        player2 = new Human("Player 2", false);
                    }
                    else
                    {
                        player1 = new Human("Player 1", true);
                        player2 = new Computer("Computer", false); // always computer
                    }
                    selectedGame = new BaseGame.GomokuGame(player1, player2);
                }
                else
                {
                    Console.WriteLine("Invalid game choice.");
                    return;
                }
            }

            if (selectedGame != null)
            {
                if (selectedGame is BaseGame.TicTacToeGame ticTacToeGame)
                {
                    ticTacToeGame.GamePlay();
                }
                else
                {
                    // Start the game
                    selectedGame.GamePlay();
                }
                
            }
        }
    }
}
