namespace Votter.ConsoleClient
{
    using System;
    using System.Linq;
    using Votter.Data;

    public class VotterConsoleClient
    {
        private static readonly VotterData votterData = new VotterData();

        internal static void Main()
        {
            Console.WriteLine(votterData.Pictures.All().Count());
        }
    }
}