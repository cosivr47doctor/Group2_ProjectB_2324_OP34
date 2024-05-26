public class MockUserInput : IUserInput
{
    private readonly Queue<string> _inputQueue;

    public MockUserInput(IEnumerable<string> inputs)
    {
        _inputQueue = new Queue<string>(inputs);
    }

    public string ReadLine()
    {
        return _inputQueue.Count > 0 ? _inputQueue.Dequeue() : string.Empty;
    }

    public void AddInputs(IEnumerable<string> inputs)
    {
        foreach (var input in inputs)
        {
            _inputQueue.Enqueue(input);
        }
    }
}
