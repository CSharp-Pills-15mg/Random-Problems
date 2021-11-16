using System.Threading.Tasks;

namespace DustInTheWind.Step03_ThreadStaticSolution
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