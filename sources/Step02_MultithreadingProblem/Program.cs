using System.Threading.Tasks;

namespace DustInTheWind.TheCriticalResource
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // System.Security.Cryptography.RandomNumberGenerator

            Problem problem = new Problem();
            await problem.Execute();
        }
    }
}