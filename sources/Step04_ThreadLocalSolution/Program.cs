using System.Threading.Tasks;

namespace DustInTheWind.Step04_ThreadLocalSolution
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Problem problem = new Problem();
            await problem.Execute();
        }
    }
}