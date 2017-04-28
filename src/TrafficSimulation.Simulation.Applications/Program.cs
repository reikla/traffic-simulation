using System;
using System.Collections.Generic;
using NLog;
using TrafficSimulation.Simulation.Engine;
namespace TrafficSimulation.Simulation.Applications
{
  class Program
  {

    private static readonly List<IController> Controllers = new List<IController>();
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    static void Main(string[] args)
    {
      var engine = new SimulationEngine();
      var loggingController = new LoggingController(false);
      Controllers.Add(loggingController);
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
