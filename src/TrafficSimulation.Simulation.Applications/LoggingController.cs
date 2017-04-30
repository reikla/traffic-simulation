namespace TrafficSimulation.Simulation.Applications
{
  class LoggingController : ProcessController
  {
    public LoggingController(bool forceKillOnExit) : base(forceKillOnExit, "Sentinel", "Sentinel",
      @"TrafficSimulation.sntl"){}
  }
}
