public class Program
{
    private static void Main()
    {
        var inputLines = File.ReadAllLines("SampleInput/SampleInput.txt");
        var processor = new RobotProcessor();
        var outputLines = processor.Process(inputLines);
        foreach (var line in outputLines)
        {
            Console.WriteLine(line);
        }
        Console.ReadLine();
    }
}