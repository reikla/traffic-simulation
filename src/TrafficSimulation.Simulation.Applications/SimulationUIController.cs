namespace TrafficSimulation.Simulation.Applications
{
  class SimulationUIController : ProcessController
  {
    public SimulationUIController() : base(true,
      "TrafficSimulation.UI.Application",
      @"UI", null, true)
    {
    }

  }
}