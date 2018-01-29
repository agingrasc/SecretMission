namespace SecretMission
{
    public interface ILineReaderWriter
    {
        void WriteLine(string line);
        string ReadLine();
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