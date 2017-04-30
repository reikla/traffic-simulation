namespace TrafficSimulation.Simulation.Applications
{
  class SimulationWebserviceController : ProcessController
  {
    public SimulationWebserviceController() : base(true,
      "TrafficSimulation.Simulation.WebService",
      @"SimulationService", null)
    {
    }

  }
}