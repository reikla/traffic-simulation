using System;

namespace Server
{
  internal class Program
  {
    private static void Main(string[] args)
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