
namespace BaseFramework
{
    public class UndoRedoManager
    {
        private Stack<ICommand> undoStack = new Stack<ICommand>();
        private Stack<ICommand> redoStack = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command, IBoard board)
        {
            command.Execute(board);
            undoStack.Push(command);
            redoStack.Clear();
        }

        public void Undo(IBoard board)
        {
            if (undoStack.Count > 0)
            {
                ICommand command = undoStack.Pop();
                command.Undo(board);
                redoStack.Push(command);
                Console.WriteLine("Undo performed.");
            }
            else
            {
                Console.WriteLine("Nothing to undo.");
            }
        }

        public void Redo(IBoard board)
        {
            if (redoStack.Count > 0)
            {
                ICommand command = redoStack.Pop();
                command.Execute(board);
                undoStack.Push(command);
                Console.WriteLine("Redo performed.");
            }
            else
            {
                Console.WriteLine("Nothing to redo.");
            }
        }

        public void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }
    }
}
