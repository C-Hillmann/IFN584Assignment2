namespace BaseFramework
{
    public class Computer : Player
    {
        public Computer(string name, bool isOdd) : base(name, isOdd) { }

        
        public override string GetInput(IBoard board)
        {
            Console.WriteLine($"{Name} (computer) ");
            return "0 0 0"; 
        }
    }
}
