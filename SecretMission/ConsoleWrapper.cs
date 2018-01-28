namespace SecretMission
{
    public interface IWriter
    {
        void WriteLine(string line);
    }

    public interface IReader
    {
        string ReadLine();
    }

    public interface IReaderWriter : IWriter, IReader
    {
    }

    public class ConsoleWrapper: IReaderWriter
    {
        public void WriteLine(string line)
        {
            System.Console.WriteLine(line);
        }

        public string ReadLine()
        {
            return System.Console.ReadLine();
        }
    }
}