using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using BaseFramework;

namespace BaseGame
{
    public class NotaktoGame : Game
    {
        protected override IMove GetInputPrompt()
        {
            Console.WriteLine("Enter your move as: Boardnumber<space>RowNumber<space>ColumnNumber");
            var compositeBoard = (CompositeNotaktoBoard)this.board;
            if (!(currentPlayer is Human))
            {
                Random random = new Random();
                List<int> indexList = Enumerable.Range(0, 27).ToList(); // First, make a list of 27 numbers
                indexList.OrderBy(_ => random.Next()).ToList(); // Randomly order the list.
                NotaktoMove smartMove = AvoidLosing(indexList, compositeBoard);
                if (smartMove != null) return smartMove;
                NotaktoMove forcedMove = ForcefullyLoseIfNoOption(indexList, compositeBoard);
                if (forcedMove != null) return forcedMove;
                return null;
            }
            else
            {
                var userInput = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(userInput)) return null;
                var inputArray = userInput.Split(" ");
                if (inputArray.Length != 3) return null;

                if (!int.TryParse(inputArray[0], out int boardIndex) || !int.TryParse(inputArray[1], out int row) || !int.TryParse(inputArray[2], out int column)) return null;

                boardIndex = boardIndex - 1;
                row = row - 1;
                column = column - 1;

                if (boardIndex < 0 || boardIndex > 2 || row < 0 || row > 2 || column < 0 || column > 2) return null;
                return new NotaktoMove(-1, row, boardIndex * 3 + column, "X", currentPlayer);
            }
        }

        private NotaktoMove ForcefullyLoseIfNoOption(List<int> indexList, CompositeNotaktoBoard compositeBoard)
        {
            foreach (int index in indexList) // When all safe moves are not possible repeat and forcefully enter in the losing cell to lose the game.
            {
                int boardIndex = index / 9;
                int cellIndex = index % 9;
                int rowIndex = cellIndex / 3;
                int colIndex = cellIndex % 3;

                var board = compositeBoard.Boards[boardIndex];

                if (board.IsCompleted() || !string.IsNullOrWhiteSpace(board.GetCell(rowIndex, colIndex)))
                    continue;

                return new NotaktoMove(-1, rowIndex, (boardIndex * 3) + colIndex, "X", currentPlayer);
            }
            return null;
        }

        private NotaktoMove AvoidLosing(List<int> indexList, CompositeNotaktoBoard compositeBoard)
        {
            foreach (int index in indexList)
            {
                int boardIndex = index / 9;
                int cellIndex = index % 9;
                int rowIndex = cellIndex / 3;
                int colIndex = cellIndex % 3;

                var board = compositeBoard.Boards[boardIndex];

                if (board.IsCompleted() || !string.IsNullOrWhiteSpace(board.GetCell(rowIndex, colIndex)))
                    continue;

                board.SetCell(rowIndex, colIndex, "X"); // setting cell just to simulate what would happen.

                bool willLose = board.IsCompleted(); // if the board gets completed it will make the ai lose.

                board.SetCell(rowIndex, colIndex, ""); // Undoing the cell simulation setting.

                if (!willLose)
                {
                    return new NotaktoMove(-1, rowIndex, (boardIndex * 3) + colIndex, "X", currentPlayer);
                }
            }
            return null;
        }

        public NotaktoGame(Player player1, Player player2) : base(GameType.Notakto, new CompositeNotaktoBoard(), new NotaktoGameLogic(), player1, player2)
        {
            var compositeNotaktoBoard = this.board as CompositeNotaktoBoard;
            for (int i = 0; i < 3; i++)
            {
                compositeNotaktoBoard.Boards[i].Game = this;
            }
        }

        public NotaktoGame(IBoard board, Player currentPlayer, Player player1, Player player2) : base(GameType.Notakto, board, new NotaktoGameLogic(), player1, player2, currentPlayer)
        {
            var compositeNotaktoBoard = this.board as CompositeNotaktoBoard;
            for (int i = 0; i < 3; i++)
            {
                compositeNotaktoBoard.Boards[i].Game = this;
            }
        }
    }

    public class NotaktoMove : Move
    {
        public int BoardIndex { get; }
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public NotaktoMove(int boardIndex, int row, int col, string value, Player player) : base(row, col, value, player)
        {
            BoardIndex = boardIndex;
            RowIndex = row;
            ColumnIndex = col;
        }
    }

    public class NotaktoGameLogic : IGameLogic
    {
        private bool gameOver = false;
        private Player loser = null;
        public bool CheckWin(IMove lastMove, IBoard board)
        {
            var move = lastMove as NotaktoMove;
            var compositeBoard = board as CompositeNotaktoBoard;
            if (move == null || compositeBoard == null) return false;

            int boardIndex = move.ColumnIndex / 3;
            var individualBoard = compositeBoard.Boards[boardIndex];

            if (individualBoard.IsCompleted())
            {
                Console.WriteLine($"Board {boardIndex + 1} is now completed.");

                if (compositeBoard.isAllCompleted())
                {
                    Console.WriteLine("All boards are completed.");
                    gameOver = true;
                    loser = move.Player;
                    return true;
                }
            }
            return false;
        }


        public Player GetLoser()
        {
            return loser;
        }

        public bool IsGameOver() => gameOver;

        public List<IMove> GetAvailableMoves(IBoard board)
        {
            throw new NotImplementedException();
        }

        public bool IsValidMove(IMove move, IBoard board)
        {
            var notaktoMove = move as NotaktoMove;
            var compositeBoard = board as CompositeNotaktoBoard;

            if (notaktoMove == null || compositeBoard == null) return false;

            int boardIndex = notaktoMove.ColumnIndex / 3;
            int individualBoardColumn = notaktoMove.ColumnIndex % 3;

            if (boardIndex < 0 || boardIndex > compositeBoard.Boards.Count) return false;

            var individualBoard = compositeBoard.Boards[boardIndex];
            if (individualBoard.IsCompleted()) return false;

            if (notaktoMove.RowIndex < 0 || notaktoMove.RowIndex >= 3) return false;
            if (individualBoardColumn < 0 || individualBoardColumn >= 3) return false;

            return string.IsNullOrWhiteSpace(individualBoard.GetCell(notaktoMove.RowIndex, individualBoardColumn));
        }

        public void MakeMove(IMove move, IBoard board)
        {
            Console.WriteLine("Went in MakeMove");
            var notaktoMove = move as NotaktoMove;
            var compositeBoard = board as CompositeNotaktoBoard;

            int boardIndex = notaktoMove.ColumnIndex / 3;
            int individualBoardColumn = notaktoMove.ColumnIndex % 3;


            compositeBoard.Boards[boardIndex].SetCell(notaktoMove.RowIndex, individualBoardColumn, notaktoMove.Value);


            if (compositeBoard.isAllCompleted())
            {
                gameOver = true;
                Console.WriteLine("Went in All Complete gameover is : " + gameOver);
                loser = move.Player;
            }

        }
    }
}