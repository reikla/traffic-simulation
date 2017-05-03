namespace TrafficSimulation.Simulation.Applications
{

  /// <summary>
  /// Controller used to start the UI Process
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Applications.ProcessController" />
  internal class SimulationUiController : ProcessController
  {
    public SimulationUiController() : base(true,
      "TrafficSimulation.UI.Application",
      @"UI", null, true)
    {
    }

  }
}