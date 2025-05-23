using System;
using System.IO;
using BaseGame;

namespace BaseFramework
{
    public static class SaveManager
    {
        public static Player player1;
        public static Player player2;
        public static Player currentPlayer;
        public static IBoard board;
        public static GameType gameType;


        public static void SaveGame(GameType gameType, IBoard board, Player currentPlayer, Player player1, Player player2)
        {
            const string FILENAME = "SaveGame.txt";
            const char DELIM = ',';
            StreamWriter writer = new StreamWriter(new FileStream(FILENAME, FileMode.Create, FileAccess.Write));


            writer.WriteLine(gameType);            // Save the game type
            writer.WriteLine(board.Size);          // Save board size

            // Save board grid
            if (gameType == GameType.Notakto)
            {
                CompositeNotaktoBoard compositeBoard = (CompositeNotaktoBoard)board;
                foreach (var individualBoard in compositeBoard.Boards)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            writer.Write(individualBoard.GetCell(i, j) + DELIM);
                        }
                        writer.WriteLine();
                    }
                }
            }

            else
            {
                string[,] grid = board.boardGrid();
                for (int i = 0; i < board.Size; i++)
                {
                    for (int j = 0; j < board.Size; j++)
                    {
                        writer.Write(grid[i, j] + DELIM);
                    }
                    writer.WriteLine();
                }
            }

            // Saving current player
            writer.WriteLine(currentPlayer.Name);

            // Save player 1 & 2
            writer.WriteLine(player1.Name);
            writer.WriteLine(player2.Name);
            //1st players always human, so check if 2nd player is comp or human
            //writer.WriteLine(player2 is Computer ? "AI" : "Human");

            if (gameType == GameType.TicTacToe)
            {
                writer.WriteLine(string.Join(DELIM.ToString(), player1.oddNum));
                writer.WriteLine(string.Join(DELIM.ToString(), player2.evenNum));
            }
            else
            {
                writer.WriteLine(""); // placeholder for Notakto
                writer.WriteLine(""); // placeholder for Gomoku
            }

            writer.Close();

        }

        public static void LoadGame()
        {
            const string FILENAME = "SaveGame.txt";
            const char DELIM = ',';
            FileStream inFile = new FileStream(FILENAME, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(inFile);

            gameType = GameType.Parse<GameType>(reader.ReadLine());  //  Read game type
            int size = int.Parse(reader.ReadLine());         //  Read board size


            board = CustomGames.CreateBoard(gameType, size);

            //  Load board state
            if (gameType == GameType.Notakto)
            {
                CompositeNotaktoBoard compositeBoard = board as CompositeNotaktoBoard;
                for (int boardIndex = 0; boardIndex < 3; boardIndex++)
                {
                    for (int row = 0; row < 3; row++)
                    {
                        var line = reader.ReadLine();
                        string[] cells = line.Split(DELIM, StringSplitOptions.None);

                        for (int col = 0; col < 3; col++)
                        {
                            compositeBoard.Boards[boardIndex].SetCell(row, col, cells[col]);
                        }
                    }
                }
            }

            else
            {
                string[,] grid = board.boardGrid();
                for (int i = 0; i < size; i++)
                {
                    string[] row = reader.ReadLine().Split(DELIM);
                    for (int j = 0; j < size; j++)
                    {
                        board.SetCell(i, j, row[j]);
                    }
                }
            }

            string currentPlayerName = reader.ReadLine();

            // Player 1 & 2
            string player1Name = reader.ReadLine();
            string player2Name = reader.ReadLine();


            player1 = new Human(player1Name, true); // odd
            if (player2Name.ToLower().Contains("computer"))
                player2 = new Computer(player2Name, false); // even
            else
                player2 = new Human(player2Name, false);




            if (gameType == GameType.TicTacToe)
            {
                string player1NumsLine = reader.ReadLine();
                string player2NumsLine = reader.ReadLine();

                string[] p1Fields = player1NumsLine.Split(DELIM, StringSplitOptions.RemoveEmptyEntries);
                player1.oddNum = Array.ConvertAll(p1Fields, int.Parse);

                string[] p2Fields = player2NumsLine.Split(DELIM, StringSplitOptions.RemoveEmptyEntries);
                player2.evenNum = Array.ConvertAll(p2Fields, int.Parse);
            }

            if (currentPlayerName == player1.Name)
            {
                currentPlayer = player1;
            }
            else
            {
                currentPlayer = player2;
            }


            reader.Close();
            inFile.Close();

            Console.WriteLine("Game successfully loaded!");



        }
    }
}