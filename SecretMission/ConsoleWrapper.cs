namespace SecretMission
{
    public interface ILineWriter
    {
        void WriteLine(string line);
    }

    public interface ILineReader
    {
        string ReadLine();
    }

    public interface ILineReaderWriter : ILineWriter, ILineReader
    {
    }

    public class ConsoleWrapper: ILineReaderWriter
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