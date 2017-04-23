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
      loggingController.Start();
      
      engine.Init();
      engine.Start();
      Console.ReadKey();

      engine.Stop();
      ShutDown();
    }

    private static void ShutDown()
    {
      Controllers.ForEach(x => x.Shutdown());
    }
  }
}
