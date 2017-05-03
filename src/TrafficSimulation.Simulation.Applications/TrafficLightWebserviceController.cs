namespace TrafficSimulation.Simulation.Applications
{

  /// <summary>
  /// Controller used to start the TrafficLightControl webservice.
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Applications.ProcessController" />
  class TrafficLightWebserviceController : ProcessController
  {
    public TrafficLightWebserviceController() : base(true,
      "TrafficSimulation.TrafficLightControl.WebService",
      @"TrafficLightControlService", null)
    {
    }

  }
}