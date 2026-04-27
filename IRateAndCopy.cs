namespace Lab1
{
    public interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }
}