using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new Server();
            server.Start();

            Console.WriteLine("Log Server Running.");
            Console.WriteLine("Press any key to exit...");

            Console.ReadKey();

            server.Stop();
        }
    }
}
