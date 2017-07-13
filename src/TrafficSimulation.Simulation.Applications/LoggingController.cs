namespace TrafficSimulation.Simulation.Applications
{

  /// <summary>
  /// The Controller that initilizes Sentinel for Logging.
  /// </summary>
  /// <seealso cref="TrafficSimulation.Simulation.Applications.ProcessController" />
  internal class LoggingController : ProcessController
  {


    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingController"/> class.
    /// </summary>
    /// <param name="forceKillOnExit">if set to <c>true</c> [force kill on exit].</param>
    public LoggingController(bool forceKillOnExit) : base(forceKillOnExit, "Sentinel", "Sentinel",
      @"TrafficSimulation.sntl"){}
  }
}
