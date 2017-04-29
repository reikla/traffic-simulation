using System;
using System.Collections.Generic;
using NLog;
namespace TrafficSimulation.Simulation.Applications
{
  class Program
  {

    private static readonly List<IController> Controllers = new List<IController>();
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    static void Main(string[] args)
    {
      Controllers.Add(new LoggingController(false));
      Controllers.Add(new SimulationWebserviceController());
      Controllers.ForEach(x => x.Start());

      Console.WriteLine("Services Started. Press any Key to exit.");
      Console.ReadKey();


      ShutDown();
    }

    private static void ShutDown()
    {
      Controllers.ForEach(x => x.Shutdown());
    }
  }
}
