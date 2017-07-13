using VehicleHandoverLibrary;

namespace TrafficSimulation.Simulation.Engine.Settings
{
  /// <summary>
  /// Settings of the simulation
  /// </summary>
  public interface ISimulationSettings
  {
    /// <summary>
    /// Gets or sets the tick rate in ms.
    /// </summary>
    double TickRate { get; set; }
    /// <summary>
    /// Gets or sets the size of the tick step size in seconds (e.g 0.1 for 100ms).
    /// </summary>
    double TickStepSize { get; set; }
     /// <summary>
    /// Gets or sets the target vehicle count. That is the total number of cars desired for the simulation
    /// </summary>
    int TargetVehicleCount { get; set; }

    /// <summary>
    /// Gets or sets own goup.
    /// </summary>
    Groups OwnGoup { get; set; }

    /// <summary>
    /// Gets or sets the target group.
    /// </summary>
    Groups TargetGroup { get; set; }
  }
}
