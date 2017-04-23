using System;
using System.Collections.Generic;
using NLog;
using TrafficSimulation.Simulation.Engine;
namespace TrafficSimulation.Simulation.Applications
{
  class Program
  {
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    private static List<IController> _controllers = new List<IController>();
    static void Main(string[] args)
    {
      var engine = new SimulationEngine();
      var loggingController = new LoggingController(false);
      _controllers.Add(loggingController);
      loggingController.Start();
      _logger.Warn("Hallo Log");


      engine.Start();

      Console.ReadKey();
      engine.Stop();
      ShutDown();
    }

    private static void ShutDown()
    {
      _controllers.ForEach(x => x.Shutdown());
    }
  }
}
