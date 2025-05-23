using BaseGame;

namespace BaseFramework
{
    public static class CustomGames
    {

        public static IBoard CreateBoard(GameType gameType, int size)
        {

            if (gameType == GameType.TicTacToe)
            {
                return null;    //new TicTacToeBoard(size);
            }
            else if (gameType == GameType.Notakto)
            {
                return new CompositeNotaktoBoard();
            }
            else if (gameType == GameType.Gomoku)
            {
                return new GomokuBoard();
            }
            else
                throw new ArgumentException("Unsupported game type: " + gameType);
        }
        /*
        public static Player CreatePlayer(string gameType, string name, bool isOdd)
        {

            if (gameType == "TicTacToe" && name.ToLower().Contains("computer"))
            {
                //return new TicTacToeComputer(name, isOdd);
            }
            else if (gameType == "Notakto" && name.ToLower().Contains("computer"))
            {
                //return new NotaktoComputer(name);
            }
            else if (gameType == "Gomoku" && name.ToLower().Contains("computer"))
            {
                //return new GomokuComputer(name);
                //else
            }
            return new Human(name, isOdd);
            //}
        }
        */
    }
}
