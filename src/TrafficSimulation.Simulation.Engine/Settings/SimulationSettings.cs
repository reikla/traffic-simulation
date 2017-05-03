namespace TrafficSimulation.Simulation.Engine.Settings
{
  /// <summary>
  /// Settings of the simulation
  /// </summary>
  public class SimulationSettings
  {
    /// <summary>
    /// Gets or sets the tick rate in ms.
    /// </summary>
    public double TickRate { get; set; }
    /// <summary>
    /// Gets or sets the size of the tick step size in seconds (e.g 0.1 for 100ms).
    /// </summary>
    public double TickStepSize { get; set; }
     /// <summary>
    /// Gets or sets the target vehicle count. That is the total number of cars desired for the simulation
    /// </summary>
    public int TargetVehicleCount { get; set; }
  }
}
