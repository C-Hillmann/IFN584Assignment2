namespace BaseFramework
{
    public interface ICommand
    {
        void Execute(IBoard board);
        void Undo(IBoard board);
    }
}
