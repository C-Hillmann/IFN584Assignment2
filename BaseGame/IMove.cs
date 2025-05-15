namespace BaseFramework
{
    public interface IMove
    {
        int Row { get; }
        int Col { get; }
        string Value { get; }
        Player Player { get; }
    }
}
