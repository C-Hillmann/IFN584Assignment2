using System.Collections.Generic;

namespace BaseFramework
{
    public interface IGameLogic
    {
        bool IsValidMove(IMove move, IBoard board);
        void MakeMove(IMove move, IBoard board);
        bool CheckWin(IMove lastMove, IBoard board);
        bool IsGameOver();
        List<IMove> GetAvailableMoves(IBoard board);
    }
}
