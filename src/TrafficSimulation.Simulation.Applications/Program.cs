using System;
using System.Collections.Generic;
using NLog;
namespace TrafficSimulation.Simulation.Applications
{
  /// <summary>
  /// The Main Application. It creates the other processes like the Engine, or the UI
  /// </summary>
  class Program
  {

    private static readonly List<IController> Controllers = new List<IController>();
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    
    /// <summary>
    /// Entry point of the Application
    /// </summary>
    static void Main(string[] args)
    {
      AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;

      Controllers.Add(new LoggingController(false));
      Controllers.Add(new SimulationWebserviceController());
      Controllers.Add(new TrafficLightWebserviceController());
      Controllers.Add(new SimulationUiController());
      Controllers.ForEach(x => x.Start());

      Console.WriteLine("Services Started. Press any Key to exit.");
      Console.ReadKey();
    }

    private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
    {
      ShutDown();
    }

    private static void ShutDown()
    {
      Controllers.ForEach(x => x.Shutdown());
    }
  }
}
