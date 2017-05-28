namespace WordBrainSolver.Core.Interfaces
{
    public interface IGameInputValidator
    {
        bool Validate(int lookupWordLength, int gridSize, string boardInput);
    }
}