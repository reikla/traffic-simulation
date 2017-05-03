namespace TrafficSimulation.Simulation.Applications
{

  /// <summary>
  /// Controller used to start the Simulation Webservice, that includes the engine.
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Applications.ProcessController" />
  class SimulationWebserviceController : ProcessController
  {
    public SimulationWebserviceController() : base(true,
      "TrafficSimulation.Simulation.WebService",
      @"SimulationService", null)
    {
    }

  }
}