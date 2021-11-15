using System.Threading.Tasks;

namespace DustInTheWind.TheBadTwin
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Demo demo = new Demo();
            await demo.Execute();
        }
    }
}