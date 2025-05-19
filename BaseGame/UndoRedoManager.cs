namespace BaseFramework
{
    public class UndoRedoManager
    {
        private ICommand lastCommand;   
        private ICommand redoCommand;   

        public void SetCommand(ICommand command)
        {
            lastCommand = command;
        }

        public void Execute(IBoard board)
        {
            if (lastCommand != null)
            {
                lastCommand.Execute(board);
                redoCommand = null; 
            }
        }

        public void Undo(IBoard board)
        {
            if (lastCommand != null)
            {
                lastCommand.Undo(board);
                redoCommand = lastCommand;  // Save for redo
                lastCommand = null;
                Console.WriteLine("Undo performed.");
            }
            else
            {
                Console.WriteLine("Nothing to undo.");
            }
        }

        public void Redo(IBoard board)
        {
            if (redoCommand != null)
            {
                redoCommand.Execute(board);
                lastCommand = redoCommand;
                redoCommand = null;
                Console.WriteLine("Redo performed.");
            }
            else
            {
                Console.WriteLine("Nothing to redo.");
            }
        }
    }
}
