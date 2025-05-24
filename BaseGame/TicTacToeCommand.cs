using BaseFramework;

namespace BaseGame
{
    public class TicTacToePlaceMoveCommand : ICommand
    {
        private IMove move;
        private string previousValue;
        private int numberUsed;
        private int arrayIndex= -1;
        private bool wasNumberRestored=false;

        public TicTacToePlaceMoveCommand(IMove move)
        {
            this.move=move;
            this.numberUsed=int.Parse(move.Value);
        }

        public void Execute(IBoard board)
        {
            previousValue=board.GetCell(move.Row, move.Col);
            board.SetCell(move.Row, move.Col, move.Value);
            MarkNumberAsUsed(move.Player, numberUsed);
            wasNumberRestored= false;
        }

        private void MarkNumberAsUsed(Player player, int number)
        {
            int[] playerNumbers= player.IsOdd ? player.oddNum : player.evenNum;
            
            if (playerNumbers== null)
                return;

            for (int i= 0; i < playerNumbers.Length; i++)
            {
                if (playerNumbers[i]==number)
                {
                    playerNumbers[i]=-1;
                    arrayIndex = i;
                    break;
                }
            }
        }
        
        public void Undo(IBoard board)
        {
            board.SetCell(move.Row, move.Col, previousValue);
            
            if (!wasNumberRestored && arrayIndex !=-1)
            {
                RestoreNumberToPlayer(move.Player, numberUsed);
                wasNumberRestored= true;
            }
        }

        private void RestoreNumberToPlayer(Player player, int number)
        {
            int[] playerNumbers = player.IsOdd ? player.oddNum : player.evenNum;

            if (playerNumbers == null || arrayIndex == -1)
                return;

            playerNumbers[arrayIndex] = number;
            arrayIndex = -1;
        }
    }
}