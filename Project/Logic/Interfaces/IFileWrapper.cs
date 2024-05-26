public interface IFileWrapper
{
    string ReadAllText(string path);
    void WriteAllText(string path, string content);
}
