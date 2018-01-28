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

    public interface ILineLineReaderLineWriter : ILineWriter, ILineReader
    {
    }

    public class ConsoleWrapper: ILineLineReaderLineWriter
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