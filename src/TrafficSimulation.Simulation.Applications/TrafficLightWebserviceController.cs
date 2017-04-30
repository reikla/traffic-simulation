namespace TrafficSimulation.Simulation.Applications
{
  class TrafficLightWebserviceController : ProcessController
  {
    public TrafficLightWebserviceController() : base(true,
      "TrafficSimulation.TrafficLightControl.WebService",
      @"TrafficLightControlService", null)
    {
    }

  }
}